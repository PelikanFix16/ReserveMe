using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Domain.Employee;

namespace User.Application.Mapper.Projections
{
    public class EmployeeProjection
    {
        public Guid Id { get; set; }
        public Guid ManagerId { get; set; }
        public string Email { get; set; }
        public EmployeePrivileges Privileges { get; set; }
    }
}