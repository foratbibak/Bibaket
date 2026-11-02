using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bibaket.Domin.Models.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }
        [Display(Name="تاریخ ایجاد ")]
        public DateTime CreatDate { get; set; }

        [Display(Name = "تاریخ ویرایش")]
        public DateTime? UpdateDate { get; set; }

        [Display(Name = "تاریخ حذف شده")]
        public DateTime? DeleteDate { get; set; }

        [Display(Name = "حذف شده")]
        public bool IsDelete { get; set; }
    }
}
