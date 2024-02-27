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
    public class OrderDetailMap : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UnitPrice)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(x => x.Quantity)
                .IsRequired()
                .HasDefaultValue(0);
            builder.HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId)
                .HasConstraintName("FK_OrderDetails_Product");
            builder.HasOne(od => od.Order)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .HasConstraintName("FK_OrderDetails_Order");
        }
    }
}
