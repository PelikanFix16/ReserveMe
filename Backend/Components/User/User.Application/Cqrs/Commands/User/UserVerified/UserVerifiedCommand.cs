using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace User.Application.Cqrs.Commands.User.UserVerified
{
    public class UserVerifiedCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }

}
