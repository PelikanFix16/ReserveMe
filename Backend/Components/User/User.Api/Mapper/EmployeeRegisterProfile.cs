using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Api.Dto.Employee;
using User.Application.Cqrs.Commands.Employee.EmployeeRegister;
using User.Application.Mapper.Dto;

namespace User.Api.Mapper
{
    public class EmployeeRegisterProfile : Profile
    {
        public EmployeeRegisterProfile() => CreateMap<EmployeeRegisterRequest,EmployeeRegisterCommand>()
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(src => new EmailDto { Email = src.Email }))
                .ForMember(
                    dest => dest.ManagerId,
                    opt => opt.MapFrom(src => src.ManagerId))
                .ForMember(
                    dest => dest.Privileges,
                    opt => opt.MapFrom(src => src.Privileges)
                );
    }
}