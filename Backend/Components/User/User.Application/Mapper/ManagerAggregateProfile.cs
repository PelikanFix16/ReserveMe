using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Application.Cqrs.Commands.Manager.ManagerRegister;
using User.Application.Mapper.Dto;
using User.Domain.Manager;
using User.Domain.User;
using User.Domain.ValueObjects;

namespace User.Application.Mapper
{
    public class ManagerAggregateProfile : Profile
    {
        public ManagerAggregateProfile()
        {
            CreateMap<EmailDto,Email>().ConstructUsing(x => new Email(x.Email));
            CreateMap<Guid,UserId>().ConstructUsing(x => new UserId(x));
            CreateMap<ManagerRegisterCommand,ManagerAggregateRoot>();
            CreateMap<Guid,ManagerId>().ConstructUsing(x => new ManagerId(x));
        }
    }
}