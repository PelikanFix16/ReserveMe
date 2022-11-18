using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FluentResults;
using MediatR;
using SharedKernel.Application.Interfaces.Repositories;
using User.Application.Interfaces.Repositories;
using User.Application.Interfaces.Security;
using User.Application.Mapper.Dto;
using User.Domain.User;

namespace User.Application.Cqrs.Commands.UserRegister
{
    public class UserRegisterHandler : IRequestHandler<UserRegisterCommand, Result<UserRegisterDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAggregateRepository _aggregateRepository;
        private readonly ISecurityHash _passwordHash;
        private readonly IUserProjectionRepository _repository;

        public UserRegisterHandler(
            IMapper mapper,
            IAggregateRepository aggregateRepository,
            ISecurityHash passwordHash,
            IUserProjectionRepository repository)
        {
            _mapper = mapper;
            _aggregateRepository = aggregateRepository;
            _passwordHash = passwordHash;
            _repository = repository;
        }

        public async Task<Result<UserRegisterDto>> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            var login = _mapper.Map<LoginDto>(request.Login);
            var userProjection = await _repository.GetAsync(login);
            if (userProjection.IsSuccess)
                return Result.Fail("User already exists");

            request.Password.Password = _passwordHash.HashPassword(request.Password.Password);
            var userAggregate = _mapper.Map<UserAggregateRoot>(request);
            _aggregateRepository.Save(userAggregate, userAggregate.Id);
            await _aggregateRepository.CommitAsync();
            return _mapper.Map<UserRegisterDto>(userAggregate);
        }
    }
}
