using Bibaket.Domain.Models.Permission;
using Bibaket.Domain.Models.Roles;
using Bibaket.Domain.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Application.Services.Interfaces
{
    public interface IRoleServices
    {
        Task<IEnumerable<Role>> GetAllRoleAsync();
        Task<Role> GetRoleByIdForAdmin(int? id);
        Task<IEnumerable<Permission>> GetAllPermissionAsync();

        Task CreateRole(AdminCreateRoleViewModel role);

        Task EditRoleAsync(AdminEditRoleViewModel role);

        Task DeleteRoleAsync(int roleId);

    }
}
