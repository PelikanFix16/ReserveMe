using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Application.Common.Interfaces.Security;
using BCrypt;

namespace SharedKernel.Infrastructure.Security
{
    public class PasswordHash : IPasswordHash
    {
        public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
        public bool VerifyHashedPassword(string passwordHash, string password) => BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
