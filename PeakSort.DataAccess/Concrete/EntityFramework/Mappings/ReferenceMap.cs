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
    public class ReferenceMap : IEntityTypeConfiguration<References>
    {
        public void Configure(EntityTypeBuilder<References> builder)
        {
            builder.HasKey(r => r.ID);
            builder.Property(r => r.ID).ValueGeneratedOnAdd();
            builder.Property(r => r.Title).HasMaxLength(50).IsRequired();
            builder.Property(r => r.Description).HasColumnType("NVARCHAR(MAX)").IsRequired();
            builder.Property(r => r.Image).HasMaxLength(150).IsRequired();
                           
                            
            builder.Property(r => r.CreatedByName).HasMaxLength(50).IsRequired();
            builder.Property(r => r.ModifiedByName).HasMaxLength(50).IsRequired();
            builder.Property(r => r.CreatedDate).IsRequired();
            builder.Property(r => r.ModifiedDate).IsRequired();
            builder.Property(r => r.IsActive).IsRequired();
            builder.Property(r => r.IsDeleted).IsRequired();
            builder.Property(r => r.Note).HasMaxLength(50);

            builder.ToTable("References");

            builder.HasData(new References
            {
                ID = 1,
                Title = "Deneme1",
                Description = "Deneme1",
                Image = "Deneme1",
               

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
