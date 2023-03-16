using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Application.Interfaces.Events;
using User.Application.Interfaces.Services;
using User.Domain.User.Events;

namespace User.Application.EventHandlers.Local.User
{
    public class UserRegisteredConfirmationEventHandler : IEventHandle<UserRegistrationConfirmedEvent>
    {
        private readonly IUserService _userService;

        public UserRegisteredConfirmationEventHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(UserRegistrationConfirmedEvent @event)
        {
            Console.WriteLine("Confirmed");
            await _userService.UserConfirmAsync(@event.Key.Key);
        }
    }
}
