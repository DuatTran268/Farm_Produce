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
    public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x=> x.Id);
            builder.Property(x=>x.Quantity)
                .HasDefaultValue(0)
                .IsRequired();
            builder.Property(x => x.Price)
               .HasDefaultValue(0)
               .IsRequired();
        }
    }
}
