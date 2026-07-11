using Bibaket.Domain.Models.Permission;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Domain.Contracts
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetAllPermission();



        Task<Permission?> GetbyIdAsync(int permissionId);

        Task<Permission?> GetPermissionByName(string PermissionName);

        Task<Permission?> GetPermissionByIdAsync(int permissionId);

        Task CreateAsync(Permission permission);

        Task UpdateAsync(Permission permission);

        Task DeletePermissionAsync(Permission permission);

        Task DeleteAsync(int PermissionId);

        Task SaveChangesAsync();
    }
}
