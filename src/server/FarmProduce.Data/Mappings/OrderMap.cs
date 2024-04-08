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
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(o=> o.Id);
            builder.Property(o => o.DateOrder)
                .IsRequired()
                .HasDefaultValue(DateTime.Now)
                .HasColumnType("datetime");
            builder.Property(o => o.TotalPrice)
               .IsRequired()
               .HasDefaultValue(0);
            builder.HasMany(o => o.PaymentMethods)
                 .WithOne(p => p.Order)
                 .HasForeignKey(p => p.OrderId)
                 .HasConstraintName("FK_PaymentMethods_Order");
            builder.HasMany(o => o.OrderStatuses)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId)
                .HasConstraintName("FK_OrderStatuses_Order");
            builder.HasMany(o => o.OrderItems)
                .WithOne(i => i.Order)
                .HasForeignKey(o => o.OrderId)
                .HasConstraintName("FK_Order_OrderItems");
            builder.HasOne(o => o.ApplicationUser)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.ApplicationUserId);
        }
    }
}
