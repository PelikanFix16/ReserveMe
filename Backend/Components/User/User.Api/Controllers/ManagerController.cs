using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Api.Dto.Manager;
using User.Application.Cqrs.Commands.Manager.ManagerRegister;

namespace User.Api.Controllers
{
    [Route("/user/manager")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public ManagerController(
                   ISender mediator,
                   IMapper mapper)
        {
            _sender = mediator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterManagerAsync(ManagerRegisterRequest manager)
        {
            ManagerRegisterCommand command = _mapper.Map<ManagerRegisterCommand>(manager);
            FluentResults.Result result = await _sender.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Reasons);
        }

    }
}