using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedKernel.Application.Common.Interfaces.Security
{
    public interface IPasswordHash
    {
        string HashPassword(string password);
        bool VerifyHashedPassword(string passwordHash, string password);
    }
}
