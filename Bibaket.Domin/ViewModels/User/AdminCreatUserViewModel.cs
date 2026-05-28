using Bibaket.Domain.Models.Roles;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bibaket.Domain.ViewModels.User
{
    public class AdminCreatUserViewModel
    {

        [DisplayName("نام")]
        public string? FirstName { get; set; }

        [DisplayName("نام خانوادگی")]
        public string? LastName { get; set; }

        [DisplayName("نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد فرمایید")]
        public string UserName { get; set; }

        [DisplayName("ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد فرمایید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }

        [DisplayName("شماره تلفن")]
        [RegularExpression("^09[0-9]{9}$", ErrorMessage = "شماره وارد شده صحیح نیست فرمت مثال 09146878657")]
        public string? Mobile { get; set; }

        [DisplayName("کدملی")]
        public string? NationalCode { get; set; }

        [DisplayName("کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد فرمایید")]
        public string Password { get; set; }

        [DisplayName("تصویر پروفایل")]
        public string? Avatar { get; set; }


        public IFormFile? AvatarFile { get; set; }

        [DisplayName("فعال/غیرفعال")]
        public bool IsActive { get; set; }

        public List<Role>? Roles { get; set; }
        public List<int>? UserSelectedRoles { get; set; }
    }
}
