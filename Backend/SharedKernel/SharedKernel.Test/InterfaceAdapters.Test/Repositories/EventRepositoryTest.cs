using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Test.Mock.Events;
using Domain.Test.Mock.ValueObjects;
using FluentAssertions;
using Moq;
using SharedKernel.Application.Common.Event;
using SharedKernel.Domain;
using SharedKernel.Domain.Event;
using SharedKernel.InterfaceAdapters.Common.Converter;
using SharedKernel.InterfaceAdapters.Dto;
using SharedKernel.InterfaceAdapters.Interfaces.Repositories;
using SharedKernel.SharedKernel.InterfaceAdapters.Interfaces.EventBus;
using SharedKernel.SharedKernel.InterfaceAdapters.Interfaces.EventStore;
using SharedKernel.SharedKernel.InterfaceAdapters.Repositories.Aggregate;
using Xunit;

namespace InterfaceAdapters.Test.Repositories
{
    public class EventRepositoryTest
    {
        [Fact]
        public async Task PassingEventKeyToEventRepositoryShouldReturnDomainEventsForThisAggregateAsync()
        {
            //Arrange
            var eventStoreMock = new Mock<IEventStoreRepository>();
            var publisherMock = new Mock<IPublishEvent>();
            var testId = new TestId(Guid.NewGuid());
            var login = new TestName("Test", "test2");
            var birthDate = new TestBirthDate(AppTime.Now().AddYears(-18));
            var eventKey = new EventKey()
            {
                Key = testId.Key
            };
            var testCreatedEvent = new TestCreatedEvent(testId, login, birthDate, 0);
            var testChangedNameEvent = new TestChangedNameEvent(testId, login, 1);
            var convertedTestCreatedEvent = EventConverter.DomainToStoreEvent(testCreatedEvent);
            var convertedTestChangedNameEvent = EventConverter.DomainToStoreEvent(testChangedNameEvent);

            IList<StoreEvent> domainEventsList = new List<StoreEvent>()
            {
                convertedTestCreatedEvent,
                convertedTestChangedNameEvent
            };

            eventStoreMock.Setup(x => x.GetAsync(It.IsAny<EventKey>())).ReturnsAsync(domainEventsList);
            IEventRepository eventRepository = new EventRepository(eventStoreMock.Object, publisherMock.Object);
            //Act
            var events = await eventRepository.GetAsync(testId);
            var eventsList = events.ToArray();
            //Assert
            var domainEvent = (TestCreatedEvent)eventsList[0];
            domainEvent.Key.Should().Be(testId);
            domainEvent.Version.Should().Be(0);
            var changedNameEvent = (TestChangedNameEvent)eventsList[1];
            changedNameEvent.Key.Should().Be(testId);
            changedNameEvent.Version.Should().Be(1);
        }

        [Fact]
        public async Task SavingDomainEventsShouldExecuteEventPublisherFunctionAndExecuteEventStoreSaveAsync()
        {
            var eventStoreMock = new Mock<IEventStoreRepository>();
            var publisherMock = new Mock<IPublishEvent>();
            var testId = new TestId(Guid.NewGuid());
            var login = new TestName("Test", "test2");
            var birthDate = new TestBirthDate(AppTime.Now().AddYears(-18));

            IList<DomainEvent> domainEventsList = new List<DomainEvent>()
            {
                new TestCreatedEvent(testId, login, birthDate, 0),
                new TestChangedNameEvent(testId, login, 1)
            };
            IEventRepository eventRepository = new EventRepository(eventStoreMock.Object, publisherMock.Object);

            await eventRepository.SaveAsync(domainEventsList);

            publisherMock.Verify(x => x.PublishAsync(It.IsAny<SharedEvent>()), Times.Exactly(2));
            eventStoreMock.Verify(x => x.SaveAsync(It.IsAny<StoreEvent>()), Times.Exactly(2));
        }
    }
}
