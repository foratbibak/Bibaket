using Bibaket.Application.Services.Interfaces;
using Bibaket.Domain.Models.Permission;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Application.Services.Implementation
{
    public class PermissionService : IPermissionService
    {
        public Task<bool> CheckUserPermission(int userId, string permissionName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckUserPermission(int userId, IEnumerable<string> permissionNames)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Permission>> GetAllPermissionAsync()
        {
            throw new NotImplementedException();
        }
    }
}
