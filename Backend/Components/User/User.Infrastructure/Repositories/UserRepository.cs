using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Application.Interfaces.Repositories;
using User.Application.Mapper.Dto;
using User.Application.Projections;

namespace User.Infrastructure.Repositories
{
    public class UserRepository : IUserProjectionRepository
    {

        public Task<UserProjection> GetAsync(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(UserProjection userProjection)
        {
            throw new NotImplementedException();
        }
    }
}
