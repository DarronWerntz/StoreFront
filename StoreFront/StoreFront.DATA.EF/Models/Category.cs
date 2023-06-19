using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Category
    {
        public Category()
        {
            Furnitures = new HashSet<Furniture>();
        }

        public int CategoryId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Furniture> Furnitures { get; set; }
    }
}
