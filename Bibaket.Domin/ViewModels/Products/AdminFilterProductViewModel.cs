using Bibaket.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bibaket.Domain.ViewModels.Products
{
    public class AdminFilterProductViewModel:BasePaging<ProductViewModel>
    {
        [Display(Name ="عنوان")]
        public string Title { get; set; }

    }
}
