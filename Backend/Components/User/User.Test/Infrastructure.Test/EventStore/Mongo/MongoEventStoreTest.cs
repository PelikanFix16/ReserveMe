using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using SharedKernel.Domain;
using SharedKernel.Domain.Event;
using SharedKernel.Infrastructure.Repositories.EventStore.Mongo;
using User.Domain.User;
using User.Domain.User.Events;
using User.Domain.ValueObjects;
using Xunit;

namespace Infrastructure.Test.EventStore.Mongo
{
    public class MongoEventStoreTest
    {
        private readonly MongoSettings _settings = MongoSettingsLocalDatabase.GetConfig();

        [Fact]
        public async Task Should_save_domain_event_to_mongoAsync()
        {
            var eventStore = new MongoEventStore(_settings);

            var userId = new UserId(Guid.NewGuid());
            var login = Login.Create("example@gmail.com");
            var password = Password.Create("Test@21Tsd");
            var name = Name.Create("Test", "Test");
            var birthDate = BirthDate.Create(AppTime.Now().AddYears(-18));

            var registeredEvent = new UserRegisteredEvent(userId, login, password, name, birthDate, 0);

            await eventStore.Save(registeredEvent);

            var events = await eventStore.Get(userId);

            events.Count().Should().BeGreaterThan(0);

        }

        [Fact]
        public async Task Should_rebuild_aggregate_from_event_store_eventsAsync()
        {
            var userId = new UserId(Guid.NewGuid());
            var login = Login.Create("example@gmail.com");
            var password = Password.Create("Test@21Tsd");
            var name = Name.Create("Test", "Test");
            var birthDate = BirthDate.Create(AppTime.Now().AddYears(-18));
            var newLogin = Login.Create("test@example.com");

            IList<DomainEvent> domainEventsList = new List<DomainEvent>()
            {
                new UserRegisteredEvent(userId, login, password, name, birthDate, 0),
                new UserRegistrationConfirmedEvent(userId,UserStatus.Activated,1),
                new UserChangedLoginEvent(userId,newLogin,2)
            };

            var eventStore = new MongoEventStore(_settings);
            await eventStore.Save(domainEventsList[0]);
            await eventStore.Save(domainEventsList[1]);
            await eventStore.Save(domainEventsList[2]);

            var eventStoreEventsReturn = await eventStore.Get(userId);
            var eventStoreEventsReturnArray = eventStoreEventsReturn.ToArray();
            var user = new UserAggregateRoot();

            user.LoadFromHistory(eventStoreEventsReturn);

            user!.Login!.Value.Should().Be(newLogin.Value);
            eventStoreEventsReturnArray[0].Version.Should().Be(0);
            eventStoreEventsReturnArray[1].Version.Should().Be(1);
            eventStoreEventsReturnArray[2].Version.Should().Be(2);
            user!.Status.Should().Be(UserStatus.Activated);




        }



    }
}
