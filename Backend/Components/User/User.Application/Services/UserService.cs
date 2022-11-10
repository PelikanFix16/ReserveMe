using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using User.Application.Interfaces.Repositories;
using User.Application.Interfaces.Services;
using User.Application.Mapper.Dto;
using User.Application.Mapper.Projections;

namespace User.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserProjectionRepository _repository;

        public UserService(IUserProjectionRepository repository)
        {
            this._repository = repository;
        }

        public async Task<Result<UserProjection>> GetUserAsync(LoginDto login)
        {
            var projection = await _repository.GetAsync(login);
            return projection;
        }

        public async Task<Result> UserCreateAsync(UserProjection userProjection)
        {
            await _repository.SaveAsync(userProjection);
            await _repository.SaveChangesAsync();
            return Result.Ok();
        }
    }
}
