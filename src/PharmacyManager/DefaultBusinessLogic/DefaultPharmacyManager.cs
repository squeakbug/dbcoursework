using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

using BusinessSpecification;
using BusinessSpecification.Entities;
using BusinessSpecification.RepositoryInterfaces;
using DefaultBusinessLogic.Exceptions;
using DefaultBusinessLogic.Validators;

namespace DefaultBusinessLogic
{
    public class DefaultPharmacyManager : BaseEmployee, IPharmacyManager
    {
        private IAuthManager _authManager { get; set; }

        public DefaultPharmacyManager(IRepositoryFactory factory, Employee employee, IAuthManager authManager)
            : base(factory, employee)
        {
            _authManager = authManager;
        }

        public void Registrate(Employee employee, PersonMetadata metadata, string passwd, string repeatPasswd)
        {
            if (employee == null || metadata == null || passwd == null || repeatPasswd == null)
                throw new PharmacyManagerException("Registrate args must be not null values");
            if (employee.e_login == null || employee.appointment == null || employee.e_login == null)
                throw new PharmacyManagerException("No enough information to registrate") { PharmacyManager = this };
            if (metadata.First_name == null || metadata.Middle_name == null || metadata.Phone == null)
                throw new PharmacyManagerException("No enough information to registrate") { PharmacyManager = this };
            if (passwd != repeatPasswd)
                throw new PharmacyManagerException("Incorrect repeated password") { PharmacyManager = this };
            using (IEmployeeRepository employeeRep = RepositoryFactory.CreateEmployeeRep())
            {
                if (employeeRep.GetEmployeeByLogin(employee.e_login) != null)
                    throw new PharmacyManagerException("Login is occupied") { PharmacyManager = this };
            }
            if (!_authManager.IsAppointmentValid(employee.appointment))
                throw new PharmacyManagerException("Appoinment is invalid") { PharmacyManager = this };
            if (passwd.Length < 6)
                throw new PharmacyManagerException("Password length must be greater than 5");

            string hashedPasswd = _authManager.HashPassword(passwd);
            employee.p_hash = hashedPasswd;
            using (IEmployeeRepository employeeRep = RepositoryFactory.CreateEmployeeRep())
            {
                employeeRep.RegistrateEmployee(employee, metadata);
            }
        }

        public void Fire(int employeeId)
        {
            using (IEmployeeRepository employeeRep = RepositoryFactory.CreateEmployeeRep())
            {
                if (employeeRep.GetEmployeeById(employeeId) == null)
                    throw new PharmacyManagerException("No employee with such id");
                employeeRep.Delete(employeeId);
            }
        }

        public IEnumerable<MarkedProduct> GetStoredProducts()
        {
            using (var markedProductRep = RepositoryFactory.CreateMarkedProductRep())
            {
                return markedProductRep.GetSellingProducts();
            }
        }

        public void WriteOffProduct(int markedProductId, int cnt, string reason)
        {
            MarkedProduct markedProduct = null;
            using (var markedProductRep = RepositoryFactory.CreateMarkedProductRep())
            {
                markedProduct = markedProductRep.GetMarkedProductById(markedProductId);
                if (markedProduct == null)
                    throw new PharmacyManagerException("No such marked product in database") { PharmacyManager = null };
            }
            using (var markInfoRep = RepositoryFactory.CreateMarkInfoRep())
            {
                MarkInfo markInfo = markInfoRep.GetMarkInfoById(markedProduct.Mark_info_id);
                if (markInfo.P_count < cnt)
                    throw new PharmacyManagerException("Database has less products than you require") { PharmacyManager = null };
            }
            using (var markedProductRep = RepositoryFactory.CreateMarkedProductRep())
            {
                markedProductRep.WriteOffProduct(markedProductId, cnt, Employee.id, reason);
            }
        }

        public void AddInvoice(Invoice invoice)
        {
            using (IVendorRepository vendorRep = RepositoryFactory.CreateVendorRep())
            {
                if (vendorRep.GetVendorById(invoice.Id) == null)
                    throw new PharmacyManagerException();
            }
            invoice.created_employee_id = Employee.id;
            invoice.invoice_date = DateTime.Now;
            using (IInvoiceRepository invoiceRep = RepositoryFactory.CreateInvoiceRep())
            {
                invoiceRep.Create(invoice);
            }
        }

        public void AddProductToInvoice(int invoiceId, IncomeProduct incomeProduct)
        {
            Invoice invoice = null;
            using (var invoiceRep = RepositoryFactory.CreateInvoiceRep())
            {
                invoice = invoiceRep.GetInvoiceById(invoiceId);
            }
            if (invoice == null)
                throw new PharmacyManagerException("No such invoice in database") { PharmacyManager = this };
            Product product = null;
            using (var productRep = RepositoryFactory.CreateProductRep())
            {
                product = productRep.GetProductById(incomeProduct.Product_id);
            }
            if (product == null)
                throw new PharmacyManagerException("No such product in database") { PharmacyManager = this };

            string[] validatedMeasureUnits = ConfigurationManager.AppSettings["measureUnits"].Split(",");
            if (Array.IndexOf(validatedMeasureUnits, incomeProduct.Measure_unit) == -1)
                throw new PharmacyManagerException("Not allowed measure unit value") { PharmacyManager = this };
            if (incomeProduct.Series < 0)
                throw new PharmacyManagerException("Series must be greater than zero") { PharmacyManager = this };
            if (incomeProduct.Vendor_vax < 0)
                throw new PharmacyManagerException("Vendor vax must be greater than zero") { PharmacyManager = this };
            if (incomeProduct.Production_date > DateTime.Now)
                throw new PharmacyManagerException("Production date in future") { PharmacyManager = this };
            if (incomeProduct.Vendor_price < 0)
                throw new PharmacyManagerException("Price must be greater than zero") { PharmacyManager = this };
            if (incomeProduct.P_count < 0)
                throw new PharmacyManagerException("Count must be greater than zero") { PharmacyManager = this };

            using (var incomeProductRep = RepositoryFactory.CreateIncomeProductRep())
            {
                incomeProductRep.Create(incomeProduct);
            }
        }

        public void RemoveInvoice(int invoiceId)
        {
            using (var invoiceRep = RepositoryFactory.CreateInvoiceRep())
            {
                if (invoiceRep.GetInvoiceById(invoiceId) == null)
                    throw new PharmacyManagerException("No invoice with such id") { PharmacyManager = this };
            }
            using (var incomeProductRep = RepositoryFactory.CreateIncomeProductRep())
            {
                IEnumerable<IncomeProduct> incomeProducts = incomeProductRep.GetIncomeProductByInvoiceId(invoiceId);
                foreach (var item in incomeProducts)
                    incomeProductRep.Delete(item.Id);
            }
            using (var invoiceRep = RepositoryFactory.CreateInvoiceRep())
            {
                invoiceRep.Delete(invoiceId);
            }
        }

        public void ApproveInvoice(int invoiceId)
        {
            Invoice invoice = null;
            using (var invoiceRep = RepositoryFactory.CreateInvoiceRep())
            {
                invoice = invoiceRep.GetInvoiceById(invoiceId);
                if (invoice == null)
                    throw new PharmacyManagerException("No invoice with such id") { PharmacyManager = this };
                if (invoice.i_state != "accepted")
                    throw new PharmacyManagerException("Invoice must be accepted previously");
            }
            invoice.approved_employee_id = Employee.id;
            using (var invoiceRep = RepositoryFactory.CreateInvoiceRep())
            {
                invoiceRep.ApproveInvoice(invoiceId, Employee.id);
            }
        }

        public void UpdateInvoice(Invoice invoice)
        {
            if (invoice.receiving_date != null && invoice.receiving_date < invoice.invoice_date)
            {
                throw new PharmacyManagerException("Receiving date must be greater than invoice date")
                { PharmacyManager = this };
            }
            if (invoice.shipping_date != null && invoice.shipping_date < invoice.invoice_date)
            {
                throw new PharmacyManagerException("Shipping date must be greater than invoice date")
                { PharmacyManager = this };
            }
            if (invoice.shipping_date != null && invoice.receiving_date != null 
                && invoice.shipping_date > invoice.receiving_date)
            {
                throw new PharmacyManagerException("Receiving date must be greater than shipping date")
                { PharmacyManager = this };
            }

            using (var invoiceRep = RepositoryFactory.CreateInvoiceRep())
            {
                invoiceRep.Update(invoice);
            }
        }

        public void UpdateProductInformation(Product product)
        {
            using (var productRep = RepositoryFactory.CreateProductRep())
            {
                productRep.Update(product);
            }
        }

        public void AddCategory(string title, int? parentCategoryId, string description)
        {
            if (parentCategoryId != null)
            {
                using (var categoryRep = RepositoryFactory.CreateCategoryRep())
                {
                    var parentCategory = categoryRep.GetCategoryById(parentCategoryId.Value);
                    if (parentCategory == null)
                        throw new PharmacyManagerException("No parent category with such id") { PharmacyManager = this };
                }
            }
            var category = new Category
            {
                Title = title,
                Parent_category_id = parentCategoryId,
                C_description = description
            };
            using (var categoryRep = RepositoryFactory.CreateCategoryRep())
            {
                categoryRep.Create(category);
            }
        }

        public void SetCategoryToProduct(int categoryId, int productId)
        {
            using (var productRep = RepositoryFactory.CreateProductRep())
            {
                if (productRep.GetProductById(productId) == null)
                    throw new PharmacyManagerException("No product with such id") { PharmacyManager = this };
            }

            using (var categoryRep = RepositoryFactory.CreateCategoryRep())
            {
                categoryRep.AddCategoryToProduct(categoryId, productId);
            }
        }

        public void ApproveProductRequest(int productRequestId)
        {
            using (var productRequestRep = RepositoryFactory.CreateProductRequestRep())
            {
                ProductRequest request = productRequestRep.GetProductRequestById(productRequestId);
                if (request == null)
                    throw new PharmacyManagerException("No product request with such id") { PharmacyManager = this };
                if (request.approved != 0)
                    throw new PharmacyManagerException("Product request with such id is already approved");
            }

            using (var productRequestRep = RepositoryFactory.CreateProductRequestRep())
            {
                productRequestRep.Approve(productRequestId, Employee.id);
            }
        }

        public IEnumerable<Invoice> GetInvoices()
        {
            using (var invoiceRep = RepositoryFactory.CreateInvoiceRep())
            {
                return invoiceRep.GetInvoices();
            }
        }

        public IEnumerable<Employee> GetEmployees()
        {
            using (var employeeRep = RepositoryFactory.CreateEmployeeRep())
            {
                return employeeRep.GetEmployees();
            }
        }

        public void AddInvoiceWithProducts(Invoice invoice, IEnumerable<IncomeProduct> incomeProducts)
        {
            invoice.created_employee_id = Employee.id;
            invoice.invoice_date = DateTime.Now;
            invoice.i_state = "init";

            foreach (var item in incomeProducts)
                item.Invoice_id = invoice.Id;

            using (var invoiceRep = RepositoryFactory.CreateInvoiceRep())
            {
                invoiceRep.AddInvoiceWithProducts(invoice, incomeProducts);
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

        public Vendor GetVendorById(int id)
        {
            using (var vendorRep = RepositoryFactory.CreateVendorRep())
            {
                return vendorRep.GetVendorById(id);
            }
        }

        public Employee GetEmployeeById(int id)
        {
            using (var employeeRep = RepositoryFactory.CreateEmployeeRep())
            {
                return employeeRep.GetEmployeeById(id);
            }
        }

        public PersonMetadata GetPersonMetadataById(int id)
        {
            using (var metadataRep = RepositoryFactory.CreatePersonMetadataRep())
            {
                return metadataRep.GetPersonMetadataById(id);
            }
        }

        public void AddProduct(Product product)
        {
            using (var productRep = RepositoryFactory.CreateProductRep())
            {
                productRep.Create(product);
            }
        }

        public Manufacturer GetManufacturerById(int id)
        {
            using (var manufacturerRep = RepositoryFactory.CreateManufacturerRep())
            {
                return manufacturerRep.GetManufacturerById(id);
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var productRep = RepositoryFactory.CreateProductRep())
            {
                productRep.Update(product);
            }
        }

        public IEnumerable<MarkedProduct> GetMarkedProducts()
        {
            using (var markedProductRep = RepositoryFactory.CreateMarkedProductRep())
            {
                return markedProductRep.GetMarkedProducts();
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            using (var productRep = RepositoryFactory.CreateProductRep())
            {
                return productRep.GetProducts();
            }
        }

        public IEnumerable<WriteOffProduct> GetWriteOffProducts()
        {
            using (var writeOffProductRep = RepositoryFactory.CreateWriteOffProductRep())
            {
                return writeOffProductRep.GetWriteOffProducts();
            }
        }

        public IEnumerable<ProductRequest> GetProductRequests()
        {
            using (var productRequestRep = RepositoryFactory.CreateProductRequestRep())
            {
                return productRequestRep.GetProductRequests();
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

        public IEnumerable<IncomeProduct> GetIncomeProductsByInvoiceId(int id)
        {
            using (var incomeProductRep = RepositoryFactory.CreateIncomeProductRep())
            {
                return incomeProductRep.GetIncomeProductByInvoiceId(id);
            }
        }

        public MarkInfo GetMarkInfoById(int id)
        {
            using (var markInfoRep = RepositoryFactory.CreateMarkInfoRep())
            {
                return markInfoRep.GetMarkInfoById(id);
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

        public ProductRequest GetProductRequestById(int id)
        {
            using (var productRequestRep = RepositoryFactory.CreateProductRequestRep())
            {
                return productRequestRep.GetProductRequestById(id);
            }
        }

        public IEnumerable<Vendor> GetVendors()
        {
            using (var vendorRep = RepositoryFactory.CreateVendorRep())
            {
                return vendorRep.GetVendors();
            }
        }

        public Invoice GetInvoiceById(int id)
        {
            using (var invoiceRep = RepositoryFactory.CreateInvoiceRep())
            {
                return invoiceRep.GetInvoiceById(id);
            }
        }
        public void UpdateEmployee(Employee employee)
        {
            using (var employeeRep = RepositoryFactory.CreateEmployeeRep())
            {
                employeeRep.Update(employee);
            }
        }

        public void UpdatePersonMetadata(PersonMetadata personMetadata)
        {
            PersonMetadataValidator.Validate(personMetadata);
            using (var metadataRep = RepositoryFactory.CreatePersonMetadataRep())
            {
                metadataRep.Update(personMetadata);
            }
        }

    }
}
