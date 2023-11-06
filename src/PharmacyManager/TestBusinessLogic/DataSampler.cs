using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Extras.Moq;

using BusinessSpecification;
using BusinessSpecification.Entities;
using BusinessSpecification.RepositoryInterfaces;
using BusinessSpecification.Filters;

/* Для тестов такое решение пойдет (наверно)), но на будущее следует формировать
 * интерфейс методов так, чтобы пользователь знал, какие зависимости потребуется
 * подменить перед тестированием функции */

namespace TestBusinessLogic
{
    static class DataSampler
    {
        public static void SetupFactoryForCategory(AutoMock mock)
        {
            mock.Mock<ICategoryRepository>()
                .Setup(x => x.GetCategories())
                .Returns(GetCategorySample());
            var rep = mock.Create<ICategoryRepository>();

            mock.Mock<IRepositoryFactory>()
                    .Setup(x => x.CreateCategoryRep())
                    .Returns(rep);
        }

        public static void SetupFactoryForPersonMetadata(AutoMock mock)
        {
            mock.Mock<IPersonMetadataRepository>()
                .Setup(x => x.GetPersonMetadatas())
                .Returns(GetPersonMetadataSample());
            var rep = mock.Create<IPersonMetadataRepository>();

            mock.Mock<IRepositoryFactory>()
                    .Setup(x => x.CreatePersonMetadataRep())
                    .Returns(rep);
        }

        public static void SetupFactoryForVendors(AutoMock mock)
        {
            mock.Mock<IVendorRepository>()
                .Setup(x => x.GetVendors())
                .Returns(GetVendorSample());
            var rep = mock.Create<IVendorRepository>();

            mock.Mock<IRepositoryFactory>()
                    .Setup(x => x.CreateVendorRep())
                    .Returns(rep);
        }

        public static void SetupFactoryForManufacturers(AutoMock mock)
        {
            mock.Mock<IManufacturerRepository>()
                .Setup(x => x.GetManufacturers())
                .Returns(GetManufacturerSample());
            var rep = mock.Create<IManufacturerRepository>();

            mock.Mock<IRepositoryFactory>()
                    .Setup(x => x.CreateManufacturerRep())
                    .Returns(rep);
        }

        public static void SetupFactoryForProducts(IRepositoryFactory factory, AutoMock mock)
        {
            SetupFactoryForManufacturers(mock);
            SetupFactoryForCategory(mock);

            mock.Mock<IProductRepository>()
                .Setup(x => x.GetProducts())
                .Returns(GetProductSample(factory));
            var rep = mock.Create<IProductRepository>();

            mock.Mock<IRepositoryFactory>()
                    .Setup(x => x.CreateProductRep())
                    .Returns(rep);
        }

        public static void SetupFactoryForEmployees(IRepositoryFactory factory, AutoMock mock)
        {
            SetupFactoryForPersonMetadata(mock);

            mock.Mock<IEmployeeRepository>()
                .Setup(x => x.GetEmployees())
                .Returns(GetEmployeeSample(factory));
            var rep = mock.Create<IEmployeeRepository>();

            mock.Mock<IRepositoryFactory>()
                    .Setup(x => x.CreateEmployeeRep())
                    .Returns(rep);
        }

        public static void SetupFactoryForInvoices(IRepositoryFactory factory, AutoMock mock)
        {
            // Вот как в этом моменте узнать, что необходимо подменить?!
            SetupFactoryForVendors(mock);
            SetupFactoryForEmployees(factory, mock);

            mock.Mock<IInvoiceRepository>()
                .Setup(x => x.GetInvoices())
                .Returns(GetInvoiceSample(factory));
            var rep = mock.Create<IInvoiceRepository>();

            mock.Mock<IRepositoryFactory>()
                    .Setup(x => x.CreateInvoiceRep())
                    .Returns(rep);
        }

        public static void SetupFactoryForIncomeProducts(IRepositoryFactory factory, AutoMock mock)
        {
            SetupFactoryForProducts(factory, mock);
            SetupFactoryForInvoices(factory, mock);

            mock.Mock<IIncomeProductRepository>()
                .Setup(x => x.GetIncomeProducts(null))
                .Returns(GetIncomeProductSample(factory));
            var rep = mock.Create<IIncomeProductRepository>();

            mock.Mock<IRepositoryFactory>()
                    .Setup(x => x.CreateIncomeProductRep())
                    .Returns(rep);
        }

        public static void SetupFactoryForMarkInfos(IRepositoryFactory factory, AutoMock mock)
        {
            SetupFactoryForIncomeProducts(factory, mock);
            SetupFactoryForEmployees(factory, mock);

            mock.Mock<IMarkInfoRepository>()
                .Setup(x => x.GetMarkInfos())
                .Returns(GetMarkInfoSample(factory));
            var rep = mock.Create<IMarkInfoRepository>();

            mock.Mock<IRepositoryFactory>()
                    .Setup(x => x.CreateMarkInfoRep())
                    .Returns(rep);
        }

        public static void SetupFactoryForMarkedProducts(IRepositoryFactory factory, AutoMock mock)
        {
            SetupFactoryForMarkInfos(factory, mock);

            mock.Mock<IMarkedProductRepository>()
                .Setup(x => x.GetMarkedProducts())
                .Returns(GetMarkedProductSample(factory));
            var rep = mock.Create<IMarkedProductRepository>();

            mock.Mock<IRepositoryFactory>()
                    .Setup(x => x.CreateMarkedProductRep())
                    .Returns(rep);
        }

        public static void SetupFactoryForReceipts(IRepositoryFactory factory, AutoMock mock)
        {
            SetupFactoryForEmployees(factory, mock);

            mock.Mock<IReceiptRepository>()
                .Setup(x => x.GetReceipts())
                .Returns(GetReceiptSample(factory));
            var rep = mock.Create<IReceiptRepository>();

            mock.Mock<IRepositoryFactory>()
                    .Setup(x => x.CreateReceiptRep())
                    .Returns(rep);
        }

        public static void SetupFactoryForProductRequests(IRepositoryFactory factory, AutoMock mock)
        {
            SetupFactoryForProducts(factory, mock);
            SetupFactoryForPersonMetadata(mock);
            SetupFactoryForEmployees(factory, mock);

            mock.Mock<IProductRequestRepository>()
                .Setup(x => x.GetProductRequests())
                .Returns(GetProductRequestSample(factory));
            var rep = mock.Create<IProductRequestRepository>();

            mock.Mock<IRepositoryFactory>()
                    .Setup(x => x.CreateProductRequestRep())
                    .Returns(rep);
        }

        public static List<Category> GetCategorySample()
        {
            return new List<Category>
            {
                new Category { Id = 1, C_description = "Category_1", Title = "Category_1" },
                new Category { Id = 2, C_description = "Category_2", Title = "Category_2" },
                new Category { Id = 3, C_description = "Category_3", Title = "Category_3" },
            };
        }

        public static List<Employee> GetEmployeeSample(IRepositoryFactory factory)
        {
            List<PersonMetadata> personMetadatas = null;
            using (var rep = factory.CreatePersonMetadataRep())
                personMetadatas = new List<PersonMetadata>(rep.GetPersonMetadatas());

            return new List<Employee>
            {
                new Employee
                {
                    id = 1,
                    e_login = "EL1", appointment = "EA1",
                    person_metadata = personMetadatas[0], p_hash = "EPH1"
                },
                new Employee
                {
                    id = 2,
                    e_login = "EL2", appointment = "EA2",
                    person_metadata = personMetadatas[1], p_hash = "EPH2"
                },
                new Employee
                {
                    id = 3,
                    e_login = "EL3", appointment = "EA3",
                    person_metadata = personMetadatas[2], p_hash = "EPH3"
                },
            };
        }

        public static List<IncomeProduct> GetIncomeProductSample(IRepositoryFactory factory)
        {
            List<Product> products = null;
            List<Invoice> invoices = null;
            using (var rep = factory.CreateProductRep())
                products = new List<Product>(rep.GetProducts());
            using (var rep = factory.CreateInvoiceRep())
                invoices = new List<Invoice>(rep.GetInvoices());

            return new List<IncomeProduct>
            {
                new IncomeProduct
                {
                    Id = 1,
                    Measure_unit = "g", Invoice = invoices[0], Product = products[0],
                    Production_date = DateTime.Now, P_count = 100, Series = 100, Vendor_vax = 10,
                    Storage_location = "IPSL1", Vendor_price = 100
                },
                new IncomeProduct
                {
                    Id = 2,
                    Measure_unit = "g", Invoice = invoices[1], Product = products[1],
                    Production_date = DateTime.Now, P_count = 100, Series = 100, Vendor_vax = 10,
                    Storage_location = "IPSL2", Vendor_price = 100
                },
                new IncomeProduct
                {
                    Id = 3,
                    Measure_unit = "g", Invoice = invoices[2], Product = products[2],
                    Production_date = DateTime.Now, P_count = 100, Series = 100, Vendor_vax = 10,
                    Storage_location = "IPSL3", Vendor_price = 100
                }
            };
        }

        public static List<Invoice> GetInvoiceSample(IRepositoryFactory factory)
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
                    Id = 1, accepted_employee = employees[0], approved_employee = employees[1],
                    created_employee = employees[2], document_gtin = "1000", invoice_date = DateTime.Now,
                    invoice_number = 1000, i_state = "approved", receiving_date = DateTime.Now,
                    shipping_date = DateTime.Now, vendor = vendors[0],
                },
                new Invoice
                {
                    Id = 2, accepted_employee = employees[0], approved_employee = employees[1],
                    created_employee = employees[2], document_gtin = "1000", invoice_date = DateTime.Now,
                    invoice_number = 2000, i_state = "approved", receiving_date = DateTime.Now,
                    shipping_date = DateTime.Now, vendor = vendors[1],
                },
                new Invoice
                {
                    Id = 3, accepted_employee = employees[0], approved_employee = employees[1],
                    created_employee = employees[2], document_gtin = "1000", invoice_date = DateTime.Now,
                    invoice_number = 3000, i_state = "approved", receiving_date = DateTime.Now,
                    shipping_date = DateTime.Now, vendor = vendors[2],
                }
            };
        }

        public static List<Manufacturer> GetManufacturerSample()
        {
            return new List<Manufacturer>
            {
                new Manufacturer { Id = 1, M_name = "MN1", Concern = "MC1", Country = "MC1" },
                new Manufacturer { Id = 2, M_name = "MN1", Concern = "MC1", Country = "MC1" },
                new Manufacturer { Id = 3, M_name = "MN1", Concern = "MC1", Country = "MC1" }
            };
        }

        public static List<MarkedProduct> GetMarkedProductSample(IRepositoryFactory factory)
        {
            List<MarkInfo> markInfos = null;
            using (var rep = factory.CreateMarkInfoRep())
                markInfos = new List<MarkInfo>(rep.GetMarkInfos());

            return new List<MarkedProduct>
            {
                new MarkedProduct { Id = 1, Sold = 0, Mark_info = markInfos[0] },
                new MarkedProduct { Id = 2, Sold = 0, Mark_info = markInfos[1] },
                new MarkedProduct { Id = 3, Sold = 0, Mark_info = markInfos[2] }
            };
        }

        public static List<MarkInfo> GetMarkInfoSample(IRepositoryFactory factory)
        {
            List<Employee> employees = null;
            List<IncomeProduct> incomeProducts = null;
            using (var rep = factory.CreateEmployeeRep())
                employees = new List<Employee>(rep.GetEmployees());
            using (var rep = factory.CreateIncomeProductRep())
                incomeProducts = new List<IncomeProduct>(rep.GetIncomeProducts(null));

            return new List<MarkInfo>
            {
                new MarkInfo
                {
                    Id = 1,
                    Storage_location = "MISL1", approved_date = DateTime.Now,
                    Approved_employee = employees[0], Income_product = incomeProducts[0],
                    markup = 16, P_count = 100
                },
                new MarkInfo
                {
                    Id = 2,
                    Storage_location = "MISL2", approved_date = DateTime.Now,
                    Approved_employee = employees[1], Income_product = incomeProducts[1],
                    markup = 16, P_count = 100
                },
                new MarkInfo
                {
                    Id = 3,
                    Storage_location = "MISL3", approved_date = DateTime.Now,
                    Approved_employee = employees[2], Income_product = incomeProducts[2],
                    markup = 16, P_count = 100
                }
            };
        }

        public static List<Product> GetProductSample(IRepositoryFactory factory)
        {
            List<Category> categories = null;
            List<Manufacturer> manufacturers = null;
            using (var rep = factory.CreateCategoryRep())
                categories = new List<Category>(rep.GetCategories());
            using (var rep = factory.CreateManufacturerRep())
                manufacturers = new List<Manufacturer>(rep.GetManufacturers());

            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Categories = categories, Articul = "1000", Count_in_package = 100,
                    Default_markup = 18, Dosage = 20, Dosage_form = "tablets", Gtin = "1000",
                    Instruction = null, International_name = "PN1", Leave_condition = "PLC1",
                    Manufacturer = manufacturers[0], Maximum_markup = 20, Maximum_shelf_life = 60,
                    Photo = null, Primacy_packaging = "PPP1", P_description = null, P_name = "PN1",
                    Storage_temperature = 25, Threashold_count = 0, Trademark = "PT1"
                },
                new Product
                {
                    Id = 2,
                    Categories = categories, Articul = "1000", Count_in_package = 100,
                    Default_markup = 18, Dosage = 20, Dosage_form = "tablets", Gtin = "1000",
                    Instruction = null, International_name = "PN2", Leave_condition = "PLC2",
                    Manufacturer = manufacturers[1], Maximum_markup = 20, Maximum_shelf_life = 60,
                    Photo = null, Primacy_packaging = "PPP2", P_description = null, P_name = "PN2",
                    Storage_temperature = 25, Threashold_count = 0, Trademark = "PT2"
                },
                new Product
                {
                    Id = 3,
                    Categories = categories, Articul = "1000", Count_in_package = 100,
                    Default_markup = 18, Dosage = 20, Dosage_form = "tablets", Gtin = "1000",
                    Instruction = null, International_name = "PN3", Leave_condition = "PLC3",
                    Manufacturer = manufacturers[2], Maximum_markup = 20, Maximum_shelf_life = 60,
                    Photo = null, Primacy_packaging = "PPP3", P_description = null, P_name = "PN3",
                    Storage_temperature = 25, Threashold_count = 0, Trademark = "PT3"
                }
            };
        }

        public static List<PersonMetadata> GetPersonMetadataSample()
        {
            return new List<PersonMetadata>
            {
                new PersonMetadata
                {
                    Id = 1,
                    First_name = "PMFN1", Middle_name = "PMMN1",
                    Last_name = "PMLN1", Email = "PME1", Phone = "PMP1"
                },
                new PersonMetadata
                {
                    Id = 2,
                    First_name = "PMFN2", Middle_name = "PMMN2",
                    Last_name = "PMLN2", Email = "PME2", Phone = "PMP2"
                },
                new PersonMetadata
                {
                    Id = 3,
                    First_name = "PMFN3", Middle_name = "PMMN3",
                    Last_name = "PMLN3", Email = "PME3", Phone = "PMP3"
                },
            };
        }

        public static List<ProductRequest> GetProductRequestSample(IRepositoryFactory factory)
        {
            List<PersonMetadata> personMetadatas = null;
            List<Employee> employees = null;
            List<Product> products = null;
            using (var rep = factory.CreateProductRep())
                products = new List<Product>(rep.GetProducts());
            using (var rep = factory.CreatePersonMetadataRep())
                personMetadatas = new List<PersonMetadata>(rep.GetPersonMetadatas());
            using (var rep = factory.CreateEmployeeRep())
                employees = new List<Employee>(rep.GetEmployees());

            return new List<ProductRequest>
            {
                new ProductRequest
                {
                    Id = 1,
                    approved = 0, Approved_date = DateTime.Now, Approved_employee = employees[0],
                    Approximate_name = "PRAN1", Customer_metadata = personMetadatas[0],
                    Product_id = products[0].Id, Pr_count = 1, Request_Date = DateTime.Now,
                    Request_employee = employees[1]
                },
                new ProductRequest
                {
                    Id = 2,
                    approved = 0, Approved_date = DateTime.Now, Approved_employee = employees[0],
                    Approximate_name = "PRAN2", Customer_metadata = personMetadatas[1],
                    Product_id = products[1].Id, Pr_count = 1, Request_Date = DateTime.Now,
                    Request_employee = employees[1]
                },
                new ProductRequest
                {
                    Id = 3,
                    approved = 0, Approved_date = DateTime.Now, Approved_employee = employees[0],
                    Approximate_name = "PRAN3", Customer_metadata = personMetadatas[2],
                    Product_id = products[2].Id, Pr_count = 1, Request_Date = DateTime.Now,
                    Request_employee = employees[2]
                }
            };
        }

        public static List<Receipt> GetReceiptSample(IRepositoryFactory factory)
        {
            List<Employee> employees = null;
            using (var rep = factory.CreateEmployeeRep())
                employees = new List<Employee>(rep.GetEmployees());

            return new List<Receipt>
            {
                new Receipt
                {
                    Id = 1,
                    Card_size = 100, Cash_size = 100, Cash_register_number = 1, Discount = 0,
                    Employee = employees[0], Leave_date = DateTime.Now, Payment_type = "card/cash",
                    Receipt_number = 1, Work_shift = 1
                },
                new Receipt
                {
                    Id = 2,
                    Card_size = 100, Cash_size = 100, Cash_register_number = 1, Discount = 0,
                    Employee = employees[1], Leave_date = DateTime.Now, Payment_type = "card/cash",
                    Receipt_number = 2, Work_shift = 1
                },
                new Receipt
                {
                    Id = 3,
                    Card_size = 100, Cash_size = 100, Cash_register_number = 1, Discount = 0,
                    Employee = employees[2], Leave_date = DateTime.Now, Payment_type = "card/cash",
                    Receipt_number = 3, Work_shift = 1
                }
            };
        }

        public static List<Vendor> GetVendorSample()
        {
            return new List<Vendor>
            {
                new Vendor { Id = 1, Full_name = "VFN1", Inn = 1000, Kpp = 1000, Phone = "VP1", Short_name = "VSN1" },
                new Vendor { Id = 2, Full_name = "VFN2", Inn = 1000, Kpp = 1000, Phone = "VP2", Short_name = "VSN2" },
                new Vendor { Id = 3, Full_name = "VFN3", Inn = 1000, Kpp = 1000, Phone = "VP3", Short_name = "VSN3" }
            };
        }

        public static List<WriteOffProduct> GetWriteOffProductSample(IRepositoryFactory factory)
        {
            List<MarkInfo> markInfos = null;
            List<Employee> employees = null;
            using (var rep = factory.CreateMarkInfoRep())
                markInfos = new List<MarkInfo>(rep.GetMarkInfos());
            using (var rep = factory.CreateEmployeeRep())
                employees = new List<Employee>(rep.GetEmployees());

            return new List<WriteOffProduct>
            {
                new WriteOffProduct
                {
                    Id = 1,
                    Mark_info = markInfos[0], Reason = "WOPR1", Write_off_date = DateTime.Now,
                    Write_off_employee = employees[0]
                },
                new WriteOffProduct
                {
                    Id = 2,
                    Mark_info = markInfos[1], Reason = "WOPR2", Write_off_date = DateTime.Now,
                    Write_off_employee = employees[1]
                },
                new WriteOffProduct
                {
                    Id = 3,
                    Mark_info = markInfos[2], Reason = "WOPR3", Write_off_date = DateTime.Now,
                    Write_off_employee = employees[2]
                }
            };
        }
    }
}
