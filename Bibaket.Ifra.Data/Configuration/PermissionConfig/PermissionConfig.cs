using Bibaket.Domain.Models.Permission;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Ifra.Data.Configuration.PermissionConfig
{
    public class PermissionConfig : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.UniqName).HasMaxLength(200).IsRequired();
            builder.Property(x=>x.DisplayName).HasMaxLength(200).IsRequired();


            builder.HasMany(x => x.RolePermissionConfigs).WithOne(x => x.Permission).HasForeignKey(x => x.PermissionId);

            builder.HasOne(x => x.Parent).WithMany().HasForeignKey(x => x.ParentId);
        }
    }
    
    
}
