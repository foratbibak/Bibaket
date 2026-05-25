using Bibaket.Domin.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Domain.Models.Roles
{
    public class Role:BaseEntity
    {
        public string RoleName { get; set; }


        #region Realations
        public ICollection<UserInRoles>? UserInRole { get; set; }
        #endregion

    }
}
