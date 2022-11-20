using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Application.Mapper.Dto;
using AutoMapper;
using Xunit;
using User.Application.Mapper;
using User.Domain.User;
using FluentAssertions;
using User.Domain.User.Factory;
using SharedKernel.Domain;
using User.Application.Cqrs.Commands.UserRegister;

namespace Application.Test.Mapper
{
    public class UserRegisterCommandMapTest
    {
        [Fact]
        public void ShouldMapFromUserRegisterCommandToUserAggregateRoot()
        {
            var nameDto = new NameDto { FirstName = "Jon", LastName = "Doe" };
            var loginDto = new LoginDto { Login = "jondoe@example.com" };
            var passwordDto = new PasswordDto { Password = "testT21ssa!" };
            var birthDateDto = new BirthDateDto { BirthDate = new DateTime(1980, 1, 1) };
            var userRegisterCommand = new UserRegisterCommand()
            {
                Name = nameDto,
                Login = loginDto,
                Password = passwordDto,
                BirthDate = birthDateDto
            };

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<UserAggregateProfile>());
            var mapper = configuration.CreateMapper();
            var userAggregateRoot = mapper.Map<UserRegisterCommand, UserAggregateRoot>(userRegisterCommand);
            var eventsList = userAggregateRoot.GetUncommittedChanges().ToList();
            userAggregateRoot!.Login!.Value.Should().Be(loginDto.Login);
            userAggregateRoot!.Password!.Value.Should().Be(passwordDto.Password);
            userAggregateRoot!.Name!.FirstName.Should().Be(nameDto.FirstName);
            userAggregateRoot!.Name!.LastName.Should().Be(nameDto.LastName);
            userAggregateRoot!.BirthDate!.Value.Should().Be(birthDateDto.BirthDate);
            eventsList.Count.Should().Be(1);
        }
    }
}
