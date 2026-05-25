using Bibaket.Domin.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Ifra.Data.Configuration.UserConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            #region Key
            builder.HasKey(u => u.Id);
            #endregion
            #region Validations
            builder.Property(u => u.FirstName).HasMaxLength(200);
            builder.Property(u => u.LastName).HasMaxLength(200);
            builder.Property(u => u.UserName).IsRequired().HasMaxLength(200);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(200);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(400);
            builder.Property(u => u.IsEmailActive).HasMaxLength(50);
            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.UserName).IsUnique();
            builder.HasIndex(u => u.Mobile).IsUnique();
            #endregion

            #region Realtions
            builder.HasMany(r => r.UserInRole).WithOne(r => r.User).HasForeignKey(r => r.UserId);
            #endregion
        }
    }
}
