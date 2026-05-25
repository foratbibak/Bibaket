using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bibaket.Domin.ViewModels.Account
{
    public class LoginViewModel
    {
        [DisplayName("نام کاربری یا ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد فرمایید")]
        public string UserNameOrEmail { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "مرابخاطر بسپار")]
        public bool RememberMe { get; set; }

        public string? ImageData { get; set; }

        [Display(Name = "عبارت امنیتی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CaptchaAnswer { get; set; }
    }
}
