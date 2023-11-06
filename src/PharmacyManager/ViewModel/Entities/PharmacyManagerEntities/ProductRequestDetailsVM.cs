using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Entities.PharmacyManagerEntities
{
    public class ProductRequestDetailsVM
    {
        public string Id { get; set; }
        public string RequestDate { get; set; }
        public string ProductName { get; set; }
        public string ConsumerFirstName { get; set; }
        public string ConsumerMiddleName { get; set; }
        public string ConsumerLastName { get; set; }
        public string Count { get; set; }
        public string Approved { get; set; }
        public string Approximate_name { get; set; }
        public string Approved_date { get; set; }
    }
}
