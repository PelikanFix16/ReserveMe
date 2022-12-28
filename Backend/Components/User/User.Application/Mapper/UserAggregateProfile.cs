using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using User.Application.Cqrs.Commands.UserRegister;
using User.Application.Mapper.Dto;
using User.Domain.User;
using User.Domain.ValueObjects;

namespace User.Application.Mapper
{
    public class UserAggregateProfile : Profile
    {
        public UserAggregateProfile()
        {
            CreateMap<NameDto, Name>().ConstructUsing(x => new Name(x.FirstName, x.LastName));
            CreateMap<LoginDto, Email>().ConstructUsing(x => new Email(x.Login));
            CreateMap<PasswordDto, Password>().ConstructUsing(x => new Password(x.Password));
            CreateMap<BirthDateDto, BirthDate>().ConstructUsing(x => new BirthDate(x.BirthDate));
            CreateMap<Name, NameDto>();
            CreateMap<UserRegisterCommand, UserAggregateRoot>().AfterMap((src, dest, context) =>
            {
                var userId = context.Mapper.Map<UserId>(src.Id);
                var name = context.Mapper.Map<Name>(src.Name);
                var login = context.Mapper.Map<Email>(src.Login);
                var password = context.Mapper.Map<Password>(src.Password);
                var birthDate = context.Mapper.Map<BirthDate>(src.BirthDate);
                dest = new UserAggregateRoot(userId, login, password, name, birthDate);
            });

            CreateMap<Guid, UserId>().ConstructUsing(x => new UserId(x));
        }
    }
}
