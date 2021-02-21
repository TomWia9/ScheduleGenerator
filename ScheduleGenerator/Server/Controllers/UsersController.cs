using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScheduleGenerator.Server.Helpers;
using ScheduleGenerator.Server.Models;
using ScheduleGenerator.Server.Repositories;
using ScheduleGenerator.Shared.Auth;
using ScheduleGenerator.Shared.Dto;
using System;
using System.Threading.Tasks;

namespace ScheduleGenerator.Server.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDbRepository _dbRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UsersController(IDbRepository dbRepository, IUsersRepository usersRepository, IMapper mapper)
        {
            _dbRepository = dbRepository;
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Authenticate the user
        /// </summary>
        /// <param name="authenticateRequest">Email and password of user</param>
        /// <returns>An ActionResult of type AuthenticateResponse</returns>
        /// <response code="200">Returns the user with token</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticateResponse>> Authenticate(AuthenticateRequest authenticateRequest)
        {
            try
            {
                var response = await _usersRepository.Authenticate(authenticateRequest);

                if (response == null)
                {
                    return BadRequest(new { message = "Email or password is incorrect" });
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="user">User to create</param>
        /// <returns>An ActionResult of type UserDto</returns>
        ///<response code="201">Creates and returns created user</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost]
        public async Task<ActionResult<UserDto>> Register(UserForCreationDto user)
        {
            try
            {
                if (await _usersRepository.IsEmailTaken(user.Email))
                {
                    return Conflict();
                }

                var newUser = _mapper.Map<User>(user);
                newUser.Password = Hash.GetHash(user.Password);

                _dbRepository.Add(newUser);

                if (await _dbRepository.SaveChangesAsync())
                {
                    return CreatedAtAction(nameof(GetUser), new { userId = newUser.Id }, _mapper.Map<UserDto>(newUser));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }


        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="userId">The id of user you want to get</param>
        /// <returns>An ActionResult of type UserDto></returns>
        /// <response code="200">Returns the requested user</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDto>> GetUser(int userId)
        {
            try
            {
                var user = await _usersRepository.GetUserById(userId);
                if (user != null)
                {
                    return Ok(_mapper.Map<UserDto>(user));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return NotFound();
        }
    }
}
