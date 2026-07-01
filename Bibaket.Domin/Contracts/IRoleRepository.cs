using Bibaket.Domain.Models.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Domain.Contracts
{
    public interface IRoleRepository
    {


        Task<IEnumerable<Role>> GetAllRolesAsync();

        Task<Role?>GetbyIdAsync(int roleId);
        Task CreateRoleAsync(Role role);
        Task UpdateRoleAsync(Role role);
        Task DeleteAsync(Role role);
        Task DeleteAsync(int Id);
        Task SaveAsync();

        Task UpdateUserInRole(int  userId, List<int> selectedroles);
    }
}
