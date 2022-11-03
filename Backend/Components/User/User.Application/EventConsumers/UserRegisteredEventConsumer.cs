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

namespace User.Application.EventConsumers
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
            //ToDo: AutoMapper profile
            // Map Event to Projection
            //Save projection in database
        }
    }
}
