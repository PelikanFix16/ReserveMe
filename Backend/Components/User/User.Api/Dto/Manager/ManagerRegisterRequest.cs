using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Api.Dto.Manager
{
    public class ManagerRegisterRequest
    {
        public string Email { get; set; } = null!;
        public Guid UserId { get; set; }
    }
}