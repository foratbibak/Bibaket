using Bibaket.Domin.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bibaket.Domain.Models.Products
{
    public class ProductFeature:BaseEntity
    {
        public int ProductId { get; set; }

        [Display(Name="عنوان")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "مقدار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Value { get; set; }

        public Product? Product { get; set; }
    }
}
