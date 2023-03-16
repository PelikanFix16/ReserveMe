using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Application.Interfaces.Repositories;
using User.Application.Mapper.Dto;
using User.Application.Mapper.Projections;
using User.Infrastructure.Persistence.DataContext;

namespace User.Infrastructure.Persistence.Repositories
{
    public class ManagerRepository : IManagerProjectionRepository
    {
        private readonly ManagerContext _context;

        public ManagerRepository(ManagerContext managerContext) => _context = managerContext;

        public async Task<Result<ManagerProjection>> GetByEmailAsync(EmailDto email)
        {
            ManagerProjection? manager = await _context.Manager.Where(x => x.Email == email.Email).FirstOrDefaultAsync();
            return manager ?? (Result<ManagerProjection>)Result.Fail("Manager not found");
        }

        public async Task<Result<ManagerProjection>> GetByUserId(Guid id)
        {
            ManagerProjection? manager = await _context.Manager.Where(x => x.UserId == id).FirstOrDefaultAsync();
            return manager ?? (Result<ManagerProjection>)Result.Fail("Manager for user not found");
        }

        public async Task SaveAsync(ManagerProjection managerProjection) => await _context.Manager.AddAsync(managerProjection);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}