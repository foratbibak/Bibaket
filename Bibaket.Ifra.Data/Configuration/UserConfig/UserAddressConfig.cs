using Bibaket.Domin.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Ifra.Data.Configuration.UserConfig
{
    public class UserAddressConfig : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.Property(a => a.Title).HasMaxLength(300);
            builder.Property(a => a.PostalCode).HasMaxLength(300);
            builder.Property(a => a.Address).HasMaxLength(1500);
        }
    }
}
