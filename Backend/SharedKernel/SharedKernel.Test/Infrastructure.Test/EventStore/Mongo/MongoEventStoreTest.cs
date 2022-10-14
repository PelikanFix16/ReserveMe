using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Options;
using SharedKernel.Infrastructure.Repositories.EventStore.Mongo;
using SharedKernel.InterfaceAdapters.Dto;
using Xunit;

namespace Infrastructure.Test.EventStore.Mongo
{
    public class MongoEventStoreTest
    {
        private readonly IOptions<MongoSettings> _settings = MongoSettingsLocalDatabase.GetConfig();

        [Fact]
        public async Task ShouldSaveDomainEventToMongoAsync()
        {
            var eventStore = new MongoEventStore(_settings);
            var idT = Guid.NewGuid();
            var eventToStore = new StoreEvent()
            {
                EventId = idT,
                EventName = "Test",
                EventData = "Test",
                EventDate = DateTimeOffset.Now,
                Version = 0
            };
            await eventStore.SaveAsync(eventToStore);
            var eventKey = new EventKey() { Key = idT };
            var events = await eventStore.GetAsync(eventKey);
            events.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task ShouldSaveTwoEventsToEventStoreAndReturnTwoEventsInSameOrderAsync()
        {
            var idT = Guid.NewGuid();
            var eventKey = new EventKey() { Key = idT };
            var eventToStore = new StoreEvent()
            {
                EventId = idT,
                EventName = "Test",
                EventData = "Test",
                EventDate = DateTimeOffset.Now,
                Version = 1
            };
            var eventToStoreSec = new StoreEvent()
            {
                EventId = idT,
                EventName = "Test2",
                EventData = "Test2",
                EventDate = DateTimeOffset.Now,
                Version = 2
            };
            var eventStore = new MongoEventStore(_settings);
            await eventStore.SaveAsync(eventToStore);
            await eventStore.SaveAsync(eventToStoreSec);
            var events = await eventStore.GetAsync(eventKey);
            events.Count().Should().BeGreaterThan(1);
            events.First().EventName.Should().Be("Test");
            events.Last().EventName.Should().Be("Test2");
        }
    }
}
