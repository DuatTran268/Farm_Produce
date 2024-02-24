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
            builder.ToTable("Order");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(o => o.UrlSlug)
               .IsRequired()
               .HasMaxLength(50);
            builder.Property(o => o.DateOrder)
              .HasDefaultValue(DateTime.Now)
              .HasColumnType("datetime");
              
            builder.Property(o => o.Note)
               .IsRequired()
               .HasMaxLength(200);
            builder.HasMany(o => o.PaymentOptions)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId)
                .HasConstraintName("FK_Order_PaymentOptions")
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(o => o.Products)
              .WithOne(p => p.Order)
              .HasForeignKey(p => p.OrderId)
              .HasConstraintName("FK_Order_Products")
              .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Buyer)
              .WithMany(u => u.Orders)
              .HasForeignKey(o => o.BuyerId)
              .HasConstraintName("FK_Buyer_Orders")
              .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(o => o.OrderStatuses)
            .WithOne(p => p.Order)
            .HasForeignKey(p => p.OrderId)
            .HasConstraintName("FK_Order_OrderStatuses")
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
