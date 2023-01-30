using FluentResults;
using MediatR;
using User.Application.Mapper.Dto;

namespace User.Application.Cqrs.Commands.User.UserRegister
{
    public class UserRegisterCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public NameDto Name { get; set; }
        public EmailDto Login { get; set; }
        public PasswordDto Password { get; set; }
        public BirthDateDto BirthDate { get; set; }

        public UserRegisterCommand() => Id = Guid.NewGuid();
    }
}
