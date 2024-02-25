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
    public class CollectionImageMap : IEntityTypeConfiguration<CollectionImage>
    {
        public void Configure(EntityTypeBuilder<CollectionImage> builder)
        {
            builder.ToTable("CollectionImage");
            builder.HasKey(ci => ci.Id);
            builder.Property(ci => ci.Name)
                .IsRequired()
                .HasMaxLength(350);
            builder.Property(ci => ci.UrlSlug)
                .IsRequired()
                .HasMaxLength(350);
        }
    }
}
