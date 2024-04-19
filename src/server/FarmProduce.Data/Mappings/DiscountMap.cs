using FarmProduce.Core.Contracts;
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
    public class DiscountMap : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable("Discounts");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.DiscountPrice)
                .IsRequired();
            builder.Property(d => d.StartDate)
                .HasDefaultValue(DateTime.Now)
                .HasColumnType("datetime");
            builder.Property(d => d.EndDate)
                .HasColumnType("datetime");
            builder.Property(d => d.CodeName)
				.IsRequired()
				.HasMaxLength(50);
		}
    }
}
