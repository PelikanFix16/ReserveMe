using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using User.Application.Mapper.Projections;
using User.Domain.User.Events;

namespace User.Application.Mapper
{
    public class UserProjectionProfile : Profile
    {
        public UserProjectionProfile()
        {
            CreateMap<UserRegisteredEvent, UserProjection>()
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.Key.Key))
                .ForMember(x => x.Email, opt => opt.MapFrom(y => y.Login.Value))
                .ForMember(x => x.Password, opt => opt.MapFrom(y => y.Password.Value))
                .ForMember(x => x.Name, opt => opt.MapFrom(y => y.Name.FirstName))
                .ForMember(x => x.Surname, opt => opt.MapFrom(y => y.Name.LastName));
        }
    }
}
