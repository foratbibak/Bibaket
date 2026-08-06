using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bibaket.Domain.ViewModels.Categories
{
    public class CategoryViewModel
    {

        public int Id { get; set; }

        public int? ParentId { get; set; }

        [Display(Name ="عنوان")]
        public string Title { get; set; }

        public string? ImageName { get; set; }

        public bool IsDeleted { get; set; }

    }
}