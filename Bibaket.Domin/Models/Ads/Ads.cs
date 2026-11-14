using Bibaket.Domin.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bibaket.Domain.Models.Ads
{
    public class Ads:BaseEntity
    {
        [Display(Name = "محل نمایش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int ZoneId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "آدرس")]
        [Url(ErrorMessage = "آدرس وارد شده معتبر نمی باشد ")]
        public string? Url { get; set; }

        [Display(Name = "تصویر")]
        public string? ImageName { get; set; }

        [Display(Name = "در برگه جدید باز شود ؟")]
        public bool IBlank { get; set; }

        [Display(Name = "فعال است ")]
        public bool IsActive { get; set; }

        [Display(Name = "تاریخ شروع نمایش")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "تاریخ پایان نمایش ")]
        public DateTime? ExpireDate { get; set; }
        [Display(Name = "تعداد بازدید")]
        public int VisitCount { get; set; }


        [ForeignKey("ZoneId")]
        public AdsZone? AdsZone { get; set; }
    }
}
