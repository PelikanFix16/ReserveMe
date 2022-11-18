using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Application.Mapper.Dto;

namespace User.Application.Cqrs.Queries.UserLogin
{
    public class UserLoginDto
    {
        public string Id { get; set; }
        public NameDto Name { get; set; }
        public string Login { get; set; }
    }
}
