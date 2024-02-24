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
    public class PaymentOptionMap : IEntityTypeConfiguration<PaymentOption>
    {
        public void Configure(EntityTypeBuilder<PaymentOption> builder)
        {
            builder.ToTable("PaymentOption");
            builder.HasKey(x => x.Id);
            builder.Property(x=> x.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.UrlSlug)
                .IsRequired()
                .HasMaxLength(100);

        }
    }
}
