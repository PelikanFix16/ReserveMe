using FluentAssertions;
using SharedKernel.Domain.BusinessRule;
using SharedKernel.Domain.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Domain.Employee;
using User.Domain.Manager;
using User.Domain.ValueObjects;
using Xunit;

namespace Domain.Test.Aggregate
{
    public class EmployeeAggregateRootTest
    {

        private readonly EmployeeId _employeeId = new(Guid.NewGuid());

        private readonly ManagerId _managerId = new(Guid.NewGuid());

        private readonly Email _email = new("test@example.com");


        [Fact]
        public void ShouldCreateEmployee()
        {
            var employee = new EmployeeAggregateRoot(
                _employeeId,
                _managerId,
                _email,
                EmployeePrivileges.None);

            employee.ManagerId.Should().Be(_managerId);
            employee.Should().NotBeNull();
            employee.Should().BeOfType<EmployeeAggregateRoot>();
            employee.Id.Should().Be(_employeeId);
            employee.Privileges.Should().Be(EmployeePrivileges.None);
            employee.BlockStatus.Should().Be(EmployeeBlockStatus.UnBlocked);
            employee.Email.Should().Be(_email);
        }

        [Fact]
        public void ShouldCreateEmployeeWithEventEmployeeCreatedEvent()
        {
            var employee = new EmployeeAggregateRoot(
                _employeeId,
                _managerId,
                _email,
                EmployeePrivileges.None);
            IList<DomainEvent> events = employee.GetUncommittedChanges().ToList();
            events.Should().NotBeNull();
            events.Should().HaveCount(1);
        }

        [Fact]
        public void ShouldChangePrivilegesEmployee()
        {
            var employee = new EmployeeAggregateRoot(_employeeId,_managerId,_email,EmployeePrivileges.None);
            employee.ChangePrivileges(EmployeePrivileges.AcceptReservation);
            employee.Privileges.Should().Be(EmployeePrivileges.AcceptReservation);
            var events = employee.GetUncommittedChanges().ToList();
            events.Should().NotBeNull();
            events.Should().HaveCount(2);
        }

        [Fact]
        public void ShouldBlockEmployee()
        {
            var employee = new EmployeeAggregateRoot(_employeeId,_managerId,_email,EmployeePrivileges.None);
            employee.Block();
            var events = employee.GetUncommittedChanges().ToList();
            events.Should().NotBeNull();
            events.Should().HaveCount(2);
            employee.BlockStatus.Should().Be(EmployeeBlockStatus.Blocked);
        }

        [Fact]
        public void ShouldUnBlockEmployeeIfIsBlocked()
        {
            var employee = new EmployeeAggregateRoot(_employeeId,_managerId,_email,EmployeePrivileges.None);
            employee.Block();
            employee.UnBlock();
            var events = employee.GetUncommittedChanges().ToList();
            events.Should().NotBeNull();
            events.Should().HaveCount(3);
            employee.BlockStatus.Should().Be(EmployeeBlockStatus.UnBlocked);
        }

        [Fact]
        public void ShouldConfirmEmployee()
        {
            var employee = new EmployeeAggregateRoot(_employeeId,_managerId,_email,EmployeePrivileges.None);
            employee.Confirm();
            var events = employee.GetUncommittedChanges().ToList();
            events.Should().NotBeNull();
            events.Should().HaveCount(2);
            employee.Status.Should().Be(EmployeeStatus.Activated);

        }

        [Fact]
        public void ShouldChangePasswordForEmployee()
        {
            var employee = new EmployeeAggregateRoot(_employeeId,_managerId,_email,EmployeePrivileges.None);
            var password = new Password("teS2sf@!");
            employee.ChangePassword(password);
            var events = employee.GetUncommittedChanges().ToList();
            events.Should().NotBeNull();
            events.Should().HaveCount(2);
            employee.Password.Should().Be(password);
        }

        [Fact]
        public void EmployeeCannotBeBlockedMoreThanOnceRule()
        {
            var employee = new EmployeeAggregateRoot(_employeeId,_managerId,_email,EmployeePrivileges.None);
            employee.Block();
            Action act = () => employee.Block();
            act.Should().Throw<BusinessRuleValidationException>()
            .WithMessage("Employee cannot be blocked more than once");
        }

        [Fact]
        public void EmployeeCannotBeConfirmedMoreThanOnceRule()
        {
            var employee = new EmployeeAggregateRoot(_employeeId,_managerId,_email,EmployeePrivileges.None);
            employee.Confirm();
            Action act = () => employee.Confirm();
            act.Should().Throw<BusinessRuleValidationException>()
            .WithMessage("Employee cannot be confirmed more than once");
        }

        [Fact]
        public void EmployeeCannotBeUnblockedMoreThanOnceRule()
        {
            var employee = new EmployeeAggregateRoot(_employeeId,_managerId,_email,EmployeePrivileges.None);
            Action act = () => employee.UnBlock();
            act.Should().Throw<BusinessRuleValidationException>()
            .WithMessage("Employee cannot be unblocked more than once");
        }

        [Fact]
        public void EmployeeCannotChangeWhenBlocked()
        {
            var employee = new EmployeeAggregateRoot(_employeeId,_managerId,_email,EmployeePrivileges.None);
            employee.Block();
            Action act = () => employee.ChangePrivileges(EmployeePrivileges.AcceptReservation);
            act.Should().Throw<BusinessRuleValidationException>()
            .WithMessage("Employee cannot change when blocked");
        }
    }
}