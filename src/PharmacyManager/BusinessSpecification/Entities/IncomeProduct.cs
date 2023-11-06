using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessSpecification.Entities
{
    public class IncomeProduct
    {
        public int Id { get; set; }
        public int Invoice_id { get; set; }
        public int Product_id { get; set; }
        public int P_count { get; set; }
        public string Measure_unit { get; set; }
        public int Series { get; set; }
        public Decimal Vendor_price { get; set; }
        public DateTime Production_date { get; set; }
        public int Vendor_vax { get; set; }
        public string Storage_location { get; set; }
    }
}
