using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Test.Mock.Events;
using Domain.Test.Mock.ValueObjects;
using FluentAssertions;
using Microsoft.Extensions.Options;
using SharedKernel.Infrastructure.EventStore;
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
            var idT = new TestId(Guid.NewGuid());
            var testName = new TestName("Test", "Name");
            var testBirthDate = new TestBirthDate(DateTime.Now);
            var eventToStore = new TestCreatedEvent(idT, testName, testBirthDate, 1);
            await eventStore.SaveAsync(eventToStore);
            var events = await eventStore.GetAsync(idT);
            events.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task ShouldSaveTwoEventsToEventStoreAndReturnTwoEventsInSameOrderAsync()
        {
            var idT = new TestId(Guid.NewGuid());
            var testName = new TestName("Test", "Name");
            var testNewName = new TestName("Test", "Test2");
            var testBirthDate = new TestBirthDate(DateTime.Now);
            var eventToStore = new TestCreatedEvent(idT, testName, testBirthDate, 1);
            var eventToStore2 = new TestChangedNameEvent(idT, testNewName, 2);
            var eventStore = new MongoEventStore(_settings);
            await eventStore.SaveAsync(eventToStore);
            await eventStore.SaveAsync(eventToStore2);
            var events = await eventStore.GetAsync(idT);
            events.Count().Should().BeGreaterThan(1);
            events.First().Should().BeEquivalentTo(eventToStore);
            events.Last().Should().BeEquivalentTo(eventToStore2);
        }
    }
}
