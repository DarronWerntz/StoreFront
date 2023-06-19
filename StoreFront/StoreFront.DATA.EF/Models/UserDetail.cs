using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class UserDetail
    {
        public UserDetail()
        {
            Orders = new HashSet<Order>();
            Reviews = new HashSet<Review>();
        }

        public string UserId { get; set; } = null!;
        public string? UserName { get; set; }
        public string? UserAddress { get; set; }
        public string? UserEmail { get; set; }
        public string? UserCity { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
