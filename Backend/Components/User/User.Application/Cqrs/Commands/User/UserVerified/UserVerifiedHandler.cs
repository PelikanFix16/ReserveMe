using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using SharedKernel.Application.Interfaces.Repositories;
using User.Application.Interfaces.Repositories;
using User.Domain.User;

namespace User.Application.Cqrs.Commands.User.UserVerified
{
    public class UserVerifiedHandler : IRequestHandler<UserVerifiedCommand,Result>
    {
        private readonly IMapper _mapper;
        private readonly IAggregateRepository _aggregateRepository;
        private readonly IUserProjectionRepository _repository;

        public UserVerifiedHandler(
            IMapper mapper,
            IAggregateRepository aggregateRepository,
            IUserProjectionRepository repository)
        {
            _mapper = mapper;
            _aggregateRepository = aggregateRepository;
            _repository = repository;
        }

        public async Task<Result> Handle(UserVerifiedCommand request,CancellationToken cancellationToken)
        {
            var userProjection = await _repository.GetByIdAsync(request.Id);
            if (userProjection.IsFailed)
                return Result.Fail("User not found");

            var userId = _mapper.Map<UserId>(request.Id);
            var aggregateUser = (await _aggregateRepository.GetAsync<UserAggregateRoot>(userId)).Value;
            aggregateUser.Confirm();
            _aggregateRepository.Save(aggregateUser,aggregateUser.Id);
            await _aggregateRepository.CommitAsync();
            return Result.Ok();
        }
    }
}
