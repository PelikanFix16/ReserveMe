using AutoMapper;
using SharedKernel.Application.Interfaces.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Application.Interfaces.Services;
using User.Application.Mapper.Projections;
using User.Domain.Employee.Events;

namespace User.Application.EventHandlers.Local.Employee
{
    public class EmployeeCreatedEventHandler : IEventHandle<EmployeeCreatedEvent>
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeCreatedEventHandler(
            IEmployeeService employeeService,
            IMapper mapper)
        {
            this._employeeService = employeeService;
            this._mapper = mapper;
        }

        public async Task HandleAsync(EmployeeCreatedEvent @event)
        {
            EmployeeProjection employeeProjection = _mapper.Map<EmployeeProjection>(@event);
            await _employeeService.EmployeeCreateAsync(employeeProjection);
        }
    }
}