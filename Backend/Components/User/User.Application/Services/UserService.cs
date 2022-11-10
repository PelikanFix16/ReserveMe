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

        public UserService(IUserProjectionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result> UserCreateAsync(UserProjection userProjection)
        {
            // check user not exists in db
            var loginDto = _mapper.Map<LoginDto>(userProjection.Email);
            var user = await _repository.GetAsync(loginDto);
            if (user.IsSuccess)
                return Result.Fail("User already exists");

            await _repository.SaveAsync(userProjection);
            await _repository.SaveChangesAsync();
            return Result.Ok();
        }
    }
}
