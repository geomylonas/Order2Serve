using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Order2ServeDbContext : DbContext
    {
        public Order2ServeDbContext(DbContextOptions<Order2ServeDbContext> options) : base(options) 
        {
        }
        public DbSet<Log> Logs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Extra> Extra { get; set; }
        public DbSet<Orderable> Orderable { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Table> Tables { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Extra>()
                .Property(e => e.AdditionalPrice)
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<Orderable>()
                .Property(o => o.Price)
                .HasColumnType("decimal(5,2)");
                
            modelBuilder.Entity<Product>()
                .Property(p => p.ProductPrice)
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<Product>()
               .HasMany(p => p.Extras)
               .WithMany(e => e.Products)
               .UsingEntity(j => j.ToTable("ProductExtras"));

            modelBuilder.Entity<Orderable>()
               .HasMany(o => o.Products)
               .WithMany(p => p.Orderables)
               .UsingEntity(j => j.ToTable("OrderableProducts"));

            modelBuilder.Entity<Orderable>()
               .HasMany(o => o.SelectedExtras)
               .WithMany(e => e.Orderables)
               .UsingEntity(j => j.ToTable("OrderableExtras"));

            base.OnModelCreating(modelBuilder);
        }     
    }
}
