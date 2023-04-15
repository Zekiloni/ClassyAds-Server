using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using WebApplication1.Models;


public class Database : DbContext
{
    public Database(DbContextOptions<Database> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Classified> Classifieds { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Classified>()
            .HasOne(c => c.User)
            .WithMany(u => u.Classifieds)
            .HasForeignKey(c => c.UserId);

        modelBuilder.Entity<Classified>()
            .HasOne(c => c.Category)
            .WithMany(c => c.Classifieds)
            .HasForeignKey(c => c.CategoryId);
    }
}

