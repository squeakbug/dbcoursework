using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification.Entities;

namespace BusinessSpecification.RepositoryInterfaces
{
    public interface IInvoiceRepository : IDisposable
    {
        IEnumerable<Invoice> GetInvoices();
        IEnumerable<Invoice> GetInvoicesByState(string state);
        Invoice GetInvoiceById(int id);
        void ApproveInvoice(int invoiceId, int employeeId, string newLocation = null);
        void AcceptInvoice(int invoiceId, int employeeid, DateTime? shippingDate = null);
        void AddInvoiceWithProducts(Invoice invoice, IEnumerable<IncomeProduct> products);
        void Create(Invoice invoice);
        void Update(Invoice invoice);
        void Delete(int id);
        void Truncate();
    }
}
