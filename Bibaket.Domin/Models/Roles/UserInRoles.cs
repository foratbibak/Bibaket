using Bibaket.Domin.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Domain.Models.Roles
{
    public class UserInRoles
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        #region Realations
        public User User { get; set; }

        public Role Role { get; set; }
        #endregion
    }
}
