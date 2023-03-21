using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Api.Dto.User;
using User.Application.Cqrs.Commands.User.UserRegister;
using User.Application.Cqrs.Commands.User.UserVerified;

namespace User.Api.Controllers
{
    [Route("/user/regular")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public UserController(
            ISender mediator,
            IMapper mapper)
        {
            _sender = mediator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync(UserRegisterRequest user)
        {
            UserRegisterCommand command = _mapper.Map<UserRegisterCommand>(user);
            FluentResults.Result result = await _sender.Send(command);
            return result.IsSuccess ? Ok(command.Id) : BadRequest(result.Reasons);
        }

        [HttpPost("confirm/{id:guid}")]
        public async Task<IActionResult> ConfirmUserAsync(Guid id)
        {
            var command = new UserVerifiedCommand { Id = id };
            FluentResults.Result result = await _sender.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Reasons);
        }
    }
}