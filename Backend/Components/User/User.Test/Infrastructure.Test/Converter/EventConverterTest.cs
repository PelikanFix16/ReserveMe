using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using SharedKernel.Application.Interfaces.Events;
using SharedKernel.Domain;
using SharedKernel.InterfaceAdapters.Common.Converter;
using User.Domain.User;
using User.Domain.User.Events;
using User.Domain.ValueObjects;
using Xunit;

namespace Infrastructure.Test.Converter
{
    public class EventConverterTest
    {
        [Fact]
        public void ShouldConvertDomainEventToSharedEvent()
        {
            var userRegisteredEvent = new UserRegisteredEvent(
                new UserId(Guid.NewGuid()),
                new Login("login@example.com"),
                new Password("password@21@33S"),
                new Name("Name", "Surname"),
                new BirthDate(AppTime.Now().AddYears(-20)),
                1);

            IEventConverter eventConverter = new SharedEventConverter();
            var sharedEvent = eventConverter.DomainEventToShared(userRegisteredEvent);
            sharedEvent.EventName.Should().Be("UserRegisteredEvent");
            sharedEvent.AssemblyName.Should().Be("User.Domain");
        }

        [Fact]
        public void ShouldConvertSharedEventToDomainEventOnGenericPassedType()
        {
            var userRegisteredEvent = new UserRegisteredEvent(
                new UserId(Guid.NewGuid()),
                new Login("login@example.com"),
                new Password("password@21@33S"),
                new Name("Name", "Surname"),
                new BirthDate(AppTime.Now().AddYears(-20)),
                1);

            IEventConverter eventConverter = new SharedEventConverter();
            var sharedEvent = eventConverter.DomainEventToShared(userRegisteredEvent);
            var domainEvent = eventConverter.SharedEventToDomain<UserRegisteredEvent>(sharedEvent);
            domainEvent.Should().BeEquivalentTo(userRegisteredEvent);
        }
    }
}
