using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class OrderFurniture
    {
        public int OrderId { get; set; }
        public int FurnitureId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public int OrderFurniture1 { get; set; }

        public virtual Furniture Furniture { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
