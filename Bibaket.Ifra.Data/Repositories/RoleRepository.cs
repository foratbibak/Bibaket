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
    }
}
