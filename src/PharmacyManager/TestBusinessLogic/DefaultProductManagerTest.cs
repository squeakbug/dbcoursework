using System;
using Xunit;
using Autofac;
using Autofac.Extras.Moq;
using System.Collections.Generic;
using System.Linq;

using DefaultBusinessLogic;
using DefaultBusinessLogic.Exceptions;
using BusinessSpecification;
using BusinessSpecification.Entities;
using BusinessSpecification.RepositoryInterfaces;

namespace TestBusinessLogic
{
    using static DataSampler;

    public class DefaultProductManagerTest
    {
        [Fact]
        public void DefaultGetSellingProducts()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepositoryFactory>();
                var repositoryFactory = mock.Create<IRepositoryFactory>();
                SetupFactoryForMarkedProducts(repositoryFactory, mock);
                SetupFactoryForEmployees(repositoryFactory, mock);
                var empl = GetEmployeeSample(repositoryFactory)[0];

                var pm = mock.Create<DefaultProductManager>(
                    new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(1, empl));

                mock.Mock<IMarkedProductRepository>()
                    .Setup(x => x.GetSellingProducts())
                    .Returns(GetMarkedProductSample(repositoryFactory).Where(x => x.Sold == 0));

                var result = pm.GetSellingProducts();

                Assert.Equal(3, new List<MarkedProduct>(result).Count);
            }
        }

        [Fact]
        public void MarkupGreatThanMaxUpdateDefaultMarkup()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepositoryFactory>();
                var repositoryFactory = mock.Create<IRepositoryFactory>();
                SetupFactoryForMarkedProducts(repositoryFactory, mock);
                SetupFactoryForEmployees(repositoryFactory, mock);
                var empl = GetEmployeeSample(repositoryFactory)[0];

                var pm = mock.Create<DefaultProductManager>(
                    new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(1, empl));

                var product = GetProductSample(repositoryFactory)[0];
                product.Maximum_markup = 5;

                mock.Mock<IProductRepository>()
                    .Setup(x => x.GetProductById(1))
                    .Returns(product);

                Assert.Throws<ProductManagerException>(() => pm.UpdateDefaultMarkup(1, 10));
            }
        }

        [Fact]
        public void NoMarkedProductUpdateDefaultMarkup()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepositoryFactory>();
                var repositoryFactory = mock.Create<IRepositoryFactory>();
                SetupFactoryForMarkedProducts(repositoryFactory, mock);
                SetupFactoryForProducts(repositoryFactory, mock);
                SetupFactoryForEmployees(repositoryFactory, mock);
                var empl = GetEmployeeSample(repositoryFactory)[0];

                var pm = mock.Create<DefaultProductManager>(
                    new PositionalParameter(0, repositoryFactory),
                    new PositionalParameter(1, empl));

                var product = GetProductSample(repositoryFactory)[0];
                var markedProduct = GetMarkedProductSample(repositoryFactory)[0];

                mock.Mock<IMarkedProductRepository>()
                    .Setup(x => x.GetMarkedProductById(1))
                    .Returns((MarkedProduct)null);
                mock.Mock<IMarkedProductRepository>()
                    .Setup(x => x.GetProductByMarkedProductId(1))
                    .Returns(product);

                Assert.Throws<ProductManagerException>(() => pm.UpdateMarkup(1, 10));
            }
        }
    }
}
