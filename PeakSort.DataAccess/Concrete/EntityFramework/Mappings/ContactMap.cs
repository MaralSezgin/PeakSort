using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeakSort.Entities.Concrete;

namespace PeakSort.DataAccess.Concrete.EntityFramework.Mappings
{
    public class ContactMap : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(c=>c.ID);
            builder.Property(c => c.ID).ValueGeneratedOnAdd();
            builder.Property(c => c.Address).HasMaxLength(500);
            builder.Property(c => c.Address1).HasMaxLength(500);
            builder.Property(c => c.Phone).HasMaxLength(15);
            builder.Property(c => c.Fax).HasMaxLength(15);
            builder.Property(c => c.MobilePhone).HasMaxLength(15);
            builder.Property(c => c.MobilePhone1).HasMaxLength(500);
            builder.Property(c => c.Email).HasMaxLength(50);
            builder.Property(c => c.MapLink).HasMaxLength(500);
            builder.Property(c => c.Image).HasMaxLength(250);

            builder.Property(c => c.CreatedByName).HasMaxLength(50).IsRequired();
            builder.Property(c => c.ModifiedByName).HasMaxLength(50).IsRequired();
            builder.Property(c => c.CreatedDate).IsRequired();
            builder.Property(c => c.ModifiedDate).IsRequired();
            builder.Property(c => c.IsActive).IsRequired();
            builder.Property(c => c.IsDeleted).IsRequired();
            builder.Property(c => c.Note).HasMaxLength(50);

            builder.ToTable("Contacts");

            builder.HasData(new Contact
            {
                ID = 1,
                Address = "Deneme1",
                Address1 = "Deneme1",
                Phone = "Deneme1",
                Fax = "Deneme1",
                MobilePhone = "Deneme1",
                MobilePhone1 = "Deneme1",
                Email = "Deneme1",
                MapLink = "Deneme1",
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
