using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.UniqueKey;

namespace User.Domain.Manager
{
    public class ManagerId : AggregateKey
    {
        public ManagerId(Guid key) : base(key)
        {
        }
    }
}
