using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class IceCreamContext : DbContext
    {
        public DbSet<IceCream> IceCreams => Set<IceCream>();

        public IceCreamContext(DbContextOptions<IceCreamContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
