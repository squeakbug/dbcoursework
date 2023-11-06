using System;
using Xunit;
using Autofac;
using Autofac.Extras.Moq;
using System.Configuration;
using System.Collections.Generic;

using DefaultBusinessLogic;
using DefaultBusinessLogic.Exceptions;
using BusinessSpecification;
using BusinessSpecification.Entities;
using BusinessSpecification.RepositoryInterfaces;

namespace TestBusinessLogic
{
    using static DataSampler;
    public class DefaultPharmacyManagerTest
    {

        [Fact]
        public void TestRegistrate()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepositoryFactory>();
                var repositoryFactory = mock.Create<IRepositoryFactory>();
                SetupFactoryForEmployees(repositoryFactory, mock);
                var empl = GetEmployeeSample(repositoryFactory)[0];

                var auth = mock.Create<DefaultAuthManager>(
                    new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(2, new DefaultEmployeeFactory()));

                var pm = mock.Create<DefaultPharmacyManager>(
                    new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(1, empl),
                    new PositionalParameter(2, auth));

                var newEmployee = new Employee
                {
                    appointment = "stockman",
                    person_metadata = new PersonMetadata
                    {
                        First_name = "stockman", Middle_name = "stockman",
                        Last_name = "stockman", Email = "stockman@stockman",
                        Phone = "9099099999"
                    },
                    e_login = "stockman",
                };

                pm.Registrate(newEmployee, "123456", "123456");

                mock.Mock<IEmployeeRepository>()
                    .Verify(x => x.RegistrateEmployee(Moq.It.Is<Employee>(
                        s => s.person_metadata.First_name == "stockman")));

                Assert.Throws<PharmacyManagerException>(() => pm.Registrate(newEmployee, "123456", "124356"));
            }
        }

        [Fact]
        public void TestBadAppointmentRegistrate()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepositoryFactory>();
                var repositoryFactory = mock.Create<IRepositoryFactory>();
                SetupFactoryForEmployees(repositoryFactory, mock);
                var empl = GetEmployeeSample(repositoryFactory)[0];

                var auth = mock.Create<DefaultAuthManager>(
                    new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(2, new DefaultEmployeeFactory()));

                var pm = mock.Create<DefaultPharmacyManager>(
                    new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(1, empl),
                    new PositionalParameter(2, auth));

                var newEmployee = new Employee
                {
                    appointment = "teacher",
                    person_metadata = new PersonMetadata
                    {
                        First_name = "stockman",
                        Middle_name = "stockman",
                        Last_name = "stockman",
                        Email = "stockman@stockman",
                        Phone = "9099099999"
                    },
                    e_login = "stockman",
                };

                Assert.Throws<PharmacyManagerException>(() => pm.Registrate(newEmployee, "123456", "123456"));
            }
        }

        [Fact]
        public void TestOccupiedLoginRegistrate()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepositoryFactory>();
                var repositoryFactory = mock.Create<IRepositoryFactory>();
                SetupFactoryForEmployees(repositoryFactory, mock);
                var empl = GetEmployeeSample(repositoryFactory)[0];

                mock.Mock<IEmployeeRepository>()
                    .Setup(x => x.GetEmployeeByLogin("EL1"))
                    .Returns(GetEmployeeSample(repositoryFactory).Find(x => x.e_login == "EL1"));

                var auth = mock.Create<DefaultAuthManager>(
                    new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(2, new DefaultEmployeeFactory()));

                var pm = mock.Create<DefaultPharmacyManager>(
                    new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(1, empl),
                    new PositionalParameter(2, auth));

                var newEmployee = new Employee
                {
                    appointment = "stockman",
                    person_metadata = new PersonMetadata
                    {
                        First_name = "stockman",
                        Middle_name = "stockman",
                        Last_name = "stockman",
                        Email = "stockman@stockman",
                        Phone = "9099099999"
                    },
                    e_login = "EL1",
                };

                Assert.Throws<PharmacyManagerException>(() => pm.Registrate(newEmployee, "123456", "123456"));
            }
        }

        [Fact]
        public void TestNoProductRequestIdApproveProductRequest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepositoryFactory>();
                var repositoryFactory = mock.Create<IRepositoryFactory>();
                SetupFactoryForProductRequests(repositoryFactory, mock);
                SetupFactoryForEmployees(repositoryFactory, mock);
                var empl = GetEmployeeSample(repositoryFactory)[0];

                var auth = mock.Create<DefaultAuthManager>(
                    new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(2, new DefaultEmployeeFactory()));

                var pm = mock.Create<DefaultPharmacyManager>(
                    new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(1, empl),
                    new PositionalParameter(2, auth));

                mock.Mock<IProductRequestRepository>()
                    .Setup(x => x.GetProductRequestById(1))
                    .Returns((ProductRequest)null);
                mock.Mock<IProductRequestRepository>()
                    .Setup(x => x.Approve(1, 1));

                Assert.Throws<PharmacyManagerException>(() => pm.ApproveProductRequest(1));
            }
        }

        [Fact]
        public void NoProductSetCategoryToProduct()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepositoryFactory>();
                var repositoryFactory = mock.Create<IRepositoryFactory>();
                SetupFactoryForCategory(mock);
                SetupFactoryForProducts(repositoryFactory, mock);
                SetupFactoryForEmployees(repositoryFactory, mock);
                var empl = GetEmployeeSample(repositoryFactory)[0];

                var auth = mock.Create<DefaultAuthManager>(
                    new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(2, new DefaultEmployeeFactory()));

                var pm = mock.Create<DefaultPharmacyManager>(
                    new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(1, empl),
                    new PositionalParameter(2, auth));

                int productId = 1;
                int categoryId = 1;

                mock.Mock<IProductRepository>()
                    .Setup(x => x.GetProductById(productId))
                    .Returns((Product)null);
                mock.Mock<ICategoryRepository>()
                    .Setup(x => x.GetCategoryById(categoryId))
                    .Returns(GetCategorySample()[0]);

                Assert.Throws<PharmacyManagerException>(() => pm.SetCategoryToProduct(categoryId, productId));
            }
        }

        [Fact]
        public void NoCategorySetCategoryToProduct()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepositoryFactory>();
                var repositoryFactory = mock.Create<IRepositoryFactory>();
                SetupFactoryForCategory(mock);
                SetupFactoryForProducts(repositoryFactory, mock);
                SetupFactoryForEmployees(repositoryFactory, mock);
                var empl = GetEmployeeSample(repositoryFactory)[0];

                var auth = mock.Create<DefaultAuthManager>(
                    new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(2, new DefaultEmployeeFactory()));

                var pm = mock.Create<DefaultPharmacyManager>(
                    new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(1, empl),
                    new PositionalParameter(2, auth));

                int productId = 1;
                int categoryId = 1;

                mock.Mock<IProductRepository>()
                    .Setup(x => x.GetProductById(productId))
                    .Returns(GetProductSample(repositoryFactory)[0]);
                mock.Mock<ICategoryRepository>()
                    .Setup(x => x.GetCategoryById(categoryId))
                    .Returns((Category)null);

                Assert.Throws<PharmacyManagerException>(() => pm.SetCategoryToProduct(categoryId, productId));
            }
        }

        [Fact]
        public void NoInvoiceUpdateInvoice()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepositoryFactory>();
                var repositoryFactory = mock.Create<IRepositoryFactory>();
                SetupFactoryForInvoices(repositoryFactory, mock);
                SetupFactoryForEmployees(repositoryFactory, mock);
                var empl = GetEmployeeSample(repositoryFactory)[0];

                var auth = mock.Create<DefaultAuthManager>(
                    new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(2, new DefaultEmployeeFactory()));

                var pm = mock.Create<DefaultPharmacyManager>(
                    new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(1, empl),
                    new PositionalParameter(2, auth));

                var invoice = GetInvoiceSample(repositoryFactory)[0];
                invoice.invoice_date = DateTime.Now.AddDays(-20);
                invoice.shipping_date = DateTime.Now.AddDays(-10);
                invoice.receiving_date = DateTime.Now.AddDays(-5);

                mock.Mock<IInvoiceRepository>()
                    .Setup(x => x.GetInvoiceById(1))
                    .Returns((Invoice)null);

                Assert.Throws<PharmacyManagerException>(() => pm.UpdateInvoice(invoice));
            }
        }

        [Fact]
        public void BadReceivingDateUpdateInvoice()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepositoryFactory>();
                var repositoryFactory = mock.Create<IRepositoryFactory>();
                SetupFactoryForInvoices(repositoryFactory, mock);
                SetupFactoryForEmployees(repositoryFactory, mock);
                var empl = GetEmployeeSample(repositoryFactory)[0];

                var auth = mock.Create<DefaultAuthManager>(
                    new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(2, new DefaultEmployeeFactory()));

                var pm = mock.Create<DefaultPharmacyManager>(
                    new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(1, empl),
                    new PositionalParameter(2, auth));

                var invoice = GetInvoiceSample(repositoryFactory)[0];
                invoice.invoice_date = DateTime.Now.AddDays(-20);
                invoice.shipping_date = DateTime.Now.AddDays(-10);
                invoice.receiving_date = DateTime.Now.AddDays(-15);

                mock.Mock<IInvoiceRepository>()
                    .Setup(x => x.GetInvoiceById(1))
                    .Returns(invoice);

                Assert.Throws<PharmacyManagerException>(() => pm.UpdateInvoice(invoice));
            }
        }

        [Fact]
        public void BadShippingDateUpdateInvoice()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepositoryFactory>();
                var repositoryFactory = mock.Create<IRepositoryFactory>();
                SetupFactoryForInvoices(repositoryFactory, mock);
                SetupFactoryForEmployees(repositoryFactory, mock);
                var empl = GetEmployeeSample(repositoryFactory)[0];

                var auth = mock.Create<DefaultAuthManager>(
                    new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(2, new DefaultEmployeeFactory()));

                var pm = mock.Create<DefaultPharmacyManager>(
                    new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(1, empl),
                    new PositionalParameter(2, auth));

                var invoice = GetInvoiceSample(repositoryFactory)[0];
                invoice.invoice_date = DateTime.Now.AddDays(-20);
                invoice.shipping_date = DateTime.Now.AddDays(-30);
                invoice.receiving_date = DateTime.Now.AddDays(-5);

                mock.Mock<IInvoiceRepository>()
                    .Setup(x => x.GetInvoiceById(1))
                    .Returns(invoice);

                Assert.Throws<PharmacyManagerException>(() => pm.UpdateInvoice(invoice));
            }
        }

        [Fact]
        public void BadMeasureUnitAddProductToInvoice()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepositoryFactory>();
                var repositoryFactory = mock.Create<IRepositoryFactory>();
                SetupFactoryForInvoices(repositoryFactory, mock);
                SetupFactoryForProducts(repositoryFactory, mock);
                SetupFactoryForIncomeProducts(repositoryFactory, mock);
                SetupFactoryForEmployees(repositoryFactory, mock);
                var empl = GetEmployeeSample(repositoryFactory)[0];

                var auth = mock.Create<DefaultAuthManager>(
                    new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(2, new DefaultEmployeeFactory()));

                var pm = mock.Create<DefaultPharmacyManager>(
                    new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(1, empl),
                    new PositionalParameter(2, auth));

                var invoice = GetInvoiceSample(repositoryFactory)[0];
                var product = GetProductSample(repositoryFactory)[0];
                var incomeProduct = GetIncomeProductSample(repositoryFactory)[0];

                mock.Mock<IInvoiceRepository>()
                    .Setup(x => x.GetInvoiceById(1))
                    .Returns(invoice);
                mock.Mock<IProductRepository>()
                    .Setup(x => x.GetProductById(1))
                    .Returns(product);
                mock.Mock<IIncomeProductRepository>()
                    .Setup(x => x.Create(incomeProduct));

                Assert.Throws<PharmacyManagerException>(
                    () => pm.AddProductToInvoice(1, 1, 10, "g", 1, 10, DateTime.Now, 10, "Main storage"));
            }
        }
    }
}
