using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class MarkedProduct
    {
        public int Id { get; set; }
        public int Mark_info_id { get; set; }
        public int Sold { get; set; }
    }
}
