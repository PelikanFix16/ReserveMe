using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;

namespace SharedKernel.Application.Common
{
    public abstract class BaseError : Error
    {
        protected BaseError(string message, int statusCode)
            : base(message)
        {
            Metadata.Add("StatusCode", statusCode);
        }
    }
}
