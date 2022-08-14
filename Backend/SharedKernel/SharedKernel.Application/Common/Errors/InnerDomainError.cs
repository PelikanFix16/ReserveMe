using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;

namespace SharedKernel.Application.Common.Errors
{
    public class InnerDomainError : BaseError
    {
        private const int Code = 500;

        public InnerDomainError(string message)
            : base(message, Code)
        {
        }
    }
}
