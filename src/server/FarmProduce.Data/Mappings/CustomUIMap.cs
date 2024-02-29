using FarmProduce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Data.Mappings
{
    public class CustomUIMap : IEntityTypeConfiguration<CustomUI>
    {
        public void Configure(EntityTypeBuilder<CustomUI> builder)
        {
            builder.ToTable("CustomUI");
            builder.HasKey(c => c.Id);
            builder.Property(c=> c.Title)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c=> c.Image)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(c => c.Color)
              .IsRequired()
              .HasMaxLength(50);

        }
    }
}
