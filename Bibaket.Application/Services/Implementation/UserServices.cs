using Bibaket.Application.Generator;
using Bibaket.Application.Mapper;
using Bibaket.Application.Security;
using Bibaket.Application.Services.Interfaces;
using Bibaket.Domain.Enums.User;
using Bibaket.Domain.ViewModels.User;
using Bibaket.Domin.Contracts;
using Bibaket.Domin.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Application.Services.Implementation
{
    public class UserServices(IUserRepository userRepository) : IUserServices
    {
        public async Task<AdminCreateUserResult> CreatUserInAdmin(AdminCreatUserViewModel model)
        {
            #region Validations
            try
            {
                if (string.IsNullOrEmpty(model.UserName) &&
                    string.IsNullOrEmpty(model.Email) &&
                    string.IsNullOrEmpty(model.Password))
                {
                    return AdminCreateUserResult.Error;
                }
                if (await userRepository.IsExistEmailAsync(model.Email))
                {
                    return AdminCreateUserResult.EmailDuplicated;
                }
                if (await userRepository.IsExsitUserNameAsync(model.UserName))
                {
                    return AdminCreateUserResult.UserNameDuplicated;
                }
                if (!string.IsNullOrEmpty(model.Mobile))
                {
                    if (await userRepository.IsExistMobileAsync(model.Mobile))
                    {
                        return AdminCreateUserResult.MobileDuplicated;
                    }
                }
                if (!string.IsNullOrEmpty(model.NationalCode))
                {
                    if (await userRepository.IsExistNationalAsync(model.NationalCode))
                    {
                        return AdminCreateUserResult.NationalCodeDuplicated;
                    }
                }
                if (model.AvatarFile?.ImageValidate() == false)
                {
                    return AdminCreateUserResult.InvalidImage;
                }
            }
            catch (DbUpdateException)
            {
                return AdminCreateUserResult.DatabaseError;
            }
            catch (Exception)
            {

                return AdminCreateUserResult.UnknownError;
            }
            #endregion

            #region Save Avatar
            var avatarName=SaveImageFileAsync(model.AvatarFile);
            #endregion

            #region CreateUser
            User user=UserMapper.MapToUser(model);
            userRepository.CreatAsync(user);
            userRepository.SaveAsync();
            if (model.UserSelectedRoles!=null&&model.UserSelectedRoles.Any())
            {
                await userRepository.AddUserToRole(user.Id, model.UserSelectedRoles);
                await userRepository.SaveAsync();
            }
            #endregion
            return AdminCreateUserResult.Success;
        }

        #region Utilites
        private async Task<string> SaveImageFileAsync(IFormFile file)
        {
            if (file == null) return "NoPhoto.jpg";
            var AvatarName = NameGenerator.GenerateUniqName() +
                Path.GetExtension(file.FileName);

            string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Avatars", AvatarName);
            using (var stream = System.IO.File.Create(savePath))
            {
                await file.CopyToAsync(stream);
            }

            return AvatarName;
        }
        #endregion
    }
}
