using MediatR;

namespace User.Application.Commands.UserRegister
{
    public class UserRegisterHandler : IRequestHandler<UserRegisterCommand, UserRegisterDto>
    {
        public Task<UserRegisterDto> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}