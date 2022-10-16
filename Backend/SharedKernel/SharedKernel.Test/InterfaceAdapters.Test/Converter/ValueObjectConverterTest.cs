using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Test.Mock.Events;
using Domain.Test.Mock.ValueObjects;
using FluentAssertions;
using SharedKernel.Domain;
using SharedKernel.InterfaceAdapters.Common.Converter;
using Xunit;

namespace InterfaceAdapters.Test.Converter
{
    public class ValueObjectConverterTest
    {
        [Fact]

        public void ShouldConvertStoreEventToDomainEvent()
        {
            var testId = new TestId(Guid.NewGuid());
            var login = new TestName("Test", "test2");
            var birthDate = new TestBirthDate(AppTime.Now().AddYears(-18));
            var testCreatedEvent = new TestCreatedEvent(testId, login, birthDate, 0);
            var convertedTestCreatedEvent = EventConverter.DomainToStoreEvent(testCreatedEvent);
            var domainEvent = EventConverter.StoreToDomainEvent(convertedTestCreatedEvent) as TestCreatedEvent;
            domainEvent.Should().NotBeNull();
            domainEvent!.Name.FirstName.Should().Be("Test");
            domainEvent!.Name.LastName.Should().Be("test2");

        }
    }
}
