using Bibaket.Domain.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Ifra.Data.Configuration.ProductConfig
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p=>p.Title).HasMaxLength(300).IsRequired();

            builder.Property(p => p.CategoryId).IsRequired();

            builder.Property(p=>p.Price).IsRequired();
        }
    }
}
