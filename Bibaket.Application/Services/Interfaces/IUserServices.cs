using Bibaket.Domain.Enums.User;
using Bibaket.Domain.Models.Roles;
using Bibaket.Domain.ViewModels.User;
using Bibaket.Domin.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Application.Services.Interfaces
{
    public interface IUserServices
    {
        Task<AdminFilterUserViewModel> AdminFilterAsync(AdminFilterUserViewModel model);
        Task<IEnumerable<UserViewModel>> ListUsersForAdmin();
        Task<AdminCreateUserResult> CreatUserInAdminAsync(AdminCreatUserViewModel model);
        Task<AdminEditUserResult> EditUserAsync(AdminEditViewModel model);
        Task<User> GetUserForDeleteAsync(int userId);
        Task<AdminEditViewModel> GetUserForEditAsync(int userId);
        Task DeleteUserAsync(int userId);
        Task UserDeAcitve(int userId);
    }
}
