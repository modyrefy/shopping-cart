using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model.Dto.Brand
{
    public class BrandModel
    {
        public int BrandId { get; set; } =0;
        public string BrandNameAr { get; set; }
        public string BrandNameEn { get; set; }
        public int CategoryId { get; set; }=0;
        public string CategoryNameAr { get; set; }
        public string CategoryNameEn { get; set; }
        public bool? IsActive { get; set; }=true;
    }
}
