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
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comment");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.UserName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.UrlSlug)
               .IsRequired()
               .HasMaxLength(50);
            builder.Property(c => c.Content)
               .IsRequired()
               .HasMaxLength(500);
            builder.Property(c => c.Status)
                .HasDefaultValue(false);
            builder.Property(c => c.Created)
                .HasColumnType("datetime");
           

        }
    }
}
