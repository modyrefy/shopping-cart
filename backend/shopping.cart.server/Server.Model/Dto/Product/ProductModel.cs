using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model.Dto.Product
{
    public class ProductModel
    {
        public int ProductId { get; set; }=0;
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int StockQuantiy { get; set; } = 0;
        public decimal Price { get; set; } = 0;
        public decimal Discount { get; set; }=0;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool? IsActive { get; set; } = true;

        public List<ProductImagesModel> Images { get; set; }
        public List<ProductTagsModel> Tags { get; set; }

    }
}
