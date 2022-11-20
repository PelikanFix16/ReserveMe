using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using User.Application.Interfaces.Repositories;
using User.Application.Mapper.Dto;
using User.Application.Mapper.Projections;
using User.Infrastructure.Persistence.DataContext;

namespace User.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserProjectionRepository
    {
        private readonly UserContext _context;

        public UserRepository(UserContext userContext)
        {
            _context = userContext;
        }

        public async Task<Result<UserProjection>> GetByEmailAsync(LoginDto loginDto)
        {
            var user = await _context.Users.Where(x => x.Email == loginDto.Login).FirstOrDefaultAsync();
            return user ?? (Result<UserProjection>)Result.Fail("User not found");
        }

        public async Task<Result<UserProjection>> GetByIdAsync(Guid id)
        {
            var user = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            return user ?? (Result<UserProjection>)Result.Fail("User not found");
        }

        public async Task SaveAsync(UserProjection userProjection)
        {
            await _context.Users.AddAsync(userProjection);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
