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
    public class DefaultPharmacistTest
    {
        [Fact]
        public void TestDefaultConfirmOperation()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepositoryFactory>();
                var repositoryFactory = mock.Create<IRepositoryFactory>();
                SetupFactoryForMarkedProducts(repositoryFactory, mock);
                SetupFactoryForReceipts(repositoryFactory, mock);

                var operList = new List<(int, uint)> { (1, 1), (2, 1) };
                Employee empl = GetEmployeeSample(repositoryFactory)[1];

                var expected = new Receipt
                {
                    Discount = 0, Employee = empl, Cash_register_number = 10, Work_shift = 12
                };

                mock.Mock<IMarkedProductRepository>()
                    .Setup(x => x.GetMarkedProductTotalPrice(operList))
                    .Returns(300);
                mock.Mock<IReceiptRepository>()
                    .Setup(x => x.AddReceipWithProducts(expected, operList));

                var cls = mock.Create<DefaultPharmacist>(new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(1, empl), new PositionalParameter(2, 10), new PositionalParameter(3, 12));

                cls.InitOperation();
                cls.AddProductToOperation(operList[0].Item1, operList[0].Item2);
                cls.AddProductToOperation(operList[1].Item1, operList[1].Item2);
                cls.ConfirmOperation(300, 0, "card");

                mock.Mock<IReceiptRepository>().Verify(
                    x => x.AddReceipWithProducts(
                        Moq.It.Is<Receipt>(
                            s => s.Card_size == 300 && s.Cash_size == 0
                                && s.Discount == 0 && s.Payment_type == "card"
                                && s.Employee == cls.Employee),
                        Moq.It.Is<IEnumerable<(int, uint)>>(
                            x => new List<(int, uint)>(x).Count == 2)), Moq.Times.Once);
            }
        }

        [Fact]
        public void TestNoEnougnMoneyConfirmOperation()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepositoryFactory>();
                var repositoryFactory = mock.Create<IRepositoryFactory>();
                SetupFactoryForMarkedProducts(repositoryFactory, mock);
                SetupFactoryForReceipts(repositoryFactory, mock);

                var operList = new List<(int, uint)> { (1, 1), (2, 1) };
                Employee empl = GetEmployeeSample(repositoryFactory)[1];

                var expected = new Receipt
                {
                    Discount = 0, Employee = empl, Cash_register_number = 10, Work_shift = 12
                };

                mock.Mock<IMarkedProductRepository>()
                    .Setup(x => x.GetMarkedProductTotalPrice(operList))
                    .Returns(500);
                mock.Mock<IReceiptRepository>()
                    .Setup(x => x.AddReceipWithProducts(expected, operList));

                var cls = mock.Create<DefaultPharmacist>(new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(1, empl), new PositionalParameter(2, 10), new PositionalParameter(3, 12));

                cls.InitOperation();
                cls.AddProductToOperation(operList[0].Item1, operList[0].Item2);
                cls.AddProductToOperation(operList[1].Item1, operList[1].Item2);

                Assert.Throws<PharmacistException>(() => cls.ConfirmOperation(300, 0, "card"));
            }
        }

        [Fact]
        public void TestBadPaymentTypeConfirmOperation()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepositoryFactory>();
                var repositoryFactory = mock.Create<IRepositoryFactory>();
                SetupFactoryForMarkedProducts(repositoryFactory, mock);
                SetupFactoryForReceipts(repositoryFactory, mock);

                var operList = new List<(int, uint)> { (1, 1), (2, 1) };
                Employee empl = GetEmployeeSample(repositoryFactory)[1];

                var expected = new Receipt
                {
                    Discount = 0, Employee = empl, Cash_register_number = 10, Work_shift = 12
                };

                mock.Mock<IMarkedProductRepository>()
                    .Setup(x => x.GetMarkedProductTotalPrice(operList))
                    .Returns(500);
                mock.Mock<IReceiptRepository>()
                    .Setup(x => x.AddReceipWithProducts(expected, operList));

                var cls = mock.Create<DefaultPharmacist>(new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(1, empl), new PositionalParameter(2, 10), new PositionalParameter(3, 12));

                cls.InitOperation();
                cls.AddProductToOperation(operList[0].Item1, operList[0].Item2);
                cls.AddProductToOperation(operList[1].Item1, operList[1].Item2);

                Assert.Throws<PharmacistException>(() => cls.ConfirmOperation(300, 0, "fuel"));
            }
        }

        [Fact]
        public void ConfirmProductRequest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepositoryFactory>();
                var repositoryFactory = mock.Create<IRepositoryFactory>();
                SetupFactoryForMarkedProducts(repositoryFactory, mock);
                SetupFactoryForReceipts(repositoryFactory, mock);
                SetupFactoryForPersonMetadata(mock);
                SetupFactoryForProductRequests(repositoryFactory, mock);

                var consumerMetadata = GetPersonMetadataSample()[0];
                var productRequest = GetProductRequestSample(repositoryFactory)[0];
                var empl = GetEmployeeSample(repositoryFactory)[1];

                mock.Mock<IProductRequestRepository>()
                    .Setup(x => x.Create(productRequest));
                mock.Mock<IPersonMetadataRepository>()
                    .Setup(x => x.Create(consumerMetadata));

                var cls = mock.Create<DefaultPharmacist>(new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(1, empl), new PositionalParameter(2, 10), new PositionalParameter(3, 12));

                cls.InitRequest();
                cls.ConfirmProductRequest(consumerMetadata, productRequest.Pr_count);

                mock.Mock<IProductRequestRepository>().Verify(
                    x => x.Create(Moq.It.Is<ProductRequest>(
                        s => (s.Product_id == null && s.Request_employee.id == 2))), Moq.Times.Once);
                mock.Mock<IPersonMetadataRepository>().Verify(
                    x => x.Create(Moq.It.Is<PersonMetadata>(
                        s => (s.First_name == consumerMetadata.First_name))), Moq.Times.Once);
            }
        }
    }
}
