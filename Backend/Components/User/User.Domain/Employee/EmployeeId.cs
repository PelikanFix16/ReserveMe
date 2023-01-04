using SharedKernel.Domain.UniqueKey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Domain.Employee
{
    public class EmployeeId : AggregateKey
    {
        public EmployeeId(Guid key) : base(key)
        {
        }
    }
}