using FarmProduce.Core.Entities;
using FarmProduce.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Data.Contexts
{
    public class FarmDbContext: DbContext
    {
        public DbSet<Admin> Admins { get; set; }    
        public DbSet<Customer> Buyers { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CollectionImage> CollectionImages { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<PaymentOption> PaymentOptions { get; set; }
        public DbSet<Products> Products { get; set; }


		public FarmDbContext(DbContextOptions<FarmDbContext> options) : base(options) { }

		public FarmDbContext()
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=XUANHUNG\\SQLEXPRESS;Database=FarmProduct;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
			//optionsBuilder.UseSqlServer("Server=DESKTOP-NLUPE1I\\MSSQLSERVER01;Database=FarmProduct;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");

		}
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryMap).Assembly);
            modelBuilder.Entity<Products>().Property(p=> p.Price).HasPrecision(18,2);
            modelBuilder.Entity<Products>().Property(p => p.PriceDiscount).HasPrecision(18, 2);

        }
    }
}
