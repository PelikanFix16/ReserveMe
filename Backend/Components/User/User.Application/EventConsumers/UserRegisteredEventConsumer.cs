using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using SharedKernel.Application.Common.Event;
using SharedKernel.Domain.Event;
using User.Application.Interfaces.Repositories;
using User.Application.Interfaces.Services;
using User.Application.Mapper.Projections;
using User.Domain.User.Events;

namespace User.Application.EventConsumers
{
    public class UserRegisteredEventConsumer : IConsumer<UserRegisteredEvent>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserRegisteredEventConsumer(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
        {
            var projection = _mapper.Map<UserProjection>(context.Message);
            await _userService.UserCreateAsync(projection);
        }
    }
}
