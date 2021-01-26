using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ScheduleGenerator.Server.Helpers;
using ScheduleGenerator.Server.Models;
using ScheduleGenerator.Shared.Auth;
using ScheduleGenerator.Shared.Dto;

namespace ScheduleGenerator.Server.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ScheduleGeneratorContext _context;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public UsersRepository(ScheduleGeneratorContext context, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest authenticateRequest)
        {
            var userFromDb = await _context.Users.SingleOrDefaultAsync(u => u.Email == authenticateRequest.Email && u.Password == Hash.GetHash(authenticateRequest.Password));

            if (userFromDb == null)
            {
                return null;
            }

            var user = _mapper.Map<UserDto>(userFromDb);

            // authentication successful so generate AuthenticateResponse with token
            return new AuthenticateResponse()
            {
                Id = user.Id,
                Email = user.Email,
                Token = Token.GenerateToken(user, _appSettings.Secret)
            };
        }

        public async Task<bool> IsEmailTaken(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
