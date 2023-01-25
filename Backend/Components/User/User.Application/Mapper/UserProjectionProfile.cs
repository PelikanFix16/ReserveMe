using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using User.Application.Cqrs.Queries.User.UserLogin;
using User.Application.Mapper.Dto;
using User.Application.Mapper.Projections;
using User.Domain.User.Events;

namespace User.Application.Mapper
{
    public class UserProjectionProfile : Profile
    {
        public UserProjectionProfile()
        {
            CreateMap<UserRegisteredEvent,UserProjection>()
                .ForMember(x => x.Id,opt => opt.MapFrom(y => y.Key.Key))
                .ForMember(x => x.Email,opt => opt.MapFrom(y => y.Login.Value))
                .ForMember(x => x.Password,opt => opt.MapFrom(y => y.Password.Value))
                .ForMember(x => x.Name,opt => opt.MapFrom(y => y.Name.FirstName))
                .ForMember(x => x.Surname,opt => opt.MapFrom(y => y.Name.LastName));

            CreateMap<UserProjection,UserLoginDto>()
                .ForMember(x => x.Id,opt => opt.MapFrom(y => y.Id))
                .ForMember(x => x.Login,opt => opt.MapFrom(y => y.Email))
                .ForMember(x => x.Name,opt => opt.MapFrom(y => new NameDto { FirstName = y.Name,LastName = y.Surname }));
        }
    }
}
