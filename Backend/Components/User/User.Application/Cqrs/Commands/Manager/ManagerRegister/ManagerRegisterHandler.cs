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
using User.Domain.Manager;
using User.Domain.User;

namespace User.Application.Cqrs.Commands.Manager.ManagerRegister
{
    public class ManagerRegisterHandler : IRequestHandler<ManagerRegisterCommand,Result>
    {
        private readonly IMapper _mapper;
        private readonly IAggregateRepository _aggregateRepository;
        private readonly IUserProjectionRepository _userProjectionRepository;
        private readonly IManagerProjectionRepository _managerProjectionRepository;

        public ManagerRegisterHandler(
            IMapper mapper,
            IAggregateRepository aggregateRepository,
            IUserProjectionRepository userProjectionRepository,
            IManagerProjectionRepository managerProjectionRepository)
        {
            _mapper = mapper;
            _aggregateRepository = aggregateRepository;
            _userProjectionRepository = userProjectionRepository;
            _managerProjectionRepository = managerProjectionRepository;
        }

        public async Task<Result> Handle(ManagerRegisterCommand request,CancellationToken cancellationToken)
        {
            EmailDto email = _mapper.Map<EmailDto>(request.ManagerEmail);
            Result<Mapper.Projections.UserProjection> userProjection = await _userProjectionRepository.GetByEmailAsync(email);
            if (userProjection.IsSuccess)
                return Result.Fail("User with this email exists");

            Result<Mapper.Projections.UserProjection> userProjectionId = await _userProjectionRepository.GetByIdAsync(request.UserId);
            if (userProjectionId.IsFailed)
                return Result.Fail("User with this id not exists");

            Result<Mapper.Projections.ManagerProjection> managerProjection = await _managerProjectionRepository.GetByEmailAsync(email);
            if (managerProjection.IsSuccess)
                return Result.Fail("Manager with this email exists");

            Result<Mapper.Projections.ManagerProjection> managerId = await _managerProjectionRepository.GetByUserId(request.UserId);
            if (managerId.IsSuccess)
                return Result.Fail("User already have registered manager");

            ManagerAggregateRoot managerAggregate = _mapper.Map<ManagerAggregateRoot>(request);
            Result resSave = _aggregateRepository.Save(managerAggregate,managerAggregate.Id);
            if (resSave.IsFailed)
                return resSave;

            Result resCommit = await _aggregateRepository.CommitAsync();

            return resCommit.IsFailed ? resCommit : Result.Ok();
        }
    }
}