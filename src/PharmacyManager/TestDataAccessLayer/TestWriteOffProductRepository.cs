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
    public class TestWriteOffProductRepository
    {
        [Fact]
        public void TestWriteOffProductCRUD()
        {
            var factory = new SqlRepositoryFactory
            (
                dataSource: "127.0.0.1,1433",
                initialCatalog: "pharmacy",
                user: "SA",
                passwd: "P@ssword"
            );

            SetupWriteOffProductRepository(factory);

            WriteOffProduct tmpWriteOffProduct = null;
            using (var rep = factory.CreateWriteOffProductRep())
            {
                tmpWriteOffProduct = rep.GetWriteOffProductById(1);
                Assert.NotNull(tmpWriteOffProduct);
                Assert.Equal("WOPR1", tmpWriteOffProduct.Reason);

                var writeOffProducts = new List<WriteOffProduct>(rep.GetWriteOffProducts());
                Assert.Equal(3, writeOffProducts.Count);
                tmpWriteOffProduct = rep.GetWriteOffProductById(1);
            }

            using (var rep = factory.CreateWriteOffProductRep())
            {
                tmpWriteOffProduct.Reason = "WOPR4";
                rep.Update(tmpWriteOffProduct);

                tmpWriteOffProduct = rep.GetWriteOffProductById(1);
                Assert.Equal("WOPR4", tmpWriteOffProduct.Reason);

                rep.Delete(tmpWriteOffProduct.Id);
                var writeOffProducts = new List<WriteOffProduct>(rep.GetWriteOffProducts());
                Assert.Equal(2, writeOffProducts.Count);
            }

            ReleaseWriteOffProductRepository(factory);
        }

        public static void SetupWriteOffProductRepository(SqlRepositoryFactory factory)
        {
            TestMarkInfoRepository.SetupMarkInfoRepository(factory);
            TestEmployeeRepository.SetupEmployeeRepository(factory);

            List<WriteOffProduct> writeOffProductSample = GetWriteOffProductSample(factory);
            using (var rep = factory.CreateWriteOffProductRep())
            {
                rep.Create(writeOffProductSample[0]);
                rep.Truncate();
            }

            using (var rep = factory.CreateWriteOffProductRep())
            {
                rep.Create(writeOffProductSample[0]);
                rep.Create(writeOffProductSample[1]);
                rep.Create(writeOffProductSample[2]);
            }
        }

        public static void ReleaseWriteOffProductRepository(SqlRepositoryFactory factory)
        {
            using (var rep = factory.CreateWriteOffProductRep())
                rep.Truncate();

            TestMarkInfoRepository.ReleaseMarkInfoRepository(factory);
            TestEmployeeRepository.ReleaseEmployeeRepository(factory);
        }

        public static List<WriteOffProduct> GetWriteOffProductSample(SqlRepositoryFactory factory)
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
                    Mark_info = markInfos[0], Reason = "WOPR1", Write_off_date = DateTime.Now,
                    Write_off_employee = employees[0]
                },
                new WriteOffProduct
                {
                    Mark_info = markInfos[1], Reason = "WOPR2", Write_off_date = DateTime.Now,
                    Write_off_employee = employees[1]
                },
                new WriteOffProduct
                {
                    Mark_info = markInfos[2], Reason = "WOPR3", Write_off_date = DateTime.Now,
                    Write_off_employee = employees[2]
                }
            };
        }
    }
}
