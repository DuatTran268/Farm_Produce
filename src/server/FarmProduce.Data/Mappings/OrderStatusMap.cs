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
    public class OrderStatusMap : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.ToTable("OrderStatuses");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.StatusCode)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Description)
               .IsRequired()
               .HasMaxLength(100);
            builder.Property(x => x.StatusDate)
               .IsRequired()
               .HasColumnType("Datetime");

        }
    }
}
