using Bibaket.Domain.Models.Categories;
using Bibaket.Domin.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bibaket.Domain.Models.Products
{
    public class Product:BaseEntity
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }

        public double Price { get; set; }

        public string? ShortDescription { get; set; }

        public string? Review { get; set; }

        public string? DeatilReview { get; set; }

        public string? ImageName { get; set; }

        public bool IsActive { get; set; }


        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

    }
}
