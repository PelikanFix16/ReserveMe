using MediatR;
using User.Application.Mapper.Dto;

namespace User.Application.Commands.UserRegister
{
    public class UserRegisterDto
    {
        public string Id {get;set;}
        public NameDto Name {get;set;}
        public string Login {get;set;}
        
    }
}