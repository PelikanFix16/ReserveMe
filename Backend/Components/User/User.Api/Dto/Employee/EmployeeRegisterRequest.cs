using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Domain.Employee;

namespace User.Api.Dto.Employee
{
    public class EmployeeRegisterRequest
    {
        public string Email { get; set; } = null!;
        public Guid ManagerId { get; set; }
        public EmployeePrivileges Privileges { get; set; }

    }
}