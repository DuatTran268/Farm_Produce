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
    public class UnitMap : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.ToTable("Units");
            builder.HasKey(x => x.Id);
            builder.Property(x=> x.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.UrlSlug)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
