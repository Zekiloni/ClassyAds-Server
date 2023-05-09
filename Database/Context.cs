using Microsoft.EntityFrameworkCore;
using MyAds.Entities;


public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Classified> Classifieds { get; set; }

    public DbSet<ClassifiedMediaFile> MediaFiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Classified>()
            .HasOne(c => c.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(c => c.UserId);

        modelBuilder.Entity<Classified>()
            .HasOne(c => c.Category)
            .WithMany(c => c.Classifieds)
            .HasForeignKey(c => c.CategoryId);
    }
};

