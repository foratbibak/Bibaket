using Bibaket.Domain.Models.Categories;
using Bibaket.Domain.Models.Permission;
using Bibaket.Domain.Models.Roles;
using Bibaket.Domin.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Ifra.Data.Context
{
    public class EshopDbContext(DbContextOptions<EshopDbContext> options):
        DbContext(options)
    {
        #region Users
        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        #endregion

        #region Roles
        public DbSet<Role> Role { get; set; }
        public DbSet<UserInRoles> UserInRoles { get; set; }
        #endregion

        #region Permissions
        public DbSet<Permission> Permissions { get; set; }

        public DbSet<RolePermissionMapping> RolePermissionMappings { get; set; }

        #endregion

        #region Categories
        public DbSet<Category> Categories { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Query Fillter
            modelBuilder.Entity<User>().HasQueryFilter(u=>!u.IsDelete);
            modelBuilder.Entity<Role>().HasQueryFilter(r => !r.IsDelete);

            #endregion
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
