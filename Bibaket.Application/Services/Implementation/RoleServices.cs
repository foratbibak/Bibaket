using Bibaket.Application.Services.Interfaces;
using Bibaket.Domain.Contracts;
using Bibaket.Domain.Models.Permission;
using Bibaket.Domain.Models.Roles;
using Bibaket.Domain.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Application.Services.Implementation
{
    public class RoleServices(IRoleRepository _roleRepository) : IRoleServices
    {
        public async Task CreateRole(AdminCreateRoleViewModel role)
        {
            Role Addrole = new Role()
            {
                RoleName = role.RoleName,
                CreatDate = DateTime.Now,
                IsDelete = false,

            };
            await _roleRepository.CreateRoleAsync(Addrole);
            await _roleRepository.SaveAsync();

            if (role.PermissonSelectedIds != null && role.PermissonSelectedIds.Any())
            {
                foreach (var item in role.PermissonSelectedIds)
                {
                    await _roleRepository.AddPermissonToRoleAsync(Addrole.Id, item);
                    await _roleRepository.SaveAsync();

                }
            }
        }

        public async Task DeleteRoleAsync(int roleId)
        {
            var role = await _roleRepository.GetbyIdAsync(roleId);
            role.IsDelete = true;
            role.DeleteDate = DateTime.Now;
            await _roleRepository.UpdateRoleAsync(role);
            await _roleRepository.SaveAsync();
        }

        public async Task EditRoleAsync(AdminEditRoleViewModel role)
        {
            var editrole = await _roleRepository.GetbyIdAsync(role.RoleId);
            editrole.UpdateDate = DateTime.Now;
            editrole.RoleName = role.RoleName;
            await _roleRepository.UpdateRoleAsync(editrole);

            await _roleRepository.DeleteAllPermissionInRole(editrole.Id);

            foreach (var item in role.PermissonSelectedIds)
            {
                await _roleRepository.AddPermissonToRoleAsync(editrole.Id, item);
            }


            await _roleRepository.SaveAsync();
        }

        public async Task<IEnumerable<Permission>> GetAllPermissionAsync()
        {
            return await _roleRepository.GetAllPermissionsAsync();
        }

        public async Task<IEnumerable<Role>> GetAllRoleAsync()
        {
            return await _roleRepository.GetAllRolesAsync();
        }

        public async Task<Role> GetRoleByIdForAdmin(int? id)
        {
            return await _roleRepository.GetRoleByIdForAdminAsync(id);

        }
    }
}
