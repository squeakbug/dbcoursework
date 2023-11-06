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
    public class TestMarkedProductRepository
    {
        [Fact]
        public void TestMarkedProductCRUD()
        {
            var factory = new SqlRepositoryFactory
            (
                dataSource: "127.0.0.1,1433",
                initialCatalog: "pharmacy",
                user: "SA",
                passwd: "P@ssword"
            );

            SetupMarkedProductRepository(factory);

            MarkedProduct tmpMarkedProduct = null;
            using (var rep = factory.CreateMarkedProductRep())
            {
                tmpMarkedProduct = rep.GetMarkedProductById(1);
                Assert.NotNull(tmpMarkedProduct);
                Assert.Equal("MISL1", tmpMarkedProduct.Mark_info.Storage_location);

                var markedProducts = new List<MarkedProduct>(rep.GetMarkedProducts());
                Assert.Equal(3, markedProducts.Count);
                tmpMarkedProduct = rep.GetMarkedProductById(1);
            }

            using (var rep = factory.CreateMarkedProductRep())
            {
                tmpMarkedProduct.Sold = 1;
                rep.Update(tmpMarkedProduct);

                tmpMarkedProduct = rep.GetMarkedProductById(1);
                Assert.Equal(1, tmpMarkedProduct.Sold);

                rep.Delete(tmpMarkedProduct.Id);
                var markedProducts = new List<MarkedProduct>(rep.GetMarkedProducts());
                Assert.Equal(2, markedProducts.Count);
            }

            ReleaseMarkedProductRepository(factory);
        }

        public static void SetupMarkedProductRepository(SqlRepositoryFactory factory)
        {
            TestMarkInfoRepository.SetupMarkInfoRepository(factory);

            List<MarkedProduct> markedProductSample = GetMarkedProductSample(factory);
            using (var rep = factory.CreateMarkedProductRep())
            {
                rep.Create(markedProductSample[0]);
                rep.Truncate();
            }

            using (var rep = factory.CreateMarkedProductRep())
            {
                rep.Create(markedProductSample[0]);
                rep.Create(markedProductSample[1]);
                rep.Create(markedProductSample[2]);
            }
        }

        public static void ReleaseMarkedProductRepository(SqlRepositoryFactory factory)
        {
            using (var rep = factory.CreateMarkedProductRep())
                rep.Truncate();

            TestMarkInfoRepository.ReleaseMarkInfoRepository(factory);
        }

        public static List<MarkedProduct> GetMarkedProductSample(SqlRepositoryFactory factory)
        {
            List<MarkInfo> markInfos = null;
            using (var rep = factory.CreateMarkInfoRep())
                markInfos = new List<MarkInfo>(rep.GetMarkInfos());

            return new List<MarkedProduct>
            {
                new MarkedProduct { Sold = 0, Mark_info = markInfos[0] },
                new MarkedProduct { Sold = 0, Mark_info = markInfos[1] },
                new MarkedProduct { Sold = 0, Mark_info = markInfos[2] }
            };
        }
    }
}
