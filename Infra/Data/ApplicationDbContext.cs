



using IWantApp.Models.Orders;

namespace IWantApp.Infra.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Order> Orders => Set<Order>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<Notification>();

            modelBuilder.Entity<Category>()
                .Property(c => c.Name).IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.Name).IsRequired();
            modelBuilder.Entity<Product>()
                .Property(p => p.Description).HasMaxLength(255);
            modelBuilder.Entity<Product>()
                .Property(p => p.Price).HasColumnType("decimal(10,2)").IsRequired();


            // modelBuilder.Entity<Order>()
            //     .Property(c => c.Name).IsRequired(false);
            modelBuilder.Entity<Order>()
                .Property(o => o.ClientId).IsRequired();
            modelBuilder.Entity<Order>()
                .Property(o => o.DeliveryAddress).IsRequired();
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Products)
                .WithMany(p => p.Orders)
                .UsingEntity(e => e.ToTable("OrderProducts"));
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>()
                .HaveMaxLength(100);
        }
    }
}