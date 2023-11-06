using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Entities
{
    [Keyless]
    public class IncludeProduct
    {
        public int marked_product_id { get; set; }
        public int receipt_id { get; set; }
    }
}
