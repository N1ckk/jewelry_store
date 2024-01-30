using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Category> category { get; set; } = null!;

        public DbSet<Product> product { get; set; } = null!;
        public DbSet<Client> client { get; set; } = null !;
        public DbSet<Order> orders { get; set; } = null!;
        public DbSet<Worker> workers { get; set; } = null!;
        public DbSet<ProductOrders> product_orders { get; set;} = null!;
        public DbSet<ProductDetails> product_details { get; set; } = null!;

    }
}
