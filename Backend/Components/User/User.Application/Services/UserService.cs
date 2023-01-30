using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public UserService(IUserProjectionRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result> UserConfirmAsync(Guid id)
        {
            var userProjection = await _repository.GetByIdAsync(id);
            if (userProjection.IsFailed)
                return Result.Fail("User not found");

            userProjection.Value.Verified = true;
            await _repository.SaveChangesAsync();
            return Result.Ok();
        }

        public async Task<Result> UserCreateAsync(UserProjection userProjection)
        {
            // check user not exists in db
            var loginDto = _mapper.Map<EmailDto>(userProjection.Email);
            var user = await _repository.GetByEmailAsync(loginDto);
            if (user.IsSuccess)
                return Result.Fail("User already exists");

            await _repository.SaveAsync(userProjection);
            await _repository.SaveChangesAsync();
            return Result.Ok();
        }
    }
}
