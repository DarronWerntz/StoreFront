using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Material
    {
        public Material()
        {
            Furnitures = new HashSet<Furniture>();
        }

        public int MaterialId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Furniture> Furnitures { get; set; }
    }
}
