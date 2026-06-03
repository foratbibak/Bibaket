using Bibaket.Domain.Models.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Application.Services.Interfaces
{
    public interface IRoleServices
    {
        Task<IEnumerable<Role>> GetAllRoleAsync();
    }
}
