using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification;
using BusinessSpecification.Entities;
using BusinessSpecification.RepositoryInterfaces;

namespace DefaultBusinessLogic
{
    public interface IPharmacyManager : IEmployee
    {
        void Registrate(Employee employee, PersonMetadata metadata, string passwd, string repeatPasswd);
        void Fire(int employeeId);
        void WriteOffProduct(int markedProductId, int cnt, string reason);
        void SetCategoryToProduct(int categoryId, int productId);
        void RemoveInvoice(int invoiceId);
        void ApproveInvoice(int invoiceId);
        void UpdateInvoice(Invoice invoice);
        void UpdateProductInformation(Product product);
        void AddInvoice(Invoice invoice);
        void AddProductToInvoice(int invoiceId, IncomeProduct incomeProduct);
        void AddCategory(string title, int? parentCategoryId, string description);
        void AddProduct(Product product);
        void ApproveProductRequest(int productRequestId);
        IEnumerable<MarkInfo> GetMarkInfos();
        IEnumerable<IncomeProduct> GetIncomeProducts();
        IEnumerable<Invoice> GetInvoices();
        IEnumerable<MarkedProduct> GetStoredProducts();
        IEnumerable<Employee> GetEmployees();
        IEnumerable<Product> GetProducts();
        IEnumerable<WriteOffProduct> GetWriteOffProducts();
        IEnumerable<ProductRequest> GetProductRequests();
        IEnumerable<IncomeProduct> GetIncomeProductsByInvoiceId(int invoiceid);
        void AddInvoiceWithProducts(Invoice invoice, IEnumerable<IncomeProduct> products);
        IncomeProduct GetIncomeProductById(int id);
        Product GetProductById(int id);
        MarkInfo GetMarkInfoById(int id);
        MarkedProduct GetMarkedProductById(int id);
        Vendor GetVendorById(int id);
        WriteOffProduct GetWriteOffProductById(int id);
        PersonMetadata GetPersonMetadataById(int id);
        Manufacturer GetManufacturerById(int id);
        Employee GetEmployeeById(int id);
        ProductRequest GetProductRequestById(int id);
        void UpdateProduct(Product product);
        IEnumerable<Vendor> GetVendors();
        Invoice GetInvoiceById(int id);
        void UpdateEmployee(Employee employee);
        void UpdatePersonMetadata(PersonMetadata personMetadata);
    }
}
