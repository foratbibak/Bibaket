using Bibaket.Domin.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Domin.Contracts
{
    public interface IUserRepository
    {
        Task<IQueryable<User>> FilterAsync();
        Task<IEnumerable<User>> GetAllUserAsync();
        Task<IEnumerable<User>> GetAllUserForAdminAsync();
        Task<User?> GetUserbyIdAsync(int userId);
        Task<User?> GetUserFullDataAsync(int userId);
        Task ReturnUserDeAcitve(User user);
        Task UserDeAcitve(int userId);
        Task CreatAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task DeleteAsync(int UserId);


        Task AddUserToRole(int UserId, List<int> roleIds);

        Task<bool> IsExsitUserNameAsync(string userName);
        Task<bool> IsExsitUserNameForEditAsync(string userName, int userId);
        Task<bool> IsExistEmailAsync(string email);
        Task<bool> IsExistEmailForEditAsync(string email, int userId);
        Task<bool> IsExistMobileAsync(string mobile);
        Task<bool> IsExistMobileForEditAsync(string mobile, int userId);
        Task<bool> IsExistNationalAsync(string national);
        Task<bool> IsExistNationalForEditAsync(string national, int userId);



        Task<User?> GetUserByActiveCodeAsync(string activeCode);

        Task<User?> GetUserByEmailOrUserName(string emailOrUserName);

        Task SaveAsync();
    }
}
