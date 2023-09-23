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
    public class AboutMap : IEntityTypeConfiguration<About>
    {
        public void Configure(EntityTypeBuilder<About> builder)
        {
            builder.HasKey(a => a.ID);
            builder.Property(a => a.ID).ValueGeneratedOnAdd();
            builder.Property(a => a.Title).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Description).HasColumnType("NVARCHAR(MAX)").IsRequired();
            builder.Property(a => a.Image).HasMaxLength(250).IsRequired();
            builder.Property(a => a.VideoLink).HasMaxLength(500);

            builder.Property(a => a.CreatedByName).HasMaxLength(50).IsRequired();
            builder.Property(a => a.ModifiedByName).HasMaxLength(50).IsRequired();
            builder.Property(a => a.CreatedDate).IsRequired();
            builder.Property(a => a.ModifiedDate).IsRequired();
            builder.Property(a => a.IsActive).IsRequired();
            builder.Property(a => a.IsDeleted).IsRequired();
            builder.Property(a => a.Note).HasMaxLength(50);

            builder.ToTable("Abouts");

            builder.HasData(new About
            {
                ID=1,
                Title="About Title",
                Description="About Description",
                Image="Default.jpg",
                VideoLink= "deneme",

                CreatedByName="sezgin",
                ModifiedByName="sezgin",
                CreatedDate=DateTime.Now,
                ModifiedDate=DateTime.Now,
                IsActive=true,
                IsDeleted=false,
                Note="Note deneme",

            });;
        }
    }
}
