using Microsoft.EntityFrameworkCore;
using MyAds.Entities;


public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Advertisement> Advertisements { get; set; }
    public DbSet<AdvertisementMediaFile> MediaFiles { get; set; }
    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Advertisement>()
            .HasOne(c => c.User)
            .WithMany(u => u.Advertisements)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Advertisement>()
            .HasOne(c => c.Category)
            .WithMany(c => c.Advertisements)
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
};

