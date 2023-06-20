using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFront.DATA.EF.Models
{
    //internal class Partials
    //{
    //}

    #region Category

    [ModelMetadataType(typeof(CategoryMetadata))]
    public partial class Category { }

    #endregion

    #region Furniture

    [ModelMetadataType(typeof(FurnitureMetadata))]
    public partial class Furniture
    {
        [NotMapped]
        public IFormFile? FurnitureImage { get; set; }
    }

    #endregion

    #region Material

    [ModelMetadataType(typeof(MaterialMetadata))]
    public partial class Material { }

    #endregion

    #region Order

    [ModelMetadataType(typeof(OrderMetadata))]
    public partial class Order { }

    #endregion

    #region OrderFurniture

    [ModelMetadataType(typeof(OrderFurnitureMetadata))]
    public partial class OrderFurniture { }

    #endregion

    #region Review

    [ModelMetadataType(typeof(ReviewMetadata))]
    public partial class Review { }

    #endregion

    #region UserDetail

    [ModelMetadataType(typeof(UserDetailMetadata))]
    public partial class UserDetail { }

    #endregion
}
