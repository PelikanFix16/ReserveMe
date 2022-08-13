using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using SharedKernel.Domain.UniqueKey;

namespace SharedKernel.Application.Repositories.Errors
{
    public class AggregateVersionError : Error
    {
        private const string ErrorMessage = "Aggregate with key {0} has invalid version";
        private const int Code = 500;

        public AggregateVersionError(AggregateKey key)
            : base(string.Format(ErrorMessage, key))
        {
            Metadata.Add("StatusCode", Code);
        }
    }
}
