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
    public class AdminMap : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("Admin");
            builder.HasKey(b => b.Id);
            builder.Property(b => b.FullName)
              .IsRequired()
              .HasMaxLength(20);
            builder.Property(b => b.Password)
             .IsRequired()
             .HasMaxLength(50);
            builder.Property(b => b.Email)
             .IsRequired()
             .HasMaxLength(50);
			builder.Property(b => b.UrlSlug)
			 .IsRequired()
			 .HasMaxLength(50);
		}
    }
}
