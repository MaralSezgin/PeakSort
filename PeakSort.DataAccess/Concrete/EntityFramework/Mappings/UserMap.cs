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
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(r => r.ID);
            builder.Property(u => u.ID).ValueGeneratedOnAdd();
            builder.Property(u => u.Email).HasMaxLength(50).IsRequired();
            builder.HasIndex(u => u.Email).IsUnique();//bu emailden bir tane daha oluşturulamaz
            builder.Property(u => u.UserName).HasMaxLength(20).IsRequired();
            builder.HasIndex(u => u.UserName).IsUnique();
            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(u => u.PasswordHash).HasColumnType("VARBINARY(500)");
            builder.Property(u => u.FirstName).HasMaxLength(30).IsRequired();
            builder.Property(u => u.LastName).HasMaxLength(30).IsRequired();
            builder.Property(u => u.Picture).HasMaxLength(250).IsRequired();

            builder.HasOne<Role>(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleID);

            //builder.Property(u=>u.CreatedByName).HasColumnName("CREATED_NAME")//eger veri tabanında "CREATED_NAME" adında bir sutun varsa onunla bunu map ediyor

            builder.Property(u => u.CreatedByName).HasMaxLength(50).IsRequired();
            builder.Property(u => u.ModifiedByName).HasMaxLength(50).IsRequired();
            builder.Property(u => u.CreatedDate).IsRequired();
            builder.Property(u => u.ModifiedDate).IsRequired();
            builder.Property(u => u.IsActive).IsRequired();
            builder.Property(u => u.IsDeleted).IsRequired();
            builder.Property(u => u.Note).HasMaxLength(50);

            builder.ToTable("Users");

            builder.HasData(new User
            {
                ID = 1,
                Email = "Deneme1",
                UserName = "Deneme1",
                PasswordHash = Encoding.ASCII.GetBytes( "Deneme1"),
                FirstName = "Deneme1",
                LastName = "Deneme1",
                Picture = "Deneme1",
                RoleID=1,
          


                CreatedByName = "Berivan",
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
