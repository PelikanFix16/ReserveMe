using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Application.Cqrs.Commands.Employee.EmployeeRegister;
using User.Application.Mapper;
using User.Application.Mapper.Dto;
using User.Domain.Employee;
using Xunit;

namespace Application.Test.Mapper
{
    public class EmployeeRegisterCommandMapTest
    {
        [Fact]
        public void ShouldMapFromEmployeeRegisterCommandToEmployeeAggregateRoot()
        {
            var emailDto = new EmailDto { Email = "test@example.com" };
            var managerId = Guid.NewGuid();
            var privileges = EmployeePrivileges.None;

            var employeeCommand = new EmployeeRegisterCommand()
            {
                ManagerId = managerId,
                Privileges = privileges,
                Email = emailDto
            };

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<EmployeeAggregateProfile>());
            var mapper = configuration.CreateMapper();
            var employeeAggregateRoot = mapper.Map<EmployeeRegisterCommand,EmployeeAggregateRoot>(employeeCommand);
            var eventList = employeeAggregateRoot.GetUncommittedChanges().ToList();
            eventList.Count.Should().Be(1);
            employeeAggregateRoot.ManagerId!.Key.Should().Be(managerId);
            employeeAggregateRoot.Email.Value.Should().Be(emailDto.Email);
        }
    }
}