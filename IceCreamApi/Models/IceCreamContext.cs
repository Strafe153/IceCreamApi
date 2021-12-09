using Microsoft.EntityFrameworkCore;

namespace IceCreamApi.Models
{
    public class IceCreamContext : DbContext
    {
        public DbSet<IceCream> IceCreams { get; set; }

        public IceCreamContext(DbContextOptions<IceCreamContext> options) : base(options)
        {
        }
    }
}
