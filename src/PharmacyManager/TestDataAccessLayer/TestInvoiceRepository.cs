using System;
using Xunit;
using System.Collections.Generic;

using DataAccessLayer;
using DataAccessLayer.RepositoriesImp;
using BusinessSpecification;
using BusinessSpecification.RepositoryInterfaces;
using BusinessSpecification.Entities;

namespace TestDataAccessLayer
{
    public class TestInvoiceRepository
    {
        [Fact]
        public void TestInvoiceCRUD()
        {
            var factory = new SqlRepositoryFactory
            (
                dataSource : "127.0.0.1,1433",
                initialCatalog : "pharmacy",
                user : "SA",
                passwd : "P@ssword"
            );

            SetupInvoiceRepository(factory);

            Invoice tmpInvoice = null;
            using (var rep = factory.CreateInvoiceRep())
            {
                tmpInvoice = rep.GetInvoiceById(1);
                Assert.NotNull(tmpInvoice);
                Assert.Equal(1000, tmpInvoice.invoice_number);

                var invoices = new List<Invoice>(rep.GetInvoices());
                Assert.Equal(3, invoices.Count);
                tmpInvoice = rep.GetInvoiceById(1);
            }

            using (var rep = factory.CreateInvoiceRep())
            {
                tmpInvoice.invoice_number = 4000;
                rep.Update(tmpInvoice);

                tmpInvoice = rep.GetInvoiceById(1);
                Assert.Equal(4000, tmpInvoice.invoice_number);

                rep.Delete(tmpInvoice.Id);
                var invoices = new List<Invoice>(rep.GetInvoices());
                Assert.Equal(2, invoices.Count);
            }

            ReleaseInvoiceRepository(factory);
        }

        public static void SetupInvoiceRepository(SqlRepositoryFactory factory)
        {
            TestVendorRepository.SetupVendorRepository(factory);
            TestEmployeeRepository.SetupEmployeeRepository(factory);

            List<Invoice> invoiceSample = GetInvoiceSample(factory);
            using (var rep = factory.CreateInvoiceRep())
            {
                rep.Create(invoiceSample[0]);
                rep.Truncate();
            }

            using (var rep = factory.CreateInvoiceRep())
            {
                rep.Create(invoiceSample[0]);
                rep.Create(invoiceSample[1]);
                rep.Create(invoiceSample[2]);
            }
        }

        public static void ReleaseInvoiceRepository(SqlRepositoryFactory factory)
        {
            using (var rep = factory.CreateInvoiceRep())
                rep.Truncate();

            TestVendorRepository.ReleaseVendorRepository(factory);
            TestEmployeeRepository.ReleaseEmployeeRepository(factory);
        }

        public static List<Invoice> GetInvoiceSample(SqlRepositoryFactory factory)
        {
            List<Vendor> vendors = null;
            List<Employee> employees = null;
            using (var rep = factory.CreateVendorRep())
                vendors = new List<Vendor>(rep.GetVendors());
            using (var rep = factory.CreateEmployeeRep())
                employees = new List<Employee>(rep.GetEmployees());

            return new List<Invoice>
            {
                new Invoice
                {
                    accepted_employee = employees[0], approved_employee = employees[1],
                    created_employee = employees[2], document_gtin = "1000", invoice_date = DateTime.Now,
                    invoice_number = 1000, i_state = "approved", receiving_date = DateTime.Now,
                    shipping_date = DateTime.Now, vendor = vendors[0],
                },
                new Invoice
                {
                    accepted_employee = employees[0], approved_employee = employees[1],
                    created_employee = employees[2], document_gtin = "1000", invoice_date = DateTime.Now,
                    invoice_number = 2000, i_state = "approved", receiving_date = DateTime.Now,
                    shipping_date = DateTime.Now, vendor = vendors[1],
                },
                new Invoice
                {
                    accepted_employee = employees[0], approved_employee = employees[1],
                    created_employee = employees[2], document_gtin = "1000", invoice_date = DateTime.Now,
                    invoice_number = 3000, i_state = "approved", receiving_date = DateTime.Now,
                    shipping_date = DateTime.Now, vendor = vendors[2],
                }
            };
        }
    }
}

