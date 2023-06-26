using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Furniture
    {
        public Furniture()
        {
            OrderFurnitures = new HashSet<OrderFurniture>();
            Reviews = new HashSet<Review>();
        }

        public int FurnitureId { get; set; }
        public int? CategoryId { get; set; }
        public int? MaterialId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? StockQuantity { get; set; }
        public bool IsDiscontinued { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Material? Material { get; set; }
        public virtual ICollection<OrderFurniture> OrderFurnitures { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
