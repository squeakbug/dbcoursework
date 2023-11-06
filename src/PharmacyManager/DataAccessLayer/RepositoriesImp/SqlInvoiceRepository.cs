using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

using DataAccessLayer.Converters;
using DataAccessLayer.Exceptions;
using BusinessSpecification.Entities;
using BusinessSpecification.RepositoryInterfaces;

namespace DataAccessLayer.RepositoriesImp
{
    class SqlInvoiceRepository : IInvoiceRepository
    {
        private Context _globalCtx;

        public SqlInvoiceRepository(Context globalCtx)
        {
            _globalCtx = globalCtx;
        }

        public IEnumerable<Invoice> GetInvoices()
        {
            var converter = new InvoiceConverter();
            var result = new List<Invoice>();
            foreach (var item in _globalCtx.Invoice)
                result.Add(converter.MapToBusinessEntity(item, _globalCtx));
            return result;
        }

        public IEnumerable<Invoice> GetInvoicesByState(string state)
        {
            var result = new List<Invoice>();
            var converter = new InvoiceConverter();
            foreach (var invoice in _globalCtx.Invoice)
            {
                if (invoice.i_state == state)
                    result.Add(converter.MapToBusinessEntity(invoice, _globalCtx));
            }
            return result;
        }

        public void ApproveInvoice(int invoiceId, int employeeId, string newLocation)
        {
            _globalCtx.Database.ExecuteSqlInterpolated(
                $"EXEC dbo.approveProductsByInvoice {invoiceId}, {employeeId}, {newLocation}");
            _globalCtx.SaveChanges();
        }

        public void AcceptInvoice(int invoiceId, int employeeId, DateTime? shippingDate)
        {
            _globalCtx.Database.ExecuteSqlInterpolated(
                $"EXEC dbo.acceptInvoice {invoiceId}, {employeeId}, {shippingDate}");
            _globalCtx.SaveChanges();
        }

        public Invoice GetInvoiceById(int id)
        {
            var invoice = _globalCtx.Invoice.Find(id);
            var converter = new InvoiceConverter();
            return invoice == null ? null : converter.MapToBusinessEntity(invoice, _globalCtx);
        }

        public void AddInvoiceWithProducts(Invoice invoice, IEnumerable<IncomeProduct> products)
        {
            var invoiceConverter = new InvoiceConverter();
            var productConverter = new IncomeProductConverter();
            var invoiceDAL = invoiceConverter.MapFromBusinessEntity(invoice);
            _globalCtx.Invoice.Add(invoiceDAL);
            _globalCtx.SaveChanges();
            int invoiceId = invoiceDAL.Id;
            foreach (var item in products)
            {
                var productDAL = productConverter.MapFromBusinessEntity(item);
                productDAL.Invoice_id = invoiceId;
                _globalCtx.Income_product.Add(productDAL);
            }
            _globalCtx.SaveChanges();
        }

        public void Create(Invoice invoice)
        {
            var converter = new InvoiceConverter();
            _globalCtx.Invoice.Add(converter.MapFromBusinessEntity(invoice));
            _globalCtx.SaveChanges();
        }

        public void Update(Invoice invoice)
        {
            var converter = new InvoiceConverter();
            _globalCtx.Invoice.Update(converter.MapFromBusinessEntity(invoice));
            _globalCtx.SaveChanges();
        }

        public void Delete(int id)
        {
            var invoice = _globalCtx.Invoice.Find(id);
            if (invoice == null)
                throw new InvoiceException("No invoice with such id");

            _globalCtx.Invoice.Remove(invoice);
            _globalCtx.SaveChanges();
        }

        public void Truncate()
        {
            _globalCtx.Database.ExecuteSqlInterpolated($"DELETE dbo.invoice");
            _globalCtx.SaveChanges();
        }

        private bool disposed = false;

        private void CleanUp(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _globalCtx.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            CleanUp(true);
            GC.SuppressFinalize(this);
        }
    }
}
