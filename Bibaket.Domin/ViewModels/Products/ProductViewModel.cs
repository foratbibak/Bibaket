using Bibaket.Domain.Models.Categories;
using Bibaket.Domain.Models.Products;
using Sofarashel.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bibaket.Domain.ViewModels.Products
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Title { get; set; }

        public double Price { get; set; }

        public double? PriceWithDisCount { get; set; }

        public int? DisCountPrecent { get; set; }

        public string? ShortDescription { get; set; }

        public string? Review { get; set; }

        public string? DeatilReview { get; set; }

        public string? ImageName { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        public DateTime CreateDate { get; set; }

        public List<ProductColor>? ProductColors { get; set; }

        public List<ProductFeature>? ProductFeatures { get; set; }

        public List<ProductGallery>? ProductGalleries { get; set; }
    }
}
