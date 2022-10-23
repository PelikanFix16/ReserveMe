using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FluentResults;
using MediatR;
using SharedKernel.Application.Interfaces.Repositories;
using User.Application.Interfaces.Security;
using User.Domain.User;

namespace User.Application.Commands.UserRegister
{
    public class UserRegisterHandler : IRequestHandler<UserRegisterCommand, Result<UserRegisterDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAggregateRepository _aggregateRepository;
        private readonly ISecurityHash _passwordHash;

        public UserRegisterHandler(IMapper mapper, IAggregateRepository aggregateRepository, ISecurityHash passwordHash)
        {
            _mapper = mapper;
            _aggregateRepository = aggregateRepository;
            _passwordHash = passwordHash;
        }

        public async Task<Result<UserRegisterDto>> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            request.Password.Password = _passwordHash.HashPassword(request.Password.Password);
            var userAggregate = _mapper.Map<UserAggregateRoot>(request);
            _aggregateRepository.Save(userAggregate, userAggregate.Id);
            await _aggregateRepository.CommitAsync();
            return _mapper.Map<UserRegisterDto>(userAggregate);
        }
    }
}
