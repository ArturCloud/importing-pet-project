using Microsoft.EntityFrameworkCore;

namespace DataImportProj.Database
{
    public class DbAppContext(DbContextOptions<DbAppContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>().HasMany(x => x.Translations).WithOne(x => x.Product).OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<Product>().HasMany(x => x.ProductCustomers).WithOne(x => x.Product).OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<ProductCustomer>().HasOne(x => x.Customer).WithMany(x => x.ProductCustomers)
            //    .HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<ProductCustomer>().HasOne(x => x.Product).WithMany(x => x.ProductCustomers)
            //    .HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<ProductCustomer> ProductCustomers { get; set; }

        public DbSet<ProductTranslation> ProductTranslations { get; set; }
    }
}
