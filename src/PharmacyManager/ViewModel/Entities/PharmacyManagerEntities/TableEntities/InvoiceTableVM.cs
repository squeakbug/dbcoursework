using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Entities.PharmacyManagerEntities.TableEntities
{
    public class InvoiceTableVM
    {
        public string Id { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; }
        public string DocumentGTIN { get; set; }
        public string State { get; set; }
    }
}
