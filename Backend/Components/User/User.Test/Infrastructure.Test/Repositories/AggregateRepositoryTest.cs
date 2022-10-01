using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using SharedKernel.Application.Repositories.Aggregate;
using SharedKernel.Domain;
using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
using SharedKernel.InterfaceAdapters.Interfaces.Repositories;
using SharedKernel.SharedKernel.InterfaceAdapters.Repositories.Aggregate;
using User.Domain.User;
using User.Domain.User.Events;
using User.Domain.ValueObjects;
using Xunit;

namespace Infrastructure.Test.Repositories
{
    public class AggregateRepositoryTest
    {
        private readonly UserId _userId = new(Guid.NewGuid());
        private readonly Login _login = Login.Create("test@gmail.com");
        private readonly Password _password = Password.Create("Test@21Tsd");
        private readonly Name _name = Name.Create("Test", "Test");
        private readonly BirthDate _birthDate = BirthDate.Create(AppTime.Now().AddYears(-18));

        [Fact]
        public async Task AggregateRepositoryShouldReturnAggregateWithOneEventRegisteredFromEventRepositoryWhenCallGetMethodAsync()
        {
            // Given
            var eventRepositoryMock = new Mock<IEventRepository>();
            var userRegisteredEvent = new UserRegisteredEvent(
                _userId,
                _login,
                _password,
                _name,
                _birthDate,
                0
            );
            IList<DomainEvent> list = new List<DomainEvent>
            {
                userRegisteredEvent
            };
            eventRepositoryMock.Setup(x => x.GetAsync(It.IsAny<AggregateKey>())).ReturnsAsync(list);
            IAggregateRepository aggregateRepository = new AggregateRepository(eventRepositoryMock.Object);
            // When
            var userAggregate = await aggregateRepository.GetAsync<UserAggregateRoot>(_userId);
            // Then
            eventRepositoryMock.Verify(x => x.GetAsync(_userId), Times.Once());
            userAggregate.Value.Should().BeOfType<UserAggregateRoot>();
            userAggregate.Value.Version.Should().Be(1);
        }

        [Fact]
        public async Task AggregateRepositoryShouldSaveAggregateToLocalDictionaryWhenPassWitchCorrectAggregateVersionAsync()
        {
            var eventRepositoryMock = new Mock<IEventRepository>();
            var user = new UserAggregateRoot(_userId, _login, _password, _name, _birthDate);
            IAggregateRepository aggregateRepository = new AggregateRepository(eventRepositoryMock.Object);

            aggregateRepository.Save(user, _userId);

            var aggregate = await aggregateRepository.GetAsync<UserAggregateRoot>(_userId);

            eventRepositoryMock.Verify(mock => mock.GetAsync(_userId), Times.Never());

            aggregate.Value.Should().BeOfType<UserAggregateRoot>();
            aggregate.Value.Version.Should().Be(1);
        }

        [Fact]
        public async Task AggregateRepositoryShouldCommitAggregateEventsToEventRepositoryAndDeleteFromLocalDirectoryAsync()
        {
            var eventRepositoryMock = new Mock<IEventRepository>();
            var user = new UserAggregateRoot(_userId, _login, _password, _name, _birthDate);
            IAggregateRepository aggregateRepository = new AggregateRepository(eventRepositoryMock.Object);

            aggregateRepository.Save(user, _userId);

            var result = await aggregateRepository.CommitAsync();

            //checking returns true
            result.IsSuccess.Should().BeTrue();
            //checking we execute save function with this parameters
            eventRepositoryMock.Verify(mock => mock.SaveAsync(user.GetUncommittedChanges()), Times.Once());
            // checking did dictionary is empty

            var userFromEvents = await aggregateRepository.GetAsync<UserAggregateRoot>(_userId);
            // if this execute we know that dictionary is empty
            eventRepositoryMock.Verify(mock => mock.GetAsync(_userId), Times.Once());
        }
    }
}
