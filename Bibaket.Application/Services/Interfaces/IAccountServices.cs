using Bibaket.Domain.ViewModels.Account;
using Bibaket.Domin.Enums;
using Bibaket.Domin.Models.Users;
using Bibaket.Domin.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Application.Services.Interfaces
{
    public interface IAccountServices
    {
        Task<RegisterUserResult> RegisterAsync(RegisterViewModel model);
        Task<LoginUserResult> LoginUserAsync(LoginViewModel model);
        Task<bool>ActiveAccountAsync(string activecode);
        Task<User?>GetUserByEmailOrUserName(string emailorUserName);
        Task<bool> ChangePassword(int UserId, ChangePasswordViewModel changePassword);
        Task<bool> IsCompleteProfile(int UserId);
        Task<bool> EditProfile(int userId, ProfileViewModel profile);
        Task<ProfileViewModel> GetUserProfile(int userId);
    }
}
