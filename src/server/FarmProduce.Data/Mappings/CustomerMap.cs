using FarmProduce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Data.Mappings
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.Email)
              .IsRequired()
              .HasMaxLength(50);
            builder.Property(c => c.Address)
              .IsRequired()
              .HasMaxLength(100);
            builder.Property(c => c.Phone)
              .IsRequired()
              .HasMaxLength(15);
            builder.HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .HasConstraintName("FK_Orders_Customer");
            builder.HasMany(c => c.Comments)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .HasConstraintName("FK_Comments_Customer");


        }
    }
}
