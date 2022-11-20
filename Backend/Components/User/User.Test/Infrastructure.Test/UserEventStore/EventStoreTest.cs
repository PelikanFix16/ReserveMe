using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using SharedKernel.Application.Interfaces.Repositories;
using SharedKernel.Domain;
using SharedKernel.Infrastructure.EventStore;
using SharedKernel.InterfaceAdapters.EventsFlowController;
using SharedKernel.InterfaceAdapters.Interfaces.EventsFlowController;
using SharedKernel.InterfaceAdapters.Interfaces.Repositories;
using SharedKernel.InterfaceAdapters.Repositories;
using User.Domain.User;
using User.Domain.User.Events;
using User.Domain.ValueObjects;
using Xunit;

namespace Infrastructure.Test.UserEventStore
{
    public class EventStoreTest
    {
        private readonly IOptions<MongoSettings> _settings = MongoSettingsLocalDatabase.GetConfig();
        private readonly IEventStoreRepository _eventStore;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IEventController _eventController;
        private readonly IAggregateRepository _aggregateRepository;
        private readonly UserId _userId;
        private readonly Login _login;
        private readonly Password _password;
        private readonly Name _name;
        private readonly BirthDate _birthDate;

        public EventStoreTest()
        {
            _eventStore = new MongoEventStore(_settings);
            _eventDispatcher = new Mock<IEventDispatcher>().Object;
            _eventController = new EventController(_eventStore, _eventDispatcher);
            _aggregateRepository = new AggregateRepository(_eventController);
            _userId = new UserId(Guid.NewGuid());
            _login = new Login("test@example.com");
            _password = new Password("fewd@12@fFdf2a");
            _name = new Name("Test", "Name");
            _birthDate = new BirthDate(AppTime.Now().AddYears(-20));
        }

        [Fact]
        public async Task ShouldSaveUserRegisteredEventToEventStoreUsingEventRepositoryAsync()
        {
            var userAggregateRoot = new UserAggregateRoot(_userId, _login, _password, _name, _birthDate);
            _aggregateRepository.Save(userAggregateRoot, _userId);
            await _aggregateRepository.CommitAsync();
            var aggregate = await _aggregateRepository.GetAsync<UserAggregateRoot>(_userId);
            aggregate.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldVerifyUserEventAsync()
        {
            var userAggregateRoot = new UserAggregateRoot(_userId, _login, _password, _name, _birthDate);
            _aggregateRepository.Save(userAggregateRoot, _userId);
            await _aggregateRepository.CommitAsync();
            var aggregateUser = (await _aggregateRepository.GetAsync<UserAggregateRoot>(_userId)).Value;
            aggregateUser.Confirm();
            _aggregateRepository.Save(aggregateUser, _userId);
            await _aggregateRepository.CommitAsync();
            var aggregate = await _aggregateRepository.GetAsync<UserAggregateRoot>(_userId);
            aggregate.IsSuccess.Should().BeTrue();
            aggregate.Value.Status.Should().Be(UserStatus.Activated);
            aggregate.Value.Version.Should().Be(2);
        }
    }
}
