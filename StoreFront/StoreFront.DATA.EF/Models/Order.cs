using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderFurnitures = new HashSet<OrderFurniture>();
        }

        public int OrderId { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public string? RecipientInfo { get; set; }

        public virtual UserDetail User { get; set; } = null!;
        public virtual ICollection<OrderFurniture> OrderFurnitures { get; set; }
    }
}
