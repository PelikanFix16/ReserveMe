using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using User.Application.Interfaces.Repositories;
using User.Application.Interfaces.Security;

namespace User.Application.Cqrs.Queries.UserLogin
{
    public class UserLoginHandler : IRequestHandler<UserLoginQuery, Result<UserLoginDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserProjectionRepository _userRepository;
        private readonly ISecurityHash _security;

        public UserLoginHandler(IMapper mapper, IUserProjectionRepository userRepository, ISecurityHash security)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _security = security;
        }

        public async Task<Result<UserLoginDto>> Handle(UserLoginQuery request, CancellationToken cancellationToken)
        {
            var userProjection = await _userRepository.GetAsync(request.Login);

            if (userProjection.IsFailed)
                return Result.Fail("User not exists");

            if (!_security.VerifyHashedPassword(userProjection.Value.Password, request.Password.Password))
                return Result.Fail("Password is incorrect");

            var userLoginDto = _mapper.Map<UserLoginDto>(userProjection.Value);
            return Result.Ok(userLoginDto);
        }
    }

}
