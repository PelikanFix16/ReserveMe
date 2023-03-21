using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Application.Cqrs.Commands.Employee.EmployeeRegister;
using User.Application.Mapper.Dto;
using User.Domain.Employee;
using User.Domain.Manager;
using User.Domain.ValueObjects;

namespace User.Application.Mapper
{
    public class EmployeeAggregateProfile : Profile
    {

        public EmployeeAggregateProfile()
        {
            CreateMap<EmployeeRegisterCommand,EmployeeAggregateRoot>();
            CreateMap<EmailDto,Email>().ConstructUsing(x => new Email(x.Email));
            CreateMap<Guid,ManagerId>().ConstructUsing(x => new ManagerId(x));
            CreateMap<Guid,EmployeeId>().ConstructUsing(x => new EmployeeId(x));
        }
    }
}