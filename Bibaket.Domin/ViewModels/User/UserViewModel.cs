using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bibaket.Domain.ViewModels.User
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [DisplayName("نام")]
        public string? FirstName { get; set; }

        [DisplayName("نام خانوادگی")]
        public string? LastName { get; set; }

        [DisplayName("نام کاربری")]
        public string UserName { get; set; }

        [DisplayName("ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد فرمایید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }

        [DisplayName("شماره تلفن")]
        [Required(ErrorMessage = "لطفا {0} را وارد فرمایید")]
        public string? Mobile { get; set; }

        [DisplayName("کدملی")]
        public string? NationalCode { get; set; }

        [DisplayName("کلمه عبور")]
        public string Password { get; set; }

        [DisplayName("تصویر پروفایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد فرمایید")]
        public string? Avatar { get; set; }

        [DisplayName("فعال/غیرفعال")]
        public bool IsActive { get; set; }

        [DisplayName("اخرین فعالیت")]
        public DateTime? LastLoginDate { get; set; }

        [DisplayName("تاریخ ایجاد")]
        public DateTime CreatDate { get; set; }

        [DisplayName("تاریخ ویرایش")]
        public DateTime? UpdateDate { get; set; }



    }
}
