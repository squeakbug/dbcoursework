using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification;
using BusinessSpecification.Entities;
using BusinessSpecification.RepositoryInterfaces;
using DefaultBusinessLogic;
using DefaultBusinessLogic.Exceptions;

namespace DefaultBusinessLogic
{
    public class DefaultStockman : BaseEmployee, IStockman
    {
        public DefaultStockman(IRepositoryFactory factory, Employee employee)
            : base(factory, employee)
        {

        }

        public IEnumerable<Invoice> GetInvoices()
        {
            using (var invoiceRep = RepositoryFactory.CreateInvoiceRep())
            {
                return invoiceRep.GetInvoices();
            }
        }

        public IEnumerable<IncomeProduct> GetIncomeProductsByInvoiceId(int invoiceId)
        {
            using (var invoiceRep = RepositoryFactory.CreateInvoiceRep())
            {
                if (invoiceRep.GetInvoiceById(invoiceId) == null)
                    throw new StockmanException("No invoice with such id") { Stockman = this };
            }

            using (var incomeProductRep = RepositoryFactory.CreateIncomeProductRep())
            {
                return incomeProductRep.GetIncomeProductByInvoiceId(invoiceId);
            }
        }

        public void ChangeProductLocation(int productId, string newLocation)
        {
            using (var incomeProductRep = RepositoryFactory.CreateIncomeProductRep())
            {
                IncomeProduct incomeProduct = incomeProductRep.GetIncomeProductById(productId);
                incomeProduct.Storage_location = newLocation;
                incomeProductRep.Update(incomeProduct);
            }
        }

        public void AcceptInvoice(int id, DateTime? shippingDate = null)
        {
            if (shippingDate != null && shippingDate.Value > DateTime.Now)
                throw new StockmanException("Shipping date cannot be in the future");

            int employeeId = Employee.id;
            using (var invoiceRep = RepositoryFactory.CreateInvoiceRep())
            {
                var invoice = invoiceRep.GetInvoiceById(id);
                if (invoice == null)
                    throw new StockmanException("No invoice with such id");
                if (invoice.i_state != "init")
                    throw new StockmanException("Invoice is already accepted");
                invoiceRep.AcceptInvoice(id, employeeId, shippingDate);
            }
        }

        public IEnumerable<MarkedProduct> GetStoredProducts()
        {
            using (var markedProductRep = RepositoryFactory.CreateMarkedProductRep())
            {
                return markedProductRep.GetSellingProducts();
            }
        }

        public IEnumerable<WriteOffProduct> GetWriteOffProducts()
        {
            using (var writeOffProductRep = RepositoryFactory.CreateWriteOffProductRep())
            {
                return writeOffProductRep.GetWriteOffProducts();
            }
        }

        public Product GetProductById(int id)
        {
            using (var productRep = RepositoryFactory.CreateProductRep())
            {
                return productRep.GetProductById(id);
            }
        }
        public IncomeProduct GetIncomeProductById(int id)
        {
            using (var incomeProductRep = RepositoryFactory.CreateIncomeProductRep())
            {
                return incomeProductRep.GetIncomeProductById(id);
            }
        }
        public MarkInfo GetMarkInfoById(int id)
        {
            using (var markInfoRep = RepositoryFactory.CreateMarkInfoRep())
            {
                return markInfoRep.GetMarkInfoById(id);
            }
        }
        public Vendor GetVendorById(int id)
        {
            using (var vendorRep = RepositoryFactory.CreateVendorRep())
            {
                return vendorRep.GetVendorById(id);
            }
        }

        public IEnumerable<Vendor> GetVendors()
        {
            using (var vendorRep = RepositoryFactory.CreateVendorRep())
            {
                return vendorRep.GetVendors();
            }
        }

        public IEnumerable<MarkInfo> GetMarkInfos()
        {
            using (var markInfoRep = RepositoryFactory.CreateMarkInfoRep())
            {
                return markInfoRep.GetMarkInfos();
            }
        }

        public IEnumerable<IncomeProduct> GetIncomeProducts()
        {
            using (var incomeProductRep = RepositoryFactory.CreateIncomeProductRep())
            {
                return incomeProductRep.GetIncomeProducts(null);
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            using (var productRep = RepositoryFactory.CreateProductRep())
            {
                return productRep.GetProducts();
            }
        }

        public Invoice GetInvoiceById(int id)
        {
            using (var invoiceRep = RepositoryFactory.CreateInvoiceRep())
            {
                return invoiceRep.GetInvoiceById(id);
            }
        }

        public Manufacturer GetManufacturerById(int id)
        {
            using (var manufacturerRep = RepositoryFactory.CreateManufacturerRep())
            {
                return manufacturerRep.GetManufacturerById(id);
            }
        }

        public MarkedProduct GetMarkedProductById(int id)
        {
            using (var markedProductRep = RepositoryFactory.CreateMarkedProductRep())
            {
                return markedProductRep.GetMarkedProductById(id);
            }
        }

        public WriteOffProduct GetWriteOffProductById(int id)
        {
            using (var writeOffProductRep = RepositoryFactory.CreateWriteOffProductRep())
            {
                return writeOffProductRep.GetWriteOffProductById(id);
            }
        }

        public void UpdateMarkInfo(MarkInfo markInfo)
        {
            using (var markInfoRep = RepositoryFactory.CreateMarkInfoRep())
            {
                markInfoRep.Update(markInfo);
            }
        }

        public void UpdateIncomeProduct(IncomeProduct incomeProduct)
        {
            using (var incomeProductRep = RepositoryFactory.CreateIncomeProductRep())
            {
                incomeProductRep.Update(incomeProduct);
            }
        }
    }
}
