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
            builder.ToTable("Comments");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.Rating)
               .IsRequired()
               .HasDefaultValue(5);
            builder.Property(x => x.Created)
             .HasDefaultValue(DateTime.Now)
             .HasColumnType("datetime");

            builder.Property(x => x.CommentText)
               .IsRequired()
               .HasMaxLength(200);
            builder.Property(x => x.Status)
               .IsRequired()
              .HasDefaultValue(false);






        }
    }
}
