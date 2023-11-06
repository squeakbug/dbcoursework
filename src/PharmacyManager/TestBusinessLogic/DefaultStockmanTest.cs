using System;
using Xunit;
using Autofac;
using Autofac.Extras.Moq;
using System.Collections.Generic;

using DefaultBusinessLogic;
using DefaultBusinessLogic.Exceptions;
using BusinessSpecification;
using BusinessSpecification.Entities;
using BusinessSpecification.RepositoryInterfaces;

namespace TestBusinessLogic
{
    using static DataSampler;

    public class DefaultStockmanTest
    {
        [Fact]
        public void TestDefaultChangeProductLocation()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepositoryFactory>();
                var repositoryFactory = mock.Create<IRepositoryFactory>();
                SetupFactoryForIncomeProducts(repositoryFactory, mock);
                SetupFactoryForEmployees(repositoryFactory, mock);
                var empl = GetEmployeeSample(repositoryFactory)[0];

                var mans = GetIncomeProductSample(repositoryFactory).Count;

                var incomeProduct = GetIncomeProductSample(repositoryFactory)[0];
                var expected = GetIncomeProductSample(repositoryFactory)[0];
                expected.Storage_location = "TestLocation";

                mock.Mock<IIncomeProductRepository>()
                    .Setup(x => x.GetIncomeProductById(incomeProduct.Id))
                    .Returns(incomeProduct);
                mock.Mock<IIncomeProductRepository>()
                    .Setup(x => x.Update(expected));
                var incomeProductRep = mock.Create<IIncomeProductRepository>();
                var cls = mock.Create<DefaultStockman>(new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(1, empl));

                cls.ChangeProductLocation(incomeProduct.Id, "TestLocation");

                mock.Mock<IIncomeProductRepository>()
                    .Verify(x => x.Update(Moq.It.Is<IncomeProduct>(
                        s => s.Storage_location == expected.Storage_location)), Moq.Times.Exactly(1));
            }
        }

        [Fact]
        public void TestDefaultAcceptInvoice()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepositoryFactory>();
                var repositoryFactory = mock.Create<IRepositoryFactory>();
                SetupFactoryForIncomeProducts(repositoryFactory, mock);

                var invoice = GetInvoiceSample(repositoryFactory)[0];
                var expected = GetInvoiceSample(repositoryFactory)[0];

                mock.Mock<IInvoiceRepository>()
                    .Setup(x => x.AcceptInvoice(invoice.Id, invoice.accepted_employee.id, null));
                var invoiceRep = mock.Create<IInvoiceRepository>();
                var cls = mock.Create<DefaultStockman>(new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(1, invoice.accepted_employee));

                cls.AcceptInvoice(invoice.Id, invoice.accepted_employee.id, null);

                mock.Mock<IInvoiceRepository>()
                    .Verify(x => x.AcceptInvoice(Moq.It.Is<int>(i => i == expected.Id),
                                                 Moq.It.Is<int>(i => i == expected.accepted_employee.id),
                                                 Moq.It.Is<DateTime?>(i => i == null)), Moq.Times.Exactly(1));
            }
        }

        [Fact]
        public void TestBadShippingDateAcceptInvoice()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepositoryFactory>();
                var repositoryFactory = mock.Create<IRepositoryFactory>();
                SetupFactoryForIncomeProducts(repositoryFactory, mock);

                var invoice = GetInvoiceSample(repositoryFactory)[0];

                mock.Mock<IInvoiceRepository>()
                    .Setup(x => x.AcceptInvoice(invoice.Id, invoice.accepted_employee.id, null));
                var invoiceRep = mock.Create<IInvoiceRepository>();
                var cls = mock.Create<DefaultStockman>(new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(1, invoice.accepted_employee));

                Assert.Throws<StockmanException>(
                    () => cls.AcceptInvoice(invoice.Id, invoice.accepted_employee.id, DateTime.Now.AddDays(1)));
            }
        }

        [Fact]
        public void TestGetInvoices()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepositoryFactory>();
                var repositoryFactory = mock.Create<IRepositoryFactory>();
                SetupFactoryForInvoices(repositoryFactory, mock);
                SetupFactoryForEmployees(repositoryFactory, mock);
                var empl = GetEmployeeSample(repositoryFactory)[0];

                var expected = GetInvoiceSample(repositoryFactory);

                var invoiceRep = mock.Create<IInvoiceRepository>();
                var cls = mock.Create<DefaultStockman>(new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(1, empl));

                var actual = new List<Invoice>(cls.GetInvoices());

                mock.Mock<IInvoiceRepository>().Verify(x => x.GetInvoices(), Moq.Times.Once);
                Assert.NotNull(actual);
                Assert.Equal(expected.Count, actual.Count);
            }
        }
    }
}
