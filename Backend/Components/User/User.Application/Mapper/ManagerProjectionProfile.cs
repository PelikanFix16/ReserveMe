using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Application.Mapper.Projections;
using User.Domain.Manager.Events;

namespace User.Application.Mapper
{
    public class ManagerProjectionProfile : Profile
    {
        public ManagerProjectionProfile() => CreateMap<ManagerCreatedEvent,ManagerProjection>()
                .ForMember(x => x.Id,opt => opt.MapFrom(y => y.Key.Key))
                .ForMember(x => x.Email,opt => opt.MapFrom(y => y.Email.Value))
                .ForMember(x => x.UserId,opt => opt.MapFrom(y => y.UserId.Key));
    }
}