using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using User.Application.Mapper.Dto;

namespace User.Application.Mapper
{
    public class DtoProfile : Profile
    {
        public DtoProfile()
        {
            CreateMap<string, LoginDto>().ForMember(x => x.Login, opt => opt.MapFrom(y => y));
        }
    }
}
