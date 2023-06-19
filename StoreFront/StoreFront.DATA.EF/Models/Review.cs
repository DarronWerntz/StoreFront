using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public int FurnitureId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public string? UserId { get; set; }

        public virtual Furniture Furniture { get; set; } = null!;
        public virtual UserDetail? User { get; set; }
    }
}
