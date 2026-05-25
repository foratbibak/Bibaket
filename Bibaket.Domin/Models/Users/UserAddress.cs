using Bibaket.Domin.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bibaket.Domin.Models.Users
{
    public class UserAddress:BaseEntity
    {
        public int UserId { get; set; }
        public required string Title { get; set; }
        public required string PostalCode { get; set; }
        public required string Address { get; set; }

        #region Realtions
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
        #endregion
    }
}
