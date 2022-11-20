using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using User.Application.Mapper.Dto;
using User.Application.Mapper.Projections;

namespace User.Application.Interfaces.Services
{
    public interface IUserService
    {
        public Task<Result> UserCreateAsync(UserProjection userProjection);
        public Task<Result> UserConfirmAsync(Guid id);
    }
}
