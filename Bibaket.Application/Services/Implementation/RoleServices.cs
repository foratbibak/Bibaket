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
                RoleName=role.RoleName,
                CreatDate=DateTime.Now,
                IsDelete=false,
                
            };
            await _roleRepository.CreateRoleAsync(Addrole);
            await _roleRepository.SaveAsync();

            foreach (int item in role.PermissonSelectedIds)
            {
                await _roleRepository.AddPermissonToRoleAsync(Addrole.Id, item);
            }
            await _roleRepository.CreateRoleAsync(Addrole);
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

    }
}
