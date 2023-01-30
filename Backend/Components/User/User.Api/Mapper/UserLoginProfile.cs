using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using User.Api.Dto.User;
using User.Application.Cqrs.Queries.User.UserLogin;
using User.Application.Mapper.Dto;

namespace User.Api.Mapper
{
    public class UserLoginProfile : Profile
    {
        public UserLoginProfile()
        {
            CreateMap<UserLoginRequest,UserLoginQuery>()
                .ForMember(dest => dest.Login,opt => opt.MapFrom(src => new EmailDto { Email = src.Email }))
                .ForMember(dest => dest.Password,opt => opt.MapFrom(src => new PasswordDto { Password = src.Password }));
        }
    }
}
