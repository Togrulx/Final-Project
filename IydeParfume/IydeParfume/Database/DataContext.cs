using Microsoft.EntityFrameworkCore;
using IydeParfume.Extensions;
using IydeParfume.Database.Models;
using IydeParfume.Database.Models.Common;

namespace IydeParfume.Database
{
    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
       : base(options)
        {

        }

        public DbSet<Navbar> Navbars { get; set; }
        public DbSet<SubNavbar> SubNavbars { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<ProductSeason> ProductSeasons { get; set; }
        public DbSet<UsageTime> UsageTimes { get; set; }
        public DbSet<ProductUsageTime> ProductUsageTimes { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserActivation> UserActivations { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketProduct> BasketProducts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogDisplay> BlogDisplays { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<SupportOrder> SupportOrders { get; set; }
        public DbSet<SupportDelivery> SupportDeliveries { get; set; }
        public DbSet<SupportPayment> SupportPayments { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<AboutUsImage> AboutUsImages { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }











        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly<Program>();
            modelBuilder.Entity<Product>().Property(x => x.Price).HasColumnType("decimal(18,2)");

        }
    }
    #region Auditing

    public partial class DataContext
    {
        public override int SaveChanges()
        {
            AutoAudit();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            AutoAudit();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AutoAudit();

            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            AutoAudit();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        private void AutoAudit()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is not IAuditable auditableEntry) //Short version
                {
                    continue;
                }

                //IAuditable auditableEntry = (IAuditable)entry;

                DateTime currentDate = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    auditableEntry.CreatedAt = currentDate;
                    auditableEntry.UpdatedAt = currentDate;
                }
                else if (entry.State == EntityState.Modified)
                {
                    auditableEntry.UpdatedAt = currentDate;
                }
            }
        }
    }

    #endregion
}
