using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using User.Application.Mapper.Dto;

namespace User.Application.Cqrs.Queries.User.UserLogin
{
    public class UserLoginQuery : IRequest<Result<UserLoginDto>>
    {
        public LoginDto Login { get; set; }
        public PasswordDto Password { get; set; }
    }
}
