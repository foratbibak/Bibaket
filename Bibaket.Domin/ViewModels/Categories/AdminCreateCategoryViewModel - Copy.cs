using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bibaket.Domain.ViewModels.Categories
{
    public class AdminCreateCategoryViewModel
    {
        public int? ParentId { get; set; }

        [Display(Name = "عنوان دسته بندی")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name ="آدرس بار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Slug { get; set; }

        public string? CategoryParentTitle { get; set; }

        [Display(Name = "تصویر دسته")]

        public IFormFile? ImageFile { get; set; }

        public string? ImageName { get; set; }

    }
}
