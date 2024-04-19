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
            builder.HasOne(o => o.PaymentMethod)
                 .WithMany(p => p.Orders)
                 .HasForeignKey(p => p.PaymentMethodId)
                 .HasConstraintName("FK_PaymentMethod_Orders");
            builder.HasOne(o => o.OrderStatus)
                .WithMany(p => p.Order)
                .HasForeignKey(p => p.OrderStatusId)
                .HasConstraintName("FK_OrderStatus_Orders");
            builder.HasMany(o => o.OrderItems)
                .WithOne(i => i.Order)
                .HasForeignKey(o => o.OrderId)
                .HasConstraintName("FK_Order_OrderItems");
            builder.HasOne(o => o.ApplicationUser)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.ApplicationUserId);
			builder.HasOne(o => o.Discount)
				 .WithMany(p => p.Orders)
				 .HasForeignKey(p => p.DiscountId)
				 .HasConstraintName("FK_Discounts_Orders");

		}
    }
}
