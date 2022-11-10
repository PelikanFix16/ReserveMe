using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using User.Application.Mapper.Dto;
using User.Application.Mapper.Projections;

namespace User.Application.Interfaces.Repositories
{
    public interface IUserProjectionRepository
    {
        public Task<Result<UserProjection>> GetAsync(LoginDto loginDto);
        public Task SaveAsync(UserProjection userProjection);
        public Task SaveChangesAsync();
    }
}
