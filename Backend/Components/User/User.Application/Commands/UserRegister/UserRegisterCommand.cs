using MediatR;
using User.Application.Mapper.Dto;

namespace User.Application.Commands.UserRegister
{
    public class UserRegisterCommand : IRequest<UserRegisterDto>
    {
        public Guid Id { get; set; }
        public NameDto Name { get; set; }
        public LoginDto Login { get; set; }
        public PasswordDto Password { get; set; }
        public BirthDateDto BirthDate { get; set; }

        public UserRegisterCommand()
        {
            Id = Guid.NewGuid();
        }
    }
}
