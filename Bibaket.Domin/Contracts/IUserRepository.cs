using Bibaket.Domin.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Domin.Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUserAsync();
        Task<User?> GetUserbyIdAsync(int userId);
        Task CreatAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task DeleteAsync(int UserId);

        Task<bool> IsExsitUserNameAsync(string userName);
        Task<bool> IsExistEmailAsync(string email);

        Task<User?> GetUserByActiveCodeAsync(string activeCode);

        Task<User?>GetUserByEmailOrUserName(string emailOrUserName);

        Task SaveAsync();
    }
}
