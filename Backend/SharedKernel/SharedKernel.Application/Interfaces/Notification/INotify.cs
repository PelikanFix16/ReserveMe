using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;

namespace SharedKernel.Application.Interfaces.Notification
{
    public interface INotify
    {
        Task<Result> Send(string title, string message);
    }
}
