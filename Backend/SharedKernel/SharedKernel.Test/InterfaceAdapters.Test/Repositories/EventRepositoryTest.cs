using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Test.Mock.Events;
using Domain.Test.Mock.ValueObjects;
using FluentAssertions;
using Moq;
using SharedKernel.Domain;
using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
using SharedKernel.InterfaceAdapters.EventsFlowController;
using SharedKernel.InterfaceAdapters.Interfaces.EventsFlowController;
using SharedKernel.InterfaceAdapters.Interfaces.Repositories;
using Xunit;

namespace InterfaceAdapters.Test.Repositories
{
    public class EventRepositoryTest
    {
        [Fact]
        public async Task PassingAggregateKeyToEventRepositoryShouldReturnDomainEventsForThisAggregateAsync()
        {
            //Arrange
            var eventStoreMock = new Mock<IEventStoreRepository>();
            var publisherMock = new Mock<IEventDispatcher>();
            var testKey = new TestId(Guid.NewGuid());
            var login = new TestName("Test", "test2");
            var birthDate = new TestBirthDate(AppTime.Now().AddYears(-18));
            var testCreatedEvent = new TestCreatedEvent(testKey, login, birthDate, 0);
            var testChangedNameEvent = new TestChangedNameEvent(testKey, login, 1);
            IList<DomainEvent> domainEventList = new List<DomainEvent>()
            {
                testCreatedEvent,
                testChangedNameEvent
            };
            eventStoreMock.Setup(x => x.GetAsync(It.IsAny<AggregateKey>())).ReturnsAsync(domainEventList);
            IEventController eventRepository = new EventController(eventStoreMock.Object, publisherMock.Object);
            //Act
            var events = await eventRepository.GetAsync(testKey);
            //Assert
            var eventsList = events.ToArray();
            var domainEvent = (TestCreatedEvent)eventsList[0];
            domainEvent.Key.Should().Be(testKey);
            domainEvent.Version.Should().Be(0);
            var changedNameEvent = (TestChangedNameEvent)eventsList[1];
            changedNameEvent.Key.Should().Be(testKey);
            changedNameEvent.Version.Should().Be(1);
        }

        [Fact]
        public async Task SavingDomainEventsShouldExecuteEventStoreManagerFunctionAndExecuteEventDispatcherAsync()
        {
            //Arrange
            var eventStoreMock = new Mock<IEventStoreRepository>();
            var publisherMock = new Mock<IEventDispatcher>();
            var testKey = new TestId(Guid.NewGuid());
            var login = new TestName("Test", "test2");
            var birthDate = new TestBirthDate(AppTime.Now().AddYears(-18));
            var testCreatedEvent = new TestCreatedEvent(testKey, login, birthDate, 0);
            var testChangedNameEvent = new TestChangedNameEvent(testKey, login, 1);
            IList<DomainEvent> domainEventList = new List<DomainEvent>()
            {
                testCreatedEvent,
                testChangedNameEvent
            };
            IEventController eventRepository = new EventController(eventStoreMock.Object, publisherMock.Object);
            //Act
            await eventRepository.SaveAsync(domainEventList);
            //Assert
            publisherMock.Verify(x => x.DispatchAsync(It.IsAny<DomainEvent>()), Times.Exactly(2));
            eventStoreMock.Verify(x => x.SaveAsync(It.IsAny<DomainEvent>()), Times.Exactly(2));
        }
    }
}
