using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using User.Application.Mapper.Projections;

namespace User.Infrastructure.Persistence.DataContext
{
    public class UserContext : DbContext
    {
        public DbSet<UserProjection> Users { get; set; } = null!;

        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

    }
}
