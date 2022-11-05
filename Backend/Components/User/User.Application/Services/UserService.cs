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

        public UserService(IUserProjectionRepository _repository)
        {
            this._repository = _repository;
        }

        public async Task<Result<UserProjection>> GetUserAsync(LoginDto login)
        {
            var userProjection = await _repository.GetAsync(login);
            return Result.Ok(userProjection);
        }

        public async Task<Result> UserCreateAsync(UserProjection userProjection)
        {
            await _repository.SaveAsync(userProjection);
            await _repository.SaveChangesAsync();
            return Result.Ok();
        }
    }
}
