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
using SharedKernel.Infrastructure.Repositories.Aggregate;
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
            eventRepositoryMock.Setup(x => x.Get(It.IsAny<AggregateKey>())).ReturnsAsync(list);
            IAggregateRepository aggregateRepository = new AggregateRepository(eventRepositoryMock.Object);
            // When
            var userAggregate = await aggregateRepository.Get<UserAggregateRoot>(_userId);
            // Then
            eventRepositoryMock.Verify(x => x.Get(_userId), Times.Once());
            userAggregate.Should().BeOfType<UserAggregateRoot>();
            userAggregate.Version.Should().Be(1);
        }

        [Fact]
        public async Task AggregateRepositoryShouldSaveAggregateToLocalDictionaryWhenPassWitchCorrectAggregateVersionAsync()
        {
            var eventRepositoryMock = new Mock<IEventRepository>();
            var user = new UserAggregateRoot(_userId, _login, _password, _name, _birthDate);
            IAggregateRepository aggregateRepository = new AggregateRepository(eventRepositoryMock.Object);

            aggregateRepository.Save(user, _userId);

            var aggregate = await aggregateRepository.Get<UserAggregateRoot>(_userId);

            eventRepositoryMock.Verify(mock => mock.Get(_userId), Times.Never());

            aggregate.Should().BeOfType<UserAggregateRoot>();
            aggregate.Version.Should().Be(1);
        }

        [Fact]
        public async Task AggregateRepositoryShouldCommitAggregateEventsToEventRepositoryAndDeleteFromLocalDirectoryAsync()
        {
            var eventRepositoryMock = new Mock<IEventRepository>();
            var user = new UserAggregateRoot(_userId, _login, _password, _name, _birthDate);
            IAggregateRepository aggregateRepository = new AggregateRepository(eventRepositoryMock.Object);

            aggregateRepository.Save(user, _userId);

            var result = await aggregateRepository.Commit();

            //checking returns true
            result.Should().BeTrue();
            //checking we execute save function with this parameters
            eventRepositoryMock.Verify(mock => mock.Save(user.GetUncommittedChanges()), Times.Once());
            // checking did dictionary is empty

            var userFromEvents = await aggregateRepository.Get<UserAggregateRoot>(_userId);
            // if this execute we know that dictionary is empty
            eventRepositoryMock.Verify(mock => mock.Get(_userId), Times.Once());
        }
    }
}
