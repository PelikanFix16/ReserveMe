using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using SharedKernel.Domain;
using SharedKernel.Domain.BusinessRule;
using SharedKernel.Domain.Event;
using User.Domain.Manager;
using User.Domain.ValueObjects;
using Xunit;

namespace Domain.Test.Aggregate
{
    public class ManagerAggregateRootTest
    {
        private readonly ManagerId _managerId = new(Guid.NewGuid());
        private readonly Email _email = new("test@example.com");

        [Fact]
        public void ShouldCreateManager()
        {
            var manager = new ManagerAggregateRoot(_managerId,_email);
            Assert.Equal(_managerId,manager.Id);
            manager.Should().NotBeNull();
            manager.Should().BeOfType<ManagerAggregateRoot>();
            manager.Id.Should().NotBeNull();
            manager.Id.Should().Be(_managerId);
            manager.ManagerEmail.Should().NotBeNull();
            manager.ManagerEmail.Should().Be(_email);
            manager.Status.Should().Be(ManagerStatus.DeActivated);
            manager.BlockedStatus.Should().Be(ManagerBlockedStatus.UnBlocked);
            manager.RegisteredDate.Should().BeCloseTo(AppTime.Now(),TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void ShouldCreateManagerWithEventManagerCreatedEvent()
        {
            var manager = new ManagerAggregateRoot(_managerId,_email);
            IList<DomainEvent> events = manager.GetUncommittedChanges().ToList();
            events.Should().NotBeNull();
            events.Should().HaveCount(1);
        }

        [Fact]
        public void ManagerShouldBeConfirmed()
        {
            var manager = new ManagerAggregateRoot(_managerId,_email);
            manager.Confirm();
            manager.Status.Should().Be(ManagerStatus.Activated);
            IList<DomainEvent> events = manager.GetUncommittedChanges().ToList();
            events.Should().NotBeNull();
            events.Should().HaveCount(2);
        }
        [Fact]
        public void ShouldThrowExceptionWhenConfirmManagerSecondTime()
        {
            var manager = new ManagerAggregateRoot(_managerId,_email);
            manager.Confirm();
            manager.Status.Should().Be(ManagerStatus.Activated);
            Action action = () => manager.Confirm();
            action.Should().Throw<BusinessRuleValidationException>()
            .WithMessage("Manager cannot be confirmed more than once");
        }


        [Fact]
        public void ShouldCannotBlockManagerWhenManagerIsNotConfirmed()
        {
            var manager = new ManagerAggregateRoot(_managerId,_email);
            Action action = () => manager.Block();
            action.Should().Throw<BusinessRuleValidationException>()
            .WithMessage("Manager cannot be modified without confirmation");
        }

        [Fact]
        public void ShouldCannotUnBlockManagerWhenManagerIsNotConfirmed()
        {
            var manager = new ManagerAggregateRoot(_managerId,_email);
            Action action = () => manager.UnBlock();
            action.Should().Throw<BusinessRuleValidationException>()
            .WithMessage("Manager cannot be modified without confirmation");
        }

        [Fact]
        public void ShouldCannotChangeEmailWhenManagerIsNotConfirmed()
        {
            var manager = new ManagerAggregateRoot(_managerId,_email);
            Action action = () => manager.ChangeEmail(_email);
            action.Should().Throw<BusinessRuleValidationException>()
            .WithMessage("Manager cannot be modified without confirmation");
        }
        [Fact]
        public void ShouldCannotChangeEmailWhenManagerIsBlocked()
        {
            var manager = new ManagerAggregateRoot(_managerId,_email);
            manager.Confirm();
            manager.Block();
            Action action = () => manager.ChangeEmail(_email);
            action.Should().Throw<BusinessRuleValidationException>()
            .WithMessage("Manager cannot be modified when blocked");
        }

        [Fact]
        public void ShouldCannotBlockManagerWhenManagerIsBlocked()
        {
            var manager = new ManagerAggregateRoot(_managerId,_email);
            manager.Confirm();
            manager.Block();
            Action action = () => manager.Block();
            action.Should().Throw<BusinessRuleValidationException>()
            .WithMessage("Manager cannot be blocked more than once");
        }

        [Fact]
        public void ShouldCannotChangeEmailWhenIsTheSame()
        {
            var manager = new ManagerAggregateRoot(_managerId,_email);
            manager.Confirm();
            Action action = () => manager.ChangeEmail(_email);
            action.Should().Throw<BusinessRuleValidationException>()
            .WithMessage("Manager cannot be modified with the same email");
        }

        [Fact]
        public void ShouldChangeEmailWhenManagerIsConfirmed()
        {
            var manager = new ManagerAggregateRoot(_managerId,_email);
            var newEmail = new Email("newEmail@example.com");
            manager.Confirm();
            manager.ChangeEmail(newEmail);
            manager.ManagerEmail.Should().Be(newEmail);
            IList<DomainEvent> events = manager.GetUncommittedChanges().ToList();
            events.Should().NotBeNull();
            events.Should().HaveCount(3);
        }
    }
}
