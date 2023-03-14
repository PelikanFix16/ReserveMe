using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Application.Mapper.Projections;

namespace User.Infrastructure.Persistence.DataContext
{
    public class ManagerContext : DbContext
    {
        public DbSet<ManagerProjection> Manager { get; set; } = null!;

        public ManagerContext(DbContextOptions<ManagerContext> options)
            : base(options)
        {
        }
    }
}