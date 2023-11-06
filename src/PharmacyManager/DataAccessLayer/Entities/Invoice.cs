using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public int vendor_id { get; set; }
        public int created_employee_id { get; set; }
        public DateTime invoice_date { get; set; }
        public int invoice_number { get; set; }
        public int? accepted_employee_id { get; set; }
        public int? approved_employee_id { get; set; }
        public DateTime? receiving_date { get; set; }
        public DateTime? shipping_date {get;set;}
        public string document_gtin { get; set; }
        public string i_state { get; set; }
    }
}
