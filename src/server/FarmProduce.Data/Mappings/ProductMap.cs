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
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.UrlSlug)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.QuanlityAvailable)
                .IsRequired();
            builder.Property(x => x.Price)
                .IsRequired()
                .HasPrecision(2,18);
            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.Status)
                .IsRequired()
                .HasDefaultValue(false);
            builder.Property(x => x.DateCreate)
                .IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValue(DateTime.Now);
            builder.Property(x => x.DateUpdate)
               .HasColumnType("datetime");
            builder.HasMany(p => p.Images)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId)
                .HasConstraintName("FK_Images_Product");
            builder.HasMany(p => p.Comments)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId)
                .HasConstraintName("FK_Comments_Product");
            builder.HasMany(o => o.OrderItems)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductId)
                .HasConstraintName("FK_Product_OrderItems");
            builder.HasOne(p => p.Unit)
                .WithMany(u => u.Products)
                .HasForeignKey(p => p.UnitId)
                .HasConstraintName("FK_Units_Product");
        }
    }
}