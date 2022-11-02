using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using SharedKernel.Application.Common.Event;
using SharedKernel.Domain.Event;
using User.Application.Interfaces.Repositories;
using User.Application.Projections;
using User.Domain.User.Events;

namespace User.Application.Consumers
{
    public class UserRegisteredEventConsumer : IConsumer<UserRegisteredEvent>
    {
        private readonly IUserProjectionRepository _repository;

        public UserRegisteredEventConsumer(IUserProjectionRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
        {
            await Task.Delay(5000);
            _repository.Save(new UserProjection
            {
                Id = context.Message.Key.Key,
                Email = context.Message.Login.Value,
                Password = context.Message.Password.Value,
                Name = context.Message.Name.FirstName,
                Surname = context.Message.Name.LastName
            });
            await _repository.SaveChangesAsync();
        }
    }
}
