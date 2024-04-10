using Microsoft.EntityFrameworkCore;
using newbuy.Domain.Models;

namespace newbuy.Infrastructure.Data;


public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserType> UserType { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<ProductType> ProductType { get; set; }

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

        modelBuilder.Entity<Product>()
                    .Property(u => u.ProductTypeId)
                    .HasColumnName("ProductTypeId");

        modelBuilder.Entity<Product>()
            .HasOne(u => u.ProductType)
            .WithMany()
            .HasForeignKey(u => u.ProductTypeId);

    }

}