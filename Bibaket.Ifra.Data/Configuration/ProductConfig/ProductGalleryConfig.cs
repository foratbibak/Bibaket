using Bibaket.Domain.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Ifra.Data.Configuration.ProductConfig
{
    public class ProductGalleryConfig: IEntityTypeConfiguration<ProductGallery>
    {
        public void Configure(EntityTypeBuilder<ProductGallery> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.ProductId).IsRequired();

            builder.Property(c => c.ImageName).HasMaxLength(200).IsRequired();

            builder.Property(c => c.Alt).HasMaxLength(200).IsRequired();
        }
    }
}
