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
	public class CustomUIMap : IEntityTypeConfiguration<CustomUI>
	{
		public void Configure(EntityTypeBuilder<CustomUI> builder)
		{
			builder.ToTable("CustomUIs");
			builder.HasKey(d => d.Id);
			builder.Property(d => d.Name)
				.IsRequired()
				.HasMaxLength(100);
			builder.Property(d => d.Title)
				.IsRequired()
				.HasMaxLength(100);
			builder.Property(d => d.UrlSlug)
				.IsRequired()
				.HasMaxLength(100);
			builder.Property(d => d.Color)
				.HasMaxLength(100);
		}

	}
}
