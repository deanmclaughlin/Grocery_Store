using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ClassroomStart.Models;

namespace ClassroomStart.Data
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Inventory> Inventories { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseMySql("server=localhost;user=root;database=exsm3943-project", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.24-mariadb"));
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);
                entity.Property(e => e.CustomerId).ValueGeneratedNever();
            });
            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => e.ProductId);
                entity.Property(e => e.ProductId).ValueGeneratedNever();

                entity.HasData(
                 new Inventory[]
                 {
                        new Inventory (1,"Apples", 150, 1.50m),
                        new Inventory (2,"Oranges", 50, 1.25m),
                        new Inventory (3,"Bananas", 95, 0.99m),
                        new Inventory (4,"Bread", 85, 3.50m),
                        new Inventory (5,"Milk", 99, 6.50m),
                        new Inventory (6,"Eggs", 250, 5.35m),
                        new Inventory (7,"Bacon", 100, 8.99m),
                        new Inventory (8,"Coffee", 88, 12.50m),
                        new Inventory (9,"Ground Beef", 75, 17.55m),
                        new Inventory (10,"Steak", 75, 20.25m),
                        new Inventory (11,"Sausage", 110, 7.25m),
                        new Inventory (12,"Lettuce", 70, 1.99m),
                        new Inventory (13,"Tomato", 91, 0.89m),
                        new Inventory (14,"Potato", 88, 0.55m),
                        new Inventory (15,"Spinach", 45, 1.05m)

                 });



            });
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderId);
                entity.Property(e => e.OrderId).ValueGeneratedOnAdd();

                entity.HasOne(x => x.Customer)
                      .WithMany(x => x.Orders)
                      .HasForeignKey(x => x.CustomerId);
                entity.HasOne(x => x.Inventory)
                      .WithMany(x => x.Orders)
                      .HasForeignKey(x => x.ProductId);
                //entity.HasOne(d => d.Product)
                //    .WithMany(p => p.InverseProduct)
                //    .HasForeignKey(d => d.ProductId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("orders_ibfk_1");
            });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}