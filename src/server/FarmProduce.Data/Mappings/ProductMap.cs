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
	public class ProductMap : IEntityTypeConfiguration<Products>
	{
		public void Configure(EntityTypeBuilder<Products> builder)
		{
			builder.ToTable("Product");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Name)
				.IsRequired()
				.HasMaxLength(200);
			builder.Property(p => p.UrlSlug)
			  .IsRequired()
			  .HasMaxLength(200);
			builder.Property(p => p.Image)
			  .IsRequired()
			  .HasMaxLength(200);
			builder.Property(p => p.Price)
			  .IsRequired();

			builder.Property(p => p.ShortDescription)
			  .IsRequired()
			  .HasMaxLength(500);

			builder.Property(p => p.Description)
			  .IsRequired()
			  .HasMaxLength(2500);

			builder.Property(p => p.PriceDiscount);

			builder.Property(p => p.DateCreate)
				.HasDefaultValue(DateTime.Now)
				.HasColumnType("datetime");

			builder.Property(p => p.DateUpdate)
				.HasDefaultValue(DateTime.Now)
				.HasColumnType("datetime");

			builder.Property(p => p.Status)
				.HasDefaultValue(false);
			builder.HasOne(p => p.Category)
				.WithMany(c => c.Products)
				.HasForeignKey(p => p.CategoryId)
				.HasConstraintName("FK_Products_Categoies")
				.OnDelete(DeleteBehavior.Cascade);
			builder.HasMany(p => p.Comments)
				.WithOne(c => c.Product)
				.HasForeignKey(c => c.ProductId)
				.HasConstraintName("FK_Comments_Products")
				.OnDelete(DeleteBehavior.Cascade);
			builder.HasMany(p => p.CollectionImages)
				.WithOne(c => c.Product)
				.HasForeignKey(c => c.ProductId)
				.HasConstraintName("FK_CollectionImages_Products")
				.OnDelete(DeleteBehavior.Cascade);



		}
	}
}
