using Microsoft.EntityFrameworkCore;
using newbuy.Domain.Models;

namespace newbuy.Infrastructure.Data;


public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserType> UserType { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<ItemType> ItemType { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .Property(u => u.UserTypeId)
            .HasColumnName("UserTypeId");

        modelBuilder.Entity<User>()
            .HasOne(u => u.UserType)
            .WithMany()
            .HasForeignKey(u => u.UserTypeId);

        modelBuilder.Entity<Item>()
                    .Property(u => u.ItemTypeId)
                    .HasColumnName("ItemTypeId");

        modelBuilder.Entity<Item>()
            .HasOne(u => u.ItemType)
            .WithMany()
            .HasForeignKey(u => u.ItemTypeId);

    }

}
