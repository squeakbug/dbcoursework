using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Entities
{
    public class MarkInfoVM
    {
        public string Id { get; set; }
        public string Approved_employee_id { get; set; }
        public string Income_product_id { get; set; }
        public string approved_date { get; set; }
        public string markup { get; set; }
        public string P_count { get; set; }
        public string Storage_location { get; set; }
    }
}
