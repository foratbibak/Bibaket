using Bibaket.Domain.Contracts;
using Bibaket.Domain.Models.Roles;
using Bibaket.Domin.Models.Users;
using Bibaket.Ifra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Ifra.Data.Repositories
{
    public class RoleRepository(EshopDbContext _context) : IRoleRepository
    {
        public async Task CreateRoleAsync(Role role)
        {
            await _context.Role.AddAsync(role);
        }

        public async Task DeleteAsync(Role role)
        {
            role.IsDelete = true;
            role.DeleteDate = DateTime.Now;

        }

        public Task DeleteAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _context.Role.ToListAsync();
        }

        public Task<Role?> GetbyIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRoleAsync(Role role)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUserInRole(int userId, List<int> selectedroles)
        {
            var rolesUser = _context.UserInRoles.Where(r => r.UserId == userId).ToList();
            foreach (var role in rolesUser)
            {
                _context.Remove(role);
            }
            if (selectedroles != null && selectedroles.Count > 0)
            {
                foreach (int role in selectedroles)
                {
                    _context.UserInRoles.Add(new UserInRoles
                    {
                        UserId = userId,
                        RoleId = role,
                    });
                }
            }
            _context.SaveChanges();

        }
    }
}
