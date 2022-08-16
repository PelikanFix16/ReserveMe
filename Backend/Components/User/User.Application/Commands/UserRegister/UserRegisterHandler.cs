using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FluentResults;
using MediatR;
using SharedKernel.Application.Repositories.Aggregate;
using User.Domain.User;

namespace User.Application.Commands.UserRegister
{
    public class UserRegisterHandler : IRequestHandler<UserRegisterCommand, Result<UserRegisterDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAggregateRepository _aggregateRepository;

        public UserRegisterHandler(IMapper mapper, IAggregateRepository aggregateRepository)
        {
            _mapper = mapper;
            _aggregateRepository = aggregateRepository;
        }

        public async Task<Result<UserRegisterDto>> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            var userAggregate = _mapper.Map<UserAggregateRoot>(request);
            _aggregateRepository.Save(userAggregate, userAggregate.Id);
            await _aggregateRepository.CommitAsync();
            return _mapper.Map<UserRegisterDto>(userAggregate);
        }
    }
}
