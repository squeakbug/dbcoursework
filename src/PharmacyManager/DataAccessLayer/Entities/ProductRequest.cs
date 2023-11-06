using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class ProductRequest
    {
        public int Id { get; set; }
        public int Request_employee_id { get; set; }
        public int Customer_metadata_id { get; set; }
        public DateTime Request_Date { get; set; }
        public int? Product_id { get; set; }
        public int? Approved_employee_id { get; set; }
        public int Pr_count { get; set; }
        public int approved { get; set; }
        public string Approximate_name { get; set; }
        public DateTime? Approved_date { get; set; }
    }
}
