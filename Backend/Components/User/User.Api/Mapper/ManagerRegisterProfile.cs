using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Api.Dto.Manager;
using User.Application.Cqrs.Commands.Manager.ManagerRegister;
using User.Application.Mapper.Dto;

namespace User.Api.Mapper
{
    public class ManagerRegisterProfile : Profile
    {
        public ManagerRegisterProfile() => CreateMap<ManagerRegisterRequest,ManagerRegisterCommand>()
                .ForMember(
                    dest => dest.ManagerEmail,
                    opt => opt.MapFrom(src => new EmailDto { Email = src.Email })
                )
                .ForMember(
                    dest => dest.UserId,
                    opt => opt.MapFrom(src => src.UserId)
                );
    }
}