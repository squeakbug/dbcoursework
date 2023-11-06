using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessSpecification.Entities
{
    public class MarkInfo
    {
        public int Id { get; set; }
        public int Approved_employee_id { get; set; }
        public int Income_product_id { get; set; }
        public DateTime approved_date { get; set; }
        public Decimal markup { get; set; }
        public int P_count { get; set; }
        public string Storage_location { get; set; }
    }
}
