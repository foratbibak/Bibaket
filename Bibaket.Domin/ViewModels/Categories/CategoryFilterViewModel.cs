using Bibaket.Domain.Enums.User;
using Bibaket.Domain.ViewModels.Common;
using Bibaket.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bibaket.Domain.ViewModels.Categories
{
    public class CategoryFilterViewModel : BasePaging<CategoryViewModel>
    {
        public string ParentId { get; set; }

        [Display(Name ="عنوان دسته بندی")]
        public string Title { get; set; }

        [Display(Name = "وضعیت حذف")]
        public FilterDeleteStatus DeleteStatus { get; set; }

        
    }
}
