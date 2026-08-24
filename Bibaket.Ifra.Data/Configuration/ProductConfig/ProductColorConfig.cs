using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sofarashel.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Ifra.Data.Configuration.ProductConfig
{
    public class ProductColorConfig : IEntityTypeConfiguration<ProductColor>
    {
        public void Configure(EntityTypeBuilder<ProductColor> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.ProductId).IsRequired();

            builder.Property(c => c.Name).HasMaxLength(200).IsRequired();

            builder.Property(c => c.Code).HasMaxLength(200).IsRequired();


        }
    }
}
