using Bibaket.Domin.Contracts;
using Bibaket.Domin.Models.Users;
using Bibaket.Ifra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Ifra.Data.Repositories
{
    public class UserRepository(EshopDbContext context) : IUserRepository
    {
        public async Task AddUserToRole(int UserId, List<int> roleIds)
        {
            foreach(int roleId in roleIds)
            {
                context.UserInRoles.Add(new Domain.Models.Roles.UserInRoles()
                {
                    RoleId = roleId,
                    UserId=UserId,
                });
            }
        }

        public async Task CreatAsync(User user)
        {
            await context.Users.AddAsync(user);
        }

        public async Task DeleteAsync(User user)
        {
            user.IsDelete = true;
            user.IsActive = false;
            user.DeleteDate = DateTime.Now;
            await UpdateAsync(user);
        }

        public async Task DeleteAsync(int UserId)
        {
            var user = await GetUserbyIdAsync(UserId);
            if (user != null)
            {
                await DeleteAsync(user);
            }
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByActiveCodeAsync(string activeCode)
        {
            return await context.Users.SingleOrDefaultAsync(u=>u.EmailActiveCode==activeCode);
        }

        public async Task<User?> GetUserByEmailOrUserName(string emailOrUserName)
        {
            return await context.Users.SingleOrDefaultAsync(u=>u.Email==emailOrUserName||u.UserName==emailOrUserName);
        }

        public async Task<User?> GetUserbyIdAsync(int userId)
        {
            return await context.Users.SingleOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<bool> IsExistEmailAsync(string email)
        {
            return await context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> IsExistMobileAsync(string mobile)
        {
            return await context.Users.AnyAsync(u => u.Mobile == mobile);

        }

        public async Task<bool> IsExistNationalAsync(string national)
        {
            return await context.Users.AnyAsync(n=>n.NationalCode==national);
        }

        public async Task<bool> IsExsitUserNameAsync(string userName)
        {
            return await context.Users.AnyAsync(u=>u.UserName == userName);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            context.Users.Update(user);
        }
    }
}
