using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SharedKernel.Application.Interfaces.Events;
using User.Application.Interfaces.Services;
using User.Application.Mapper.Projections;
using User.Domain.User.Events;

namespace User.Application.EventHandlers.Local
{
    public class UserRegisteredEventHandler : IEventHandle<UserRegisteredEvent>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserRegisteredEventHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task HandleAsync(UserRegisteredEvent @event)
        {
            var userProjection = _mapper.Map<UserProjection>(@event);
            await _userService.UserCreateAsync(userProjection);
        }
    }
}
