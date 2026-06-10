
using Microsoft.EntityFrameworkCore;
using ProductApi.Domain.Entites;

namespace ProductApi.Infrastructure.Data
{
    public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
    }
}
