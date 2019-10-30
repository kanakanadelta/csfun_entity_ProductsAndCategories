using Microsoft.EntityFrameworkCore;

namespace ProductsAndCategories.Models
{
    public class PADContext : DbContext
    {
        public PADContext(DbContextOptions options) : base(options) {}

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Association> Associations { get; set; }
    }
}