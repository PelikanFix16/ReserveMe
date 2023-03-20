using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Application.Mapper.Dto;
using User.Domain.Employee;

namespace User.Application.Cqrs.Commands.Employee.EmployeeRegister
{
    public class EmployeeRegisterCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public Guid ManagerId { get; set; }
        public EmailDto Email { get; set; }
        public EmployeePrivileges Privileges { get; set; }

        public EmployeeRegisterCommand() => Id = Guid.NewGuid();
    }
}