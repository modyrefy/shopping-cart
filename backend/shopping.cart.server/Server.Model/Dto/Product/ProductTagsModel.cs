using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model.Dto.Product
{
    public class ProductTagsModel
    {
        public int ProductTagId { get; set; } = 0;
        public int ProductId { get; set; }
        public int TagId { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}
