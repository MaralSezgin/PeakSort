using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeakSort.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakSort.DataAccess.Concrete.EntityFramework.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ID);
            builder.Property(p => p.ID).ValueGeneratedOnAdd();
            builder.Property(p => p.Title).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Description).HasColumnType("NVARCHAR(MAX)").IsRequired();
            builder.Property(p => p.date).HasMaxLength(250);
            builder.Property(p => p.Thumbnail).HasMaxLength(150).IsRequired();
            builder.Property(p => p.SeoDescription).HasMaxLength(500).IsRequired();
            builder.Property(p => p.SeoTags).HasMaxLength(150).IsRequired();

            builder.Property(p => p.CreatedByName).HasMaxLength(50).IsRequired();
            builder.Property(p => p.ModifiedByName).HasMaxLength(50).IsRequired();
            builder.Property(p => p.CreatedDate).IsRequired();
            builder.Property(p => p.ModifiedDate).IsRequired();
            builder.Property(p => p.IsActive).IsRequired();
            builder.Property(p => p.IsDeleted).IsRequired();
            builder.Property(p => p.Note).HasMaxLength(50);

            builder.ToTable("Products");

            builder.HasData(new Product
            {
                ID = 1,
                Title = "Deneme1",
                Description = "Deneme1",
                date =DateTime.Now,
                Thumbnail = "Deneme1",
                SeoDescription = "Deneme1",
                SeoTags = "Deneme1",
                CategoryID=1,
                
           

                CreatedByName = "sezgin",
                ModifiedByName = "sezgin",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsActive = true,
                IsDeleted = false,
                Note = "Note deneme",

            });
        }
    }
}
