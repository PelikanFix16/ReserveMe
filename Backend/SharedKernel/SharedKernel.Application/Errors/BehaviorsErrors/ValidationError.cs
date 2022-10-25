using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Application.Interfaces.BaseError;

namespace SharedKernel.Application.Errors.BehaviorsErrors
{
    public class ValidationError : BaseError
    {
        private const int Code = 400;

        public ValidationError(string propertyName, string message)
            : base(FormatMessage(propertyName, message), Code)
        {
        }

        private static string FormatMessage(string propertyName, string message) => $"Error while validating {propertyName} - {message}";
    }
}
