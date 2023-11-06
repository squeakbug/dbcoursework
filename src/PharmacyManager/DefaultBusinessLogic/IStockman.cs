using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification.Entities;

namespace DefaultBusinessLogic
{
    public interface IStockman : IEmployee
    {
        void ChangeProductLocation(int id, string newLocation);
        void AcceptInvoice(int id, DateTime? shippingDate = null);
        IEnumerable<IncomeProduct> GetIncomeProductsByInvoiceId(int id);
        IEnumerable<MarkedProduct> GetStoredProducts();
        IEnumerable<WriteOffProduct> GetWriteOffProducts();
        IEnumerable<Invoice> GetInvoices();
        Invoice GetInvoiceById(int id);
        Manufacturer GetManufacturerById(int id);
        Product GetProductById(int id);
        IncomeProduct GetIncomeProductById(int id);
        MarkInfo GetMarkInfoById(int id);
        MarkedProduct GetMarkedProductById(int id);
        Vendor GetVendorById(int id);
        WriteOffProduct GetWriteOffProductById(int id);
        IEnumerable<Vendor> GetVendors();
        IEnumerable<MarkInfo> GetMarkInfos();
        IEnumerable<IncomeProduct> GetIncomeProducts();
        IEnumerable<Product> GetProducts();
        void UpdateMarkInfo(MarkInfo markInfo);
        void UpdateIncomeProduct(IncomeProduct incomeProduct);
    }
}
