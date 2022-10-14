using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.UniqueKey;

namespace Domain.Test.Mock.ValueObjects
{
    public class TestId : AggregateKey
    {
        public TestId(Guid key)
            : base(key)
        {
        }
    }
}
