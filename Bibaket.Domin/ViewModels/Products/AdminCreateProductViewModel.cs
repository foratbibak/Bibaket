using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bibaket.Domain.ViewModels.Products
{
    public class AdminCreateProductViewModel
    {
        [Display(Name = "گروه")]
        [Required(ErrorMessage = "لطفا {0}وارد کنید ")]
        public int CategoryId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0}وارد کنید ")]
        public string Title { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0}وارد کنید ")]
        public double Price { get; set; }

        [Display(Name = "توضیح مختصر")]
        public string? ShortDescription { get; set; }

        [Display(Name = "نقد و بررسی")]
        public string? Review { get; set; }

        [Display(Name = "بررسی تخصصی")]
        public string? DeatilReview { get; set; }

        [Display(Name = "انتخاب تصویر")]
        public string? ImageName { get; set; }

        public IFormFile? ImageFile  { get; set; }

        [Display(Name = "گالری تصاویر")]

        public IFormFile[]? Gallaries { get; set; }


        [Display(Name = "موجودی انبار")]
        public int Count { get; set; }

        [Display(Name = "فعال است")]
        public bool IsActive { get; set; }
    }
}
