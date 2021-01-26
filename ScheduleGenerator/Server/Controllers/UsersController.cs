using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ScheduleGenerator.Server.Helpers;
using ScheduleGenerator.Server.Models;
using ScheduleGenerator.Server.Repositories;
using ScheduleGenerator.Shared.Auth;
using ScheduleGenerator.Shared.Dto;

namespace ScheduleGenerator.Server.Controllers
{
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
