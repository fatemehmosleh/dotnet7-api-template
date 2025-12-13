using Microsoft.EntityFrameworkCore;
using BackEnd.Services.Core.Api.Configuration;
using BackEnd.Services.Core.Api.Domain.Common;

namespace BackEnd.Services.Core.Api.Context
{
    // this is Core Context class for connectig to database using entity framework
    public class CoreContext : DbContext
    {
        public CoreContext(DbContextOptions<CoreContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        // define DbSets 
        public DbSet<User> Users { get; set; }
        

    }
}
