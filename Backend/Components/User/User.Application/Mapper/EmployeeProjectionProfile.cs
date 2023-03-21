using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Application.Mapper.Projections;
using User.Domain.Employee.Events;

namespace User.Application.Mapper
{
    public class EmployeeProjectionProfile : Profile
    {
        public EmployeeProjectionProfile() => CreateMap<EmployeeCreatedEvent,EmployeeProjection>()
            .ForMember(x => x.Id,opt => opt.MapFrom(y => y.Key.Key))
            .ForMember(x => x.Email,opt => opt.MapFrom(y => y.Email.Value))
            .ForMember(x => x.ManagerId,opt => opt.MapFrom(y => y.ManagerId.Key))
            .ForMember(x => x.Privileges,opt => opt.MapFrom(y => y.Privileges));
    }
}