using Microsoft.EntityFrameworkCore;

namespace Onboard12.Server.Models;


public class StoreDbContext : DbContext 
{
    public StoreDbContext() { }
    public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Store> Stores { get; set; }
    public virtual DbSet<Sales> Sales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Product");

            entity.ToTable("Product");

            entity.HasMany(e => e.Sales).WithOne(s => s.Product).HasForeignKey(s => s.ProductId);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Customer");

            entity.ToTable("Customer");

            entity.HasMany(e => e.Sales).WithOne(s => s.Customer).HasForeignKey(s => s.CustomerId);
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Store");

            entity.ToTable("Store");

            entity.HasMany(e => e.Sales).WithOne(s => s.Store).HasForeignKey(s => s.StoreId);
        });

        modelBuilder.Entity<Sales>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Sales");

            entity.ToTable("Sales");

            entity.HasIndex(e => e.ProductId).HasDatabaseName("IX_Sales_ProductId");
            entity.HasIndex(e => e.CustomerId).HasDatabaseName("IX_Sales_CustomerId");
            entity.HasIndex(e => e.StoreId).HasDatabaseName("IX_Sales_StoreId");

            entity.Property(e => e.ProductId).HasColumnName("ProductId");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerId");
            entity.Property(e => e.StoreId).HasColumnName("StoreId");


            entity.HasOne(e => e.Product)
                .WithMany(p => p.Sales)
                .HasForeignKey(e => e.ProductId)
                .HasConstraintName("FK_Sales_Product");

            entity.HasOne(e => e.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(e => e.CustomerId)
                .HasConstraintName("FK_Sales_Customer");

            entity.HasOne(e => e.Store)
                .WithMany(s => s.Sales)
                .HasForeignKey(e => e.StoreId)
                .HasConstraintName("FK_Sales_Store");
        });


    }
}