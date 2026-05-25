using Bibaket.Application.Extensions;
using Bibaket.Application.Generator;
using Bibaket.Application.Security;
using Bibaket.Domin.Models.Users;
using Bibaket.Domin.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Application.Mapper
{
    public static class UserMapper
    {
        public static User MapToUser(RegisterViewModel model)
        {
            return new User()
            {
                UserName = model.UserName.FixUserName(),
                Email = model.Email.FixEmail(),
                Avatar = "NoPhoto.jpg",
                CreatDate = DateTime.Now,
                EmailActiveCode = NameGenerator.GenerateUniqName(),
                IsActive = true,
                IsEmailActive = false,
                Password =PasswordHelper.EncodePasswordMd5(model.Password),
            };
        }
    }
}
