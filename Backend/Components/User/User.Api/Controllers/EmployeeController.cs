using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Api.Dto.Employee;
using User.Application.Cqrs.Commands.Employee.EmployeeRegister;

namespace User.Api.Controllers
{
    [Route("/user/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        public EmployeeController(
           ISender mediator,
           IMapper mapper)
        {
            _sender = mediator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterEmployeeAsync(EmployeeRegisterRequest employee)
        {
            EmployeeRegisterCommand command = _mapper.Map<EmployeeRegisterCommand>(employee);
            FluentResults.Result result = await _sender.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Reasons);
        }
    }
}