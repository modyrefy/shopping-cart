using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model.Dto.Product
{
    public class ProductImagesModel
    {
        public int ProductImageId { get; set; }=0;
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
        public string ThumbnailImageUrl { get; set; }
        public bool? IsPrimary { get; set; } = false;
        public bool? IsActive { get; set; } = true;
    }
}
