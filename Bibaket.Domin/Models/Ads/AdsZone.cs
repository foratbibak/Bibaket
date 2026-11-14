using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bibaket.Domain.Models.Ads
{
    public class AdsZone
    {
        [Key]
        public int Id { get; set; }
        [Display(Name= "عنوان")]
        [Required(ErrorMessage ="لطفا محل تبلیغات را وارد کنید")]
        [MaxLength(200,ErrorMessage ="حداکثر طول محل تبلغات 200 کاراکتر می باشد")]
        public string Title { get; set; }


        public List<Ads>? Ads { get; set; }
    }
}
