using Bibaket.Application.Extensions;
using Bibaket.Application.Generator;
using Bibaket.Application.Mapper;
using Bibaket.Application.Security;
using Bibaket.Application.Services.Interfaces;
using Bibaket.Domain.Enums.Account;
using Bibaket.Domain.ViewModels.Account;
using Bibaket.Domin.Contracts;
using Bibaket.Domin.Models.Users;
using Bibaket.Domin.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Application.Services.Implementation
{
    public class AccountServices(IUserRepository _userRepository) : IAccountServices
    {
        public async Task<bool> ActiveAccountAsync(string activecode)
        {
            var user = await _userRepository.GetUserByActiveCodeAsync(activecode);
            if (user == null) return false;
            user.IsEmailActive = true;
            user.EmailActiveCode = NameGenerator.GenerateUniqName();

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveAsync();

            return true;
        }

        public async Task<bool> ChangePassword(int UserId, ChangePasswordViewModel changePassword)
        {
            var user = await _userRepository.GetUserbyIdAsync(UserId);
            if ((user == null))
                throw new Exception("کاربر یافت نشد");
            if (!PasswordHelper.VerifyPassword(changePassword.OldPassword, user.Password))
                return false;
            user.Password = PasswordHelper.EncodePasswordMd5(changePassword.Password);
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveAsync();
            return true;


        }

        public async Task<bool> EditProfile(int userId, ProfileViewModel profile)
        {
            var user = await _userRepository.GetUserbyIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            user.FirstName = profile.FirstName;
            user.LastName = profile.LastName;
            user.Mobile = profile.Mobile;
            user.NationalCode = profile.NationalCode;
            user.Avatar=profile.Avatar;
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveAsync();
            return true;

        }

        public async Task<User?> GetUserByEmailOrUserName(string emailorUserName)
        {
            return await _userRepository.GetUserByEmailOrUserName(emailorUserName);
        }

        public async Task<ProfileViewModel> GetUserProfile(int userId)
        {
            var user = await _userRepository.GetUserbyIdAsync(userId);
            return new ProfileViewModel()
            {
                Avatar = user.Avatar,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Mobile = user.Mobile,
                NationalCode = user.NationalCode,
            };
        }

        public async Task<bool> IsCompleteProfile(int UserId)
        {
            var user = await _userRepository.GetUserbyIdAsync(UserId);
            if (user.Mobile != null && user.Mobile != "")
            {
                return true;
            }
            return false;
        }

        public async Task<LoginUserResult> LoginUserAsync(LoginViewModel model)
        {
            var user = await _userRepository.GetUserByEmailOrUserName(model.UserNameOrEmail);
            if (user == null)
                return LoginUserResult.NotFound;
            if (!PasswordHelper.VerifyPassword(model.Password, user.Password))
                return LoginUserResult.NotFound;
            if (!user.IsEmailActive)
                return LoginUserResult.NotActive;
            return LoginUserResult.Success;

        }

        public async Task<RegisterUserResult> RegisterAsync(RegisterViewModel model)
        {
            #region Validations
            if (string.IsNullOrWhiteSpace(model.UserName) ||
                string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.Password))
            {
                return RegisterUserResult.InValidInputs;
            }

            if (await _userRepository.IsExistEmailAsync(model.Email.FixEmail()))
            {
                return RegisterUserResult.EmailDuplicated;
            }
            if (await _userRepository.IsExsitUserNameAsync(model.UserName.FixUserName()))
            {
                return RegisterUserResult.UserNameDuplicated;
            }
            #endregion

            var user = UserMapper.MapToUser(model);
            await _userRepository.CreatAsync(user);
            await _userRepository.SaveAsync();
            return RegisterUserResult.Success;


            //Todo Send Email Activation
        }
    }
}
