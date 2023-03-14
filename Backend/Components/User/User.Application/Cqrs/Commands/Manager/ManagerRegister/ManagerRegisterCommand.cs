using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Application.Mapper.Dto;

namespace User.Application.Cqrs.Commands.Manager.ManagerRegister
{
    public class ManagerRegisterCommand : IRequest<Result>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public EmailDto ManagerEmail { get; set; }


        public ManagerRegisterCommand() => Id = Guid.NewGuid();
    }
}