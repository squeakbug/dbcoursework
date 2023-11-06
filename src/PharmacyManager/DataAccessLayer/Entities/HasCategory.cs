using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Entities
{
    [Keyless]
    public class HasCategory
    {
        public int category_id { get; set; }
        public int product_id { get; set; }
    }
}
