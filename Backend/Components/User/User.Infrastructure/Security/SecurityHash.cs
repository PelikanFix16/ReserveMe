using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Application.Interfaces.Security;

namespace User.Infrastructure.Security
{
    public class SecurityHash : ISecurityHash
    {
        public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
        public bool VerifyHashedPassword(string passwordHash, string password) => BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
