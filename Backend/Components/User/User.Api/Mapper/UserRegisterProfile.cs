using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using User.Api.Dto.User;
using User.Application.Cqrs.Commands.User.UserRegister;

namespace User.Api.Mapper
{
    public class UserRegisterProfile : Profile
    {
        public UserRegisterProfile()
        {
            CreateMap<UserRegisterRequest,UserRegisterCommand>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => new Application.Mapper.Dto.NameDto { FirstName = src.FirstName,LastName = src.LastName }))
                .ForMember(
                    dest => dest.Login,
                    opt => opt.MapFrom(src => new Application.Mapper.Dto.LoginDto { Login = src.Email }))
                .ForMember(
                    dest => dest.Password,
                    opt => opt.MapFrom(src => new Application.Mapper.Dto.PasswordDto { Password = src.Password }))
                .ForMember(
                    dest => dest.BirthDate,
                    opt => opt.MapFrom(src => new Application.Mapper.Dto.BirthDateDto { BirthDate = src.DateOfBirth }));
        }
    }
}
