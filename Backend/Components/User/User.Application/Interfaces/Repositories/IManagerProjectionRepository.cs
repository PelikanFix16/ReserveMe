using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Application.Mapper.Dto;
using User.Application.Mapper.Projections;

namespace User.Application.Interfaces.Repositories
{
    public interface IManagerProjectionRepository
    {
        public Task<Result<ManagerProjection>> GetByEmailAsync(EmailDto email);
        public Task SaveAsync(ManagerProjection managerProjection);
        public Task SaveChangesAsync();
    }
}