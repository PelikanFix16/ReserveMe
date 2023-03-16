using AutoMapper;
using SharedKernel.Application.Interfaces.Events;
using SharedKernel.Application.Interfaces.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Application.Interfaces.Services;
using User.Application.Mapper.Projections;
using User.Domain.Manager.Events;

namespace User.Application.EventHandlers.Local.Manager
{
    public class ManagerCreatedEventHandler : IEventHandle<ManagerCreatedEvent>
    {
        private readonly IManagerService _managerService;
        private readonly IMapper _mapper;
        private readonly IEmailNotification _emailNotification;

        public ManagerCreatedEventHandler(
            IManagerService managerService,
            IMapper mapper,
            IEmailNotification emailNotification)
        {
            _managerService = managerService;
            _mapper = mapper;
            _emailNotification = emailNotification;
        }

        public async Task HandleAsync(ManagerCreatedEvent @event)
        {
            ManagerProjection managerProjection = _mapper.Map<ManagerProjection>(@event);
            await _managerService.ManagerCreateAsync(managerProjection);
            await _emailNotification.Send("New manager","Hello new manager");
        }
    }
}