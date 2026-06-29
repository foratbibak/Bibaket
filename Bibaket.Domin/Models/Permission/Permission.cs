using Bibaket.Domin.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Domain.Models.Permission
{
    public class Permission:BaseEntity
    {
        public int? ParentId { get; set; }
        public string UniqName { get; set; }
        public string DisplayName { get; set; }



        public  Permission? Parent { get; set; }

        public ICollection<RolePermissionMapping> RolePermissionConfigs { get; set; }
    }
}
