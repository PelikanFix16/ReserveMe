using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;

namespace SharedKernel.Application.Repositories.Errors
{
    public class SaveEventError : Error
    {
        private const string ErrorMessage = "Failed to save events to event repository";
        private const int Code = 503;

        public SaveEventError()
            : base(ErrorMessage)
        {
            Metadata.Add("StatusCode", Code);
        }
    }
}
