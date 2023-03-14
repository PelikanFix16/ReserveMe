using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Application.Mapper.Projections
{
    public class ManagerProjection
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }

    }
}