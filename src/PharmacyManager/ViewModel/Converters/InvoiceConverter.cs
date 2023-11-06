using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification.Entities;
using ViewModel.Entities;

namespace ViewModel.Converters
{
    public static class InvoiceConverter
    {
        public static InvoiceVM MapToViewModel(Invoice invoice)
        {
            var invoiceVM = new InvoiceVM();

            if (invoice.accepted_employee_id != null)
                invoiceVM.accepted_employee_id = invoice.accepted_employee_id.Value.ToString();
            if (invoice.approved_employee_id != null)
                invoiceVM.approved_employee_id = invoice.approved_employee_id.Value.ToString();
            if (invoice.shipping_date != null)
                invoiceVM.shipping_date = invoice.shipping_date.Value.ToString();

            invoiceVM.vendor_id = invoice.vendor_id.ToString();
            invoiceVM.receiving_date = invoice.receiving_date.ToString();
            invoiceVM.i_state = invoice.i_state;
            invoiceVM.invoice_number = invoice.invoice_number.ToString();
            invoiceVM.invoice_date = invoice.invoice_date.ToString();
            invoiceVM.Id = invoice.Id.ToString();
            invoiceVM.document_gtin = invoice.document_gtin;
            invoiceVM.created_employee_id = invoice.created_employee_id.ToString();

            return invoiceVM;
        }
    }
}
