using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Application.Mapper.Dto;

namespace User.Application.Cqrs.Commands.Manager.ManagerRegister
{
    public class ManagerRegisterCommand
    {
        public Guid Id { get; set; }
        public EmailDto ManagerEmail { get; set; }

        public ManagerRegisterCommand() => Id = Guid.NewGuid();
    }
}