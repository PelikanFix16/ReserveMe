using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Application.Mapper.Dto;
using User.Domain.ValueObjects;

namespace User.Application.Common.Interfaces.Security
{
    public interface IPasswordHash
    {
        PasswordDto Hash(PasswordDto password);
        bool Verify(PasswordDto password, string hash);
    }
}
