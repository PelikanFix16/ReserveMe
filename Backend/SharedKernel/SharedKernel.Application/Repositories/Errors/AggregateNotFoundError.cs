using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using SharedKernel.Application.Common;
using SharedKernel.Domain.UniqueKey;

namespace SharedKernel.Application.Repositories.Errors
{
    public class AggregateNotFoundError : BaseError
    {
        private const string ErrorMessage = "Aggregate with key {0} not found";
        private const int Code = 404;

        public AggregateNotFoundError(AggregateKey key)
            : base(string.Format(ErrorMessage, key), Code)
        {
        }
    }
}
