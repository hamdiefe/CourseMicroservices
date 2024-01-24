using Microsoft.EntityFrameworkCore;

namespace Course.Services.Order.Infrastructure
{
    public class OrderDbContext : DbContext
    {
        public const string DEFAULT_SCHEME = "ordering";

        public OrderDbContext(DbContextOptions<OrderDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Domain.OrderAggregate.Order> Orders { get; set; }

        public DbSet<Domain.OrderAggregate.OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.OrderAggregate.Order>().ToTable("Order", DEFAULT_SCHEME);
            modelBuilder.Entity<Domain.OrderAggregate.OrderItem>().ToTable("OrderItem", DEFAULT_SCHEME);

            modelBuilder.Entity<Domain.OrderAggregate.OrderItem>().Property(x => x.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Domain.OrderAggregate.Order>().OwnsOne(x => x.Address).WithOwner();
            base.OnModelCreating(modelBuilder);
        }
    }
}
