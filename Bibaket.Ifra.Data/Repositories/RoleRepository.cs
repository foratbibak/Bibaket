using Bibaket.Domain.Contracts;
using Bibaket.Domain.Models.Roles;
using Bibaket.Ifra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Ifra.Data.Repositories
{
    public class RoleRepository(EshopDbContext _context) : IRoleRepository
    {
        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _context.Role.ToListAsync();
        }

        public async Task UpdateUserInRole(int userId, List<int> selectedroles)
        {
            var rolesUser = _context.UserInRoles.Where(r => r.UserId == userId).ToList();
            foreach (var role in rolesUser)
            {
                _context.Remove(role);
            }
            if(selectedroles != null && selectedroles.Count > 0)
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
