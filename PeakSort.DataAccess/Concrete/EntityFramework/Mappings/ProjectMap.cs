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
    public class ProjectMap : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(p => p.ID);
            builder.Property(p => p.ID).ValueGeneratedOnAdd();
            builder.Property(p => p.Title).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Description).HasColumnType("NVARCHAR(MAX)").IsRequired();
            builder.Property(p => p.Image).HasMaxLength(150).IsRequired();
       

            builder.Property(p => p.CreatedByName).HasMaxLength(50).IsRequired();
            builder.Property(p => p.ModifiedByName).HasMaxLength(50).IsRequired();
            builder.Property(p => p.CreatedDate).IsRequired();
            builder.Property(p => p.ModifiedDate).IsRequired();
            builder.Property(p => p.IsActive).IsRequired();
            builder.Property(p => p.IsDeleted).IsRequired();
            builder.Property(p => p.Note).HasMaxLength(50);

            builder.ToTable("Projects");

            builder.HasData(new Project
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
