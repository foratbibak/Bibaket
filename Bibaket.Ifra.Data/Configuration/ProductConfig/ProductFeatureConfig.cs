using Bibaket.Domain.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Ifra.Data.Configuration.ProductConfig
{
    public class ProductFeatureConfig : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.ProductId).IsRequired();

            builder.Property(c => c.Name).HasMaxLength(200).IsRequired();

            builder.Property(c => c.Value).IsRequired();
        }
    }
}
