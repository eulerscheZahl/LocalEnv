using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LocalEnv.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Game> Games { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<ParameterRange> Ranges { get; set; }
        public DbSet<Agent> Agents { get; set; }
    }
}