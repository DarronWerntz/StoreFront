using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Add using statement to access the annotations below
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace StoreFront.DATA.EF.Models
{
    //internal class Metadata
    //{
    //}


    #region CategoryMetadata

    public class CategoryMetadata
    {

        //public int CategoryId { get; set; }

        [StringLength(50, ErrorMessage = "*Max 50 characters")]
        [Display(Name = "Category")]
        public string? Name { get; set; }

    }

    #endregion




    #region FurnitureMetadata

    public class FurnitureMetadata
    {

        //public int FurnitureId { get; set; }
        //public int? CategoryId { get; set; }
        //public int? MaterialId { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(50, ErrorMessage = "*Max 50 characters")]
        [Display(Name = "Product")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Product Price is required")]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = false)]
        [Range(0, (double)decimal.MaxValue)]
        public decimal Price { get; set; }

        [StringLength(500, ErrorMessage = "*Max 500 characters")]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [StringLength(75, ErrorMessage = "*Cannot exceed 75 characters")]
        [Display(Name = "Image")]
        public string? Image { get; set; }

        [Required(ErrorMessage = "*In Stock is required")]
        [Display(Name = "In Stock")]
        public string? StockQuantity { get; set; }

        [Required(ErrorMessage = "Discontinued is required")]
        [Display(Name = "Discontued?")] 
        public bool IsDiscontinued { get; set; }
    }

    #endregion




    #region MaterialMetadata

    public class MaterialMetadata
    {
        //public int MaterialId { get; set; }

        [Required(ErrorMessage = "Material Name is required")]
        [Display(Name = "Material")]
        public string? Name { get; set; }
    }

    #endregion



    #region OrderMetadata

    public class OrderMetadata
    {
        //public int OrderId { get; set; }
        //public string UserId { get; set; } = null!;

        [Required(ErrorMessage = "Order Date is required")]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Recipient")]
        [DataType(DataType.MultilineText)]
        public string? RecipientInfo { get; set; }

    }

    #endregion



    #region OrderFurnitureMetadata

    public class OrderFurnitureMetadata
    {
        //public int OrderId { get; set; }
        //public int FurnitureId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Display(Name = "Quantity")]
        [Range(0, 10)]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "*Price is required")]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = false)]
        [Range(0, (double)decimal.MaxValue)]
        public decimal Price { get; set; }
                
        [Display(Name = "Discount")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = false)]
        [Range(0, (double)decimal.MaxValue)]
        public decimal? Discount { get; set; }

        [Required(ErrorMessage = "Order amounts are required")]
        [Display(Name = "Order Furniture")]
        [Range(0, 10)]
        public int OrderFurniture { get; set; }

    }

    #endregion



    #region ReviewMetadata

    public class ReviewMetadata
    {
        //public int ReviewId { get; set; }
        //public int FurnitureId { get; set; }

        [Required(ErrorMessage = "Ratings are required and help us to improve our products")]
        [Display(Name = "Rating")]
        [Range(0, 5)]
        public int Rating { get; set; }

        [StringLength(500, ErrorMessage = "*Max 500 characters")]
        [Display(Name = "Comment")]
        public string? Comment { get; set; }

        //public string? UserId { get; set; }

    }

    #endregion



    #region UserDetailMetadata

    public class UserDetailMetadata
    {
        //public string UserId { get; set; } = null!;

        [StringLength(50, ErrorMessage = "*Max 50 characters")]
        [Display(Name = "Username")]
        public string? UserName { get; set; }

        [StringLength(50, ErrorMessage = "*Max 50 characters")]
        [Display(Name = "Address")]
        public string? UserAddress { get; set; }

        [StringLength(50, ErrorMessage = "*Max 50 characters")]
        [Display(Name = "Email")]
        public string? UserEmail { get; set; }

        [StringLength(25, ErrorMessage = "*Max 25 characters")]
        [Display(Name = "City")]
        public string? UserCity { get; set; }

    }

    #endregion
}
