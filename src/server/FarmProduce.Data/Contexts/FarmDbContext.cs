using FarmProduce.Core.Entities;
using FarmProduce.Data.Mappings;
using FarmProduce.Data.Seeders;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Data.Contexts
{
    public class FarmDbContext:IdentityDbContext<
        ApplicationUser>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
      
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
       public DbSet<CustomUI> CustomUIs { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }



        public FarmDbContext(DbContextOptions<FarmDbContext> options)
        : base(options)
        {
        }
        public FarmDbContext()
		{
		}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=XUANHUNG;Database=FarmProducts;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
            //optionsBuilder.UseSqlServer("Server=DESKTOP-NLUPE1I\\MSSQLSERVER01;Database=FarmProductsV5;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
            modelBuilder.Entity<Product>().Property(p=> p.Price).HasPrecision(18,2);
            modelBuilder.Entity<Discount>().Property(p => p.DiscountPrice).HasPrecision(18, 2);
         
        }
    }
}
