using Microsoft.EntityFrameworkCore;
using MobilivaCase.Data;

namespace MobilivaCase.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

    }
}
