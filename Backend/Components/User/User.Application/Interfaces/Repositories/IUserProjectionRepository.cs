using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Application.Mapper.Dto;
using User.Application.Projections;

namespace User.Application.Interfaces.Repositories
{
    public interface IUserProjectionRepository
    {
        public Task<UserProjection> GetAsync(LoginDto loginDto);
        public Task SaveAsync(UserProjection userProjection);
        public Task SaveChangesAsync();
    }
}
