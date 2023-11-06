using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using BusinessSpecification.Entities;
using DataAccessLayer;
using DataAccessLayer.RepositoriesImp;

namespace DataAccessLayer.Converters
{
    internal class InvoiceConverter
    {
        public Invoice MapToBusinessEntity(Entities.Invoice invoice, Context globalCtx)
        {
            return new Invoice()
            {
                Id = invoice.Id,
                accepted_employee_id = invoice.accepted_employee_id,
                approved_employee_id = invoice.approved_employee_id,
                created_employee_id = invoice.created_employee_id,
                document_gtin = invoice.document_gtin,
                invoice_date = invoice.invoice_date,
                invoice_number = invoice.invoice_number,
                i_state = invoice.i_state,
                receiving_date = invoice.receiving_date,
                shipping_date = invoice.shipping_date,
                vendor_id = invoice.vendor_id
            };
        }

        public Entities.Invoice MapFromBusinessEntity(Invoice invoice)
        {
            var retval = new Entities.Invoice()
            {
                Id = invoice.Id,
                accepted_employee_id = invoice.accepted_employee_id,
                approved_employee_id = invoice.approved_employee_id,
                created_employee_id = invoice.created_employee_id,
                document_gtin = invoice.document_gtin,
                invoice_date = invoice.invoice_date,
                invoice_number = invoice.invoice_number,
                i_state = invoice.i_state,
                receiving_date = invoice.receiving_date,
                shipping_date = invoice.shipping_date,
                vendor_id = invoice.vendor_id
            };

            return retval;
        }
    }
}
