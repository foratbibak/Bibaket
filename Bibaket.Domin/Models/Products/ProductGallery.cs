using Bibaket.Domin.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Domain.Models.Products
{
    public class ProductGallery:BaseEntity
    {
        public int ProductId { get; set; }

        public string Alt { get; set; }

        public string ImageName { get; set; }

        public Product? Product { get; set; }
    }
}
