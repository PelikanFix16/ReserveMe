using MediatR;
using User.Application.Mapper.Dto;

namespace User.Application.Commands.UserRegister
{
    public class UserRegisterDto : IRequest<UserRegisterDto>
    {
        public string Id {get;set;} = Guid.NewGuid().ToString();
        public NameDto Name {get;set;}
        public string Login {get;set;}



        
    }
}