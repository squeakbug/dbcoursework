using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class Receipt
    {
        public int Id { get; set; }
        public int Employee_id { get; set; }
        public int Cash_register_number { get; set; }
        public DateTime Leave_date { get; set; }
        public int Receipt_number { get; set; }
        public string Payment_type { get; set; }
        public Decimal Discount { get; set; }
        public Decimal Cash_size { get; set; }
        public Decimal Card_size { get; set; }
        public int Work_shift { get; set; }
    }
}
