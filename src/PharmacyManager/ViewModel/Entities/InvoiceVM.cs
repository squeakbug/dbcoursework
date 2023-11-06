using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Entities
{
    public class InvoiceVM
    {
        public string Id { get; set; }
        public string vendor_id { get; set; }
        public string created_employee_id { get; set; }
        public string invoice_date { get; set; }
        public string invoice_number { get; set; }
        public string accepted_employee_id { get; set; }
        public string approved_employee_id { get; set; }
        public string receiving_date { get; set; }
        public string shipping_date { get; set; }
        public string document_gtin { get; set; }
        public string i_state { get; set; }
    }
}
