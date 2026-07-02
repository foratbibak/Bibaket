using Bibaket.Domain.Models.Permission;
using Bibaket.Domain.Models.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bibaket.Domain.ViewModels.Roles
{
    public class AdminCreateRoleViewModel
    {
        [DisplayName("نام نقش")]
        public string RoleName { get; set; }

        public List<int?> PermissonSelectedIds{ get; set; }
        public IEnumerable<Permission>? permissions { get; set; }


    }
}
