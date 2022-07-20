using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using SharedKernel.Application.Repositories.Aggregate;
using SharedKernel.Application.Repositories.EventBus;
using SharedKernel.Application.Repositories.EventStore;
using SharedKernel.Domain;
using SharedKernel.Domain.Event;
using SharedKernel.Infrastructure.Repositories.Aggregate;
using User.Domain.User;
using User.Domain.User.Events;
using User.Domain.ValueObjects;
using Xunit;

namespace Infrastructure.Test.Repositories
{
    public class EventRepositoryTest
    {
        [Fact]
        public async Task Passing_aggregate_key_to_event_store_should_return_domain_events_for_this_aggregateAsync()
        {
            //Arrange
            var eventStoreMock = new Mock<IEventStoreRepository>();
            var publisherMock = new Mock<IPublishEvent>();
            var userId = new UserId(Guid.NewGuid());
            var login = Login.Create("example@gmail.com");
            var password = Password.Create("Test@21Tsd");
            var name = Name.Create("Test", "Test");
            var birthDate = BirthDate.Create(AppTime.Now().AddYears(-18));


            IList<DomainEvent> domainEventsList = new List<DomainEvent>()
            {
                new UserRegisteredEvent(userId, login, password, name, birthDate, 0),
                new UserRegistrationConfirmedEvent(userId,UserStatus.Activated,1),
                new UserChangedLoginEvent(userId,login,2)
            };

            eventStoreMock.Setup(x => x.Get(userId)).ReturnsAsync(domainEventsList);
            IEventRepository eventRepository = new EventRepository(eventStoreMock.Object, publisherMock.Object);
            //Act
            var events = await eventRepository.Get(userId);
            var eventsList = events.ToArray();
            //Assert
            var domainEvent = (UserRegisteredEvent)eventsList[0];
            domainEvent.Id.Should().Be(userId);
            domainEvent.Version.Should().Be(0);


        }

        [Fact]
        public async Task Saving_domain_events_should_execute_event_publisher_function_and_execute_event_store_saveAsync()
        {
            var eventStoreMock = new Mock<IEventStoreRepository>();
            var publisherMock = new Mock<IPublishEvent>();
            var userId = new UserId(Guid.NewGuid());
            var login = Login.Create("example@gmail.com");
            var password = Password.Create("Test@21Tsd");
            var name = Name.Create("Test", "Test");
            var birthDate = BirthDate.Create(AppTime.Now().AddYears(-18));

            IList<DomainEvent> domainEventsList = new List<DomainEvent>()
            {
                new UserRegisteredEvent(userId, login, password, name, birthDate, 0),
                new UserRegistrationConfirmedEvent(userId,UserStatus.Activated,1),
                new UserChangedLoginEvent(userId,login,2)
            };

            IEventRepository eventRepository = new EventRepository(eventStoreMock.Object, publisherMock.Object);

            await eventRepository.Save(domainEventsList);

            publisherMock.Verify(x => x.Publish(It.IsAny<DomainEvent>()), Times.Exactly(3));
            eventStoreMock.Verify(x => x.Save(It.IsAny<DomainEvent>()), Times.Exactly(3));

        }
    }
}
