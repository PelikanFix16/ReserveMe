using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Application.Common.Interfaces.Security;
using User.Application.Mapper.Dto;

namespace User.Infrastructure.Common.Security
{
    public class PasswordHash : IPasswordHash
    {
        public PasswordHash()
        {

        }
        public PasswordDto Hash(PasswordDto password)
        {

        }

        public bool Verify(PasswordDto password, string hash)
        {
            throw new NotImplementedException();
        }
    }
}
