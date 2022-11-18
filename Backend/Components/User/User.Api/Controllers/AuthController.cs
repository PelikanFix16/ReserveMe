using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using User.Api.Dto.User;
using User.Application.Cqrs.Commands.UserRegister;
using User.Application.Cqrs.Queries.UserLogin;

namespace User.Api.Controllers
{
    [Route("api/user/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public AuthController(
            ISender mediator,
            IMapper mapper)
        {
            _sender = mediator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync(UserRegisterRequest user)
        {
            var command = _mapper.Map<UserRegisterCommand>(user);
            var result = await _sender.Send(command);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result.Reasons);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUserAsync(UserLoginRequest user)
        {
            var query = _mapper.Map<UserLoginQuery>(user);
            var result = await _sender.Send(query);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result.Reasons);
        }
    }
}
