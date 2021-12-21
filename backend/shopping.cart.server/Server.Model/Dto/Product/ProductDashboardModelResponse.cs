using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model.Dto.Product
{
    public class ProductDashboardModelResponse
    {
        public int ProductId { get; set; }
        public string ProductKey { get; set; }
        public string ProductNameAr { get; set; }
        public string ProductNameEn { get; set; }
        public int StockQuantiy { get; set; } = 0;
        public decimal Price { get; set; } = 0;
        public string ProductImage { get; set; }    
    }
}
