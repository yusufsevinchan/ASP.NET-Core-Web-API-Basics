using Microsoft.EntityFrameworkCore;
using ProductAppAPI.Core;

namespace ProductAppAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<ProductEntity> Products { get; set; }

    }
}
