using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using User.Application.Projections;

namespace User.Infrastructure.DataContext
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
