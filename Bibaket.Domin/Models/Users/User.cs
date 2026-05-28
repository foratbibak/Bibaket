using Bibaket.Domain.Models.Roles;
using Bibaket.Domin.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Domin.Models.Users
{
    public class User:BaseEntity
    {
        #region Properties

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? EmailActiveCode { get; set; }
        public bool IsEmailActive { get; set; }
        public string? Mobile { get; set; }
        public int? MobileActiveCode { get; set; }
        public string? NationalCode { get; set; }

        public string Password { get; set; }
        public string? Avatar { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastLoginDate { get; set; }
        #endregion

        #region Realtions
        public ICollection<UserAddress>? Address { get; set; }
        public ICollection<UserInRoles>? UserInRole { get; set; }

        #endregion
    }
}
