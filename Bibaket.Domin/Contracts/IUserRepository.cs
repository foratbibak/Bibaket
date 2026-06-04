using Bibaket.Domin.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Domin.Contracts
{
    public interface IUserRepository
    {
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
        Task<bool> IsExistEmailAsync(string email);
        Task<bool> IsExistMobileAsync(string mobile);
        Task<bool> IsExistNationalAsync(string national);



        Task<User?> GetUserByActiveCodeAsync(string activeCode);

        Task<User?>GetUserByEmailOrUserName(string emailOrUserName);

        Task SaveAsync();
    }
}
