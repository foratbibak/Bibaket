using Bibaket.Domain.Enums.User;
using Bibaket.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bibaket.Domain.ViewModels.User
{
    public class AdminFilterUserViewModel:BasePaging<UserViewModel>
    {
        [DisplayName("نام")]
        public string? FirstName { get; set; }

        [DisplayName("نام خانوادگی")]
        public string? LastName { get; set; }

        [DisplayName("نام کاربری")]
        public string UserName { get; set; }

        [DisplayName("ایمیل")]
        public string Email { get; set; }

        [DisplayName("شماره تلفن")]
        public string? Mobile { get; set; }

        [DisplayName("کدملی")]
        public string? NationalCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        [Display(Name ="وضعیت حذف")]
        public FilterDeleteStatus DeleteStatus { get; set; }

    }
}
