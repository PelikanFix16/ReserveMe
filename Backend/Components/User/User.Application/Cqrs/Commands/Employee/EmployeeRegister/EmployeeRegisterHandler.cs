using AutoMapper;
using FluentResults;
using MediatR;
using SharedKernel.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Application.Interfaces.Repositories;
using User.Application.Mapper.Dto;
using User.Domain.Employee;

namespace User.Application.Cqrs.Commands.Employee.EmployeeRegister
{
    public class EmployeeRegisterHandler : IRequestHandler<EmployeeRegisterCommand,Result>
    {

        private readonly IMapper _mapper;

        private readonly IAggregateRepository _aggregateRepository;

        private readonly IManagerProjectionRepository _managerProjectionRepository;
        private readonly IUserProjectionRepository _userProjectionRepository;
        private readonly IEmployeeProjectionRepository _employeeProjectionRepository;

        public EmployeeRegisterHandler(
            IMapper mapper,
            IAggregateRepository aggregateRepository,
            IManagerProjectionRepository managerProjectionRepository,
            IEmployeeProjectionRepository employeeProjectionRepository,
            IUserProjectionRepository userProjectionRepository)
        {
            _mapper = mapper;
            _aggregateRepository = aggregateRepository;
            _managerProjectionRepository = managerProjectionRepository;
            _userProjectionRepository = userProjectionRepository;
            _employeeProjectionRepository = employeeProjectionRepository;
        }

        public async Task<Result> Handle(EmployeeRegisterCommand request,CancellationToken cancellationToken)
        {
            Result<Mapper.Projections.UserProjection> user = await _userProjectionRepository.GetByEmailAsync(request.Email);
            if (user.IsSuccess)
                return Result.Fail("User with this email already exists");

            Result<Mapper.Projections.ManagerProjection> manager = await _managerProjectionRepository.GetById(request.ManagerId);
            if (manager.IsFailed)
                return Result.Fail("Manager with this id not exists");

            if (manager.Value.Email == request.Email.Email)
                return Result.Fail("Manager and employee cannot have same email");

            //ToDo check if manager is activated and can create employee

            Result<Mapper.Projections.EmployeeProjection> employee = await _employeeProjectionRepository.GetByEmailAsync(request.Email);
            if (employee.IsSuccess)
                return Result.Fail("Employee with this email already exists");

            EmployeeAggregateRoot employeeAggregateRoot = _mapper.Map<EmployeeAggregateRoot>(request);
            Result saveResult = _aggregateRepository.Save(employeeAggregateRoot,employeeAggregateRoot.Id);
            if (saveResult.IsFailed)
                return Result.Fail("Failed to save aggregate");

            Result commitResult = await _aggregateRepository.CommitAsync();

            return commitResult.IsFailed ? Result.Fail("Failed to commit aggregate in event store") : Result.Ok();
        }
    }
}