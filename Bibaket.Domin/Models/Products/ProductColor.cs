using Bibaket.Domain.Models.Products;
using Bibaket.Domin.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sofarashel.Domain.Models.Products
{
    public class ProductColor:BaseEntity
    {
        public int ProductId { get; set; }
        [Display(Name="عنوان")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string Name { get; set; }
        [Display(Name="کد رنگ")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string Code { get; set; }
        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public double Price { get; set; }
        [Display(Name = "پیش فرض")]

        public bool IsDefault { get; set; }
        [Display(Name = "موجودی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Quntity { get; set; }

        public Product? Product { get; set; }
    }
}
