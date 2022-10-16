using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Test.Mock;
using Domain.Test.Mock.Events;
using Domain.Test.Mock.ValueObjects;
using FluentAssertions;
using Moq;
using SharedKernel.Application.Repositories.Aggregate;
using SharedKernel.Domain;
using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
using SharedKernel.InterfaceAdapters.Interfaces.Repositories;
using SharedKernel.SharedKernel.InterfaceAdapters.Repositories.Aggregate;
using Xunit;

namespace InterfaceAdapters.Test.Repositories
{
    public class AggregateRepositoryTest
    {
        private readonly TestId _testId = new(Guid.NewGuid());
        private readonly TestName _testName = new("Test", "Test2");
        private readonly TestBirthDate _testBirthDate = new(AppTime.Now().AddYears(-20));

        [Fact]
        public async Task AggregateRepositoryShouldReturnAggregateWithOneEventRegisteredFromEventRepositoryWhenCallGetMethodAsync()
        {
            var eventRepositoryMock = new Mock<IEventRepository>();
            var testCreatedEvent = new TestCreatedEvent(
                _testId,
                _testName,
                _testBirthDate,
                0
            );
            IList<DomainEvent> list = new List<DomainEvent>
            {
                testCreatedEvent
            };

            eventRepositoryMock.Setup(x => x.GetAsync(It.IsAny<AggregateKey>())).ReturnsAsync(list);

            IAggregateRepository aggregateRepository = new AggregateRepository(eventRepositoryMock.Object);

            var userAggregate = await aggregateRepository.GetAsync<TestAggregateRoot>(_testId);

            eventRepositoryMock.Verify(x => x.GetAsync(_testId), Times.Once());
            userAggregate.Value.Should().BeOfType<TestAggregateRoot>();
            userAggregate.Value.Version.Should().Be(1);
        }

        [Fact]
        public async Task AggregateRepositoryShouldSaveAggregateToLocalDictionaryWhenPassWitchCorrectAggregateVersionAsync()
        {
            var eventRepositoryMock = new Mock<IEventRepository>();
            var test = new TestAggregateRoot(_testId, _testName, _testBirthDate);
            IAggregateRepository aggregateRepository = new AggregateRepository(eventRepositoryMock.Object);

            aggregateRepository.Save(test, _testId);

            var aggregate = await aggregateRepository.GetAsync<TestAggregateRoot>(_testId);

            eventRepositoryMock.Verify(mock => mock.GetAsync(_testId), Times.Never());

            aggregate.Value.Should().BeOfType<TestAggregateRoot>();
            aggregate.Value.Version.Should().Be(1);
        }

        [Fact]
        public async Task AggregateRepositoryShouldCommitAggregateEventsToEventRepositoryAndDeleteFromLocalDirectoryAsync()
        {
            var eventRepositoryMock = new Mock<IEventRepository>();
            var test = new TestAggregateRoot(_testId, _testName, _testBirthDate);
            IAggregateRepository aggregateRepository = new AggregateRepository(eventRepositoryMock.Object);

            aggregateRepository.Save(test, _testId);

            var result = await aggregateRepository.CommitAsync();

            //checking returns true
            result.IsSuccess.Should().BeTrue();
            //checking we execute save function with this parameters
            eventRepositoryMock.Verify(mock => mock.SaveAsync(test.GetUncommittedChanges()), Times.Once());
            // checking did dictionary is empty

            var userFromEvents = await aggregateRepository.GetAsync<TestAggregateRoot>(_testId);
            // if this execute we know that dictionary is empty
            eventRepositoryMock.Verify(mock => mock.GetAsync(_testId), Times.Once());
        }

    }
}
