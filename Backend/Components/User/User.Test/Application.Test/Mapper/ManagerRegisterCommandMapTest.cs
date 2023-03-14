using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Application.Cqrs.Commands.Manager.ManagerRegister;
using User.Application.Mapper;
using User.Application.Mapper.Dto;
using User.Domain.Manager;
using Xunit;

namespace Application.Test.Mapper
{
    public class ManagerRegisterCommandMapTest
    {
        [Fact]
        public void ShouldMapFromManagerRegisterCommandToManagerAggregateRoot()
        {
            var emailDto = new EmailDto { Email = "test@example.com" };
            var userGuid = Guid.NewGuid();
            var managerRegisterCommand = new ManagerRegisterCommand()
            {
                ManagerEmail = emailDto,
                UserId = userGuid
            };

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ManagerAggregateProfile>());
            var mapper = configuration.CreateMapper();
            var managerAggregateRoot = mapper.Map<ManagerRegisterCommand,ManagerAggregateRoot>(managerRegisterCommand);
            var eventList = managerAggregateRoot.GetUncommittedChanges().ToList();

            eventList.Count.Should().Be(1);
            managerAggregateRoot.UserId!.Key.Should().Be(userGuid);
            managerAggregateRoot.ManagerEmail.Value.Should().Be(emailDto.Email);

        }
    }
}