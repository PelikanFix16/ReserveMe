using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using User.Application.Interfaces.Repositories;
using User.Application.Mapper.Dto;
using User.Application.Projections;
using User.Infrastructure.DataContext;

namespace User.Infrastructure.Repositories
{
    public class UserRepository : IUserProjectionRepository
    {
        private readonly UserContext _context;

        public UserRepository(UserContext userContext)
        {
            _context = userContext;
        }

        public async Task<UserProjection> GetAsync(LoginDto loginDto)
        {
            var user = await _context.Users.Where(x => x.Email == loginDto.Login).FirstOrDefaultAsync();
            if (user is null)
                throw new Exception("User not found");

            return user;
        }

        public void Save(UserProjection userProjection)
        {
            _context.Users.Add(userProjection);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
