using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model.Dto.Product
{
    public class ProductDashboardModelRequest
    {
        public string Name { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; }= 0;
    }
}
