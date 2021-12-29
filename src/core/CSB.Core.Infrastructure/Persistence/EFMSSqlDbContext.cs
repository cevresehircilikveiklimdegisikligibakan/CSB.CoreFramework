using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;

namespace CSB.Core.Infrastructure.Persistence
{
    public abstract class EFMSSqlDbContext : EFDbContext
    {
        public EFMSSqlDbContext(string connectionStringName, IConfiguration configuration, IServiceProvider provider)
            : base(connectionStringName, configuration, provider) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseSqlServer(_configuration.GetConnectionString(_connectionStringName)));
            }
        }
    }
}