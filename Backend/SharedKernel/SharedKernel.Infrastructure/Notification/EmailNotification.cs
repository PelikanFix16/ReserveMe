using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using SharedKernel.Application.Interfaces.Notification;

namespace SharedKernel.Infrastructure.Notification
{
    public class EmailNotification : IEmailNotification
    {
        public Task<Result> Send(string title, string message)
        {
            Console.WriteLine("Email sent");
            return Task.FromResult(Result.Ok());
        }
    }
}
