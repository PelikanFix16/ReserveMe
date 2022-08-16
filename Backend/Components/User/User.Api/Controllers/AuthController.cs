using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using User.Application.Commands.UserRegister;
using User.Application.Mapper.Dto;

namespace User.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAuthAsync()
        {
            var userRegister = new UserRegisterCommand
            {
                Name = new NameDto
                {
                    FirstName = "FirstName",
                    LastName = "l"
                },
                Login = new LoginDto
                {
                    Login = "test@example.com"
                },
                Password = new PasswordDto
                {
                    Password = "Password21!@s"
                },
                BirthDate = new BirthDateDto
                {
                    BirthDate = new DateTime(2000, 1, 1)
                }
            };

            var res = await _mediator.Send(userRegister);
            return Ok("auth");
        }
    }
}
