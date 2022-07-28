using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using SharedKernel.Domain;
using SharedKernel.Infrastructure.Repositories.MessageBus.Rabbit;
using User.Domain.User;
using User.Domain.User.Events;
using User.Domain.ValueObjects;
using Xunit;

namespace Infrastructure.Test.EventBus.Rabbit
{
    public class PublishEventTest
    {
        [Fact]
        public void ShouldSendEventToRabbitQueue()
        {
            // Arrange
            var eventBus = new PublishEvent(RabbitSettingsLocal.GetConfig());
            var userId = new UserId(Guid.NewGuid());
            var login = Login.Create("example@gmail.com");
            var password = Password.Create("Test@21Tsd");
            var name = Name.Create("Test", "Test");
            var birthDate = BirthDate.Create(AppTime.Now().AddYears(-18));

            var registeredEvent = new UserRegisteredEvent(userId, login, password, name, birthDate, 0);
            Action act = () => eventBus.Publish(registeredEvent);
            act.Should().NotThrow();
        }
    }
}
