using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;

namespace User.Application.Commands.UserRegister
{
    public class UserRegisterHandler : IRequestHandler<UserRegisterCommand, UserRegisterDto>
    {
        public UserRegisterHandler(IMapper mapper)
        {

        }
        public async Task<UserRegisterDto> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            // var validator = new UserRegisterCommandValidator();
            // var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            // if (!validatorResult.IsValid)
            // {
            //     throw new ValidationException("Invalid data");
            // }

            throw new NotImplementedException();

        }
    }
}
