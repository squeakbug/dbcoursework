using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Entities.PharmacyManagerEntities
{
    public class InvoiceDetailsVM
    {
        public string Id { get; set; }
        public string VendorName { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; }
        public string DocumentGTIN { get; set; }
        public string State { get; set; }
    }
}
