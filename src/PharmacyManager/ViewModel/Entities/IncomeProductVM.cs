using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Entities
{
    public class IncomeProductVM
    {
        public string Id { get; set; }
        public string Invoice_id { get; set; }
        public string Product_id { get; set; }
        public string P_count { get; set; }
        public string Measure_unit { get; set; }
        public string Series { get; set; }
        public string Vendor_price { get; set; }
        public string Production_date { get; set; }
        public string Vendor_vax { get; set; }
        public string Storage_location { get; set; }
    }
}
