using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Application.Interfaces.Security
{
    public interface ISecurityHash
    {
        string HashPassword(string password);
        bool VerifyHashedPassword(string passwordHash, string password);
    }
}
