using ClassyAdsServer.Entities;
using Microsoft.EntityFrameworkCore;
using ClassyAdsServer.Entities;


public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    public DbSet<Notification> Notifications { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Advertisement> Advertisements { get; set; }

    public DbSet<AdvertisementMediaFile> MediaFiles { get; set; }

    public DbSet<Review> Reviews { get; set; }

    public DbSet<Message> Messages { get; set; }

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

        modelBuilder.Entity<Category>()
            .HasOne(c => c.ParentCategory)
            .WithMany(c => c.ChildCategories)
            .HasForeignKey(c => c.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Message>()
           .HasOne(m => m.Sender)
           .WithMany()
           .HasForeignKey(m => m.SenderId)
           .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Target)
            .WithMany()
            .HasForeignKey(m => m.TargetId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
};

