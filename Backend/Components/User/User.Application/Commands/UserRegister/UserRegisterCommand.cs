using MediatR;
using User.Application.Mapper.Dto;

namespace User.Application.Commands.UserRegister
{
    public class UserRegisterCommand : IRequest<UserRegisterDto>
    {
        public string? Id {get;set;}
        public NameDto? Name {get;set;}
        public string? Login {get;set;}
        public string? Password {get;set;}
        public DateTime BirthDate {get;set;}


    }
}