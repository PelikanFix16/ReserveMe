using AutoMapper;
using FluentResults;
using MediatR;
using SharedKernel.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Application.Interfaces.Repositories;

namespace User.Application.Cqrs.Commands.Employee.EmployeeRegister
{
    public class EmployeeRegisterHandler : IRequestHandler<EmployeeRegisterCommand,Result>
    {

        private readonly IMapper _mapper;

        private readonly IAggregateRepository _aggregateRepository;

        private readonly IManagerProjectionRepository _managerProjectionRepository;

        public EmployeeRegisterHandler(
            IMapper mapper,
            IAggregateRepository aggregateRepository,
            IManagerProjectionRepository managerProjectionRepository)
        {
            _mapper = mapper;
            _aggregateRepository = aggregateRepository;
            _managerProjectionRepository = managerProjectionRepository;
        }

        public async Task<Result> Handle(EmployeeRegisterCommand request,CancellationToken cancellationToken)
        {

        }
    }
}