using Microsoft.EntityFrameworkCore;
using MyAds.Entities;


public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasOne(c => c.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(c => c.UserId);

        modelBuilder.Entity<Order>()
            .HasOne(c => c.Category)
            .WithMany(c => c.Orders)
            .HasForeignKey(c => c.CategoryId);
    }
};

