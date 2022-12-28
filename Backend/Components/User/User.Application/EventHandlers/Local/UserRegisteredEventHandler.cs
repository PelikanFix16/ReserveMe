using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SharedKernel.Application.Interfaces.Events;
using SharedKernel.Application.Interfaces.Notification;
using User.Application.Interfaces.Services;
using User.Application.Mapper.Projections;
using User.Domain.User.Events;

namespace User.Application.EventHandlers.Local
{
    public class UserRegisteredEventHandler : IEventHandle<UserRegisteredEvent>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IEmailNotification _emailNotify;

        public UserRegisteredEventHandler(
            IUserService userService,
            IMapper mapper,
            IEmailNotification emailNotify)
        {
            _userService = userService;
            _mapper = mapper;
            _emailNotify = emailNotify;
        }

        public async Task HandleAsync(UserRegisteredEvent @event)
        {
            var userProjection = _mapper.Map<UserProjection>(@event);
            await _userService.UserCreateAsync(userProjection);
            //ToDo: Delete static string change to const strings in another file
            await _emailNotify.Send("New user", $"Hello {userProjection.Name} click here to verify your account {userProjection.Id}");
        }
    }
}
