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
        private UserId userId = new UserId(Guid.NewGuid());
        private Login login = Login.Create("test@gmail.com");
        private Password password = Password.Create("Test@21Tsd");
        private Name name = Name.Create("Test", "Test");
        private BirthDate birthDate = BirthDate.Create(AppTime.Now().AddYears(-18));

        [Fact]
        public async Task Aggregate_Repository_should_return_aggregate_with_one_event_registered_from_event_repository_when_call_get_method()
        {
            // Given
            Mock<IEventRepository> eventRepositoryMock = new Mock<IEventRepository>();
            UserRegisteredEvent userRegisteredEvent = new UserRegisteredEvent(
                userId,
                login,
                password,
                name,
                birthDate,
                0
            );
            IList<DomainEvent> list = new List<DomainEvent>();
            list.Add(userRegisteredEvent);
            eventRepositoryMock.Setup(x => x.Get(It.IsAny<AggregateKey>())).ReturnsAsync(list);
            IAggregateRepository _aggregateRepository = new AggregateRepository(eventRepositoryMock.Object);
            // When
            var userAggregate = await _aggregateRepository.Get<UserAggregateRoot>(userId);
            // Then
            eventRepositoryMock.Verify(x => x.Get(userId), Times.Once());
            userAggregate.Should().BeOfType<UserAggregateRoot>();
            userAggregate.Version.Should().Be(1);

        }

        [Fact]
        public async Task Aggregate_repository_should_save_aggregate_to_local_dictionary_when_pass_witch_correct_aggregate_version()
        {
            Mock<IEventRepository> eventRepositoryMock = new Mock<IEventRepository>();
            UserAggregateRoot user = new UserAggregateRoot(userId, login, password, name, birthDate);
            IAggregateRepository _aggregateRepository = new AggregateRepository(eventRepositoryMock.Object);

            _aggregateRepository.Save(user, userId);

            var aggregate = await _aggregateRepository.Get<UserAggregateRoot>(userId);

            eventRepositoryMock.Verify(mock => mock.Get(userId), Times.Never());

            aggregate.Should().BeOfType<UserAggregateRoot>();
            aggregate.Version.Should().Be(1);


        }


        [Fact]
        public async Task Aggregate_repository_should_commit_aggregate_events_to_event_repository_and_delete_from_local_direcotry()
        {
            Mock<IEventRepository> eventRepositoryMock = new Mock<IEventRepository>();
            UserAggregateRoot user = new UserAggregateRoot(userId, login, password, name, birthDate);
            IAggregateRepository _aggregateRepository = new AggregateRepository(eventRepositoryMock.Object);

            _aggregateRepository.Save(user, userId);

            var result = await _aggregateRepository.Commit();

            //checking returns true 
            result.Should().BeTrue();
            //checking we execute save function with this parameters
            eventRepositoryMock.Verify(mock => mock.Save(user.GetUncommittedChanges()), Times.Once());
            // checking did dictionary is empty

            var userFromEvents = await _aggregateRepository.Get<UserAggregateRoot>(userId);
            // if this execute we know that dictionary is empty
            eventRepositoryMock.Verify(mock => mock.Get(userId), Times.Once());


        }



    }
}
