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
    public class TestMarkInfoRepository
    {
        [Fact]
        public void TestMarkInfoCRUD()
        {
            var factory = new SqlRepositoryFactory
            (
                dataSource: "127.0.0.1,1433",
                initialCatalog: "pharmacy",
                user: "SA",
                passwd: "P@ssword"
            );

            SetupMarkInfoRepository(factory);

            MarkInfo tmpMarkInfo = null;
            using (var rep = factory.CreateMarkInfoRep())
            {
                tmpMarkInfo = rep.GetMarkInfoById(1);
                Assert.NotNull(tmpMarkInfo);
                Assert.Equal("MISL1", tmpMarkInfo.Storage_location);

                var markInfos = new List<MarkInfo>(rep.GetMarkInfos());
                Assert.Equal(3, markInfos.Count);
                tmpMarkInfo = rep.GetMarkInfoById(1);
            }

            using (var rep = factory.CreateMarkInfoRep())
            {
                tmpMarkInfo.Storage_location = "MISL4";
                rep.Update(tmpMarkInfo);

                tmpMarkInfo = rep.GetMarkInfoById(1);
                Assert.Equal("MISL4", tmpMarkInfo.Storage_location);

                rep.Delete(tmpMarkInfo.Id);
                var markInfos = new List<MarkInfo>(rep.GetMarkInfos());
                Assert.Equal(2, markInfos.Count);
            }

            ReleaseMarkInfoRepository(factory);
        }

        public static void SetupMarkInfoRepository(SqlRepositoryFactory factory)
        {
            TestEmployeeRepository.SetupEmployeeRepository(factory);
            TestIncomeProductRepository.SetupIncomeProductRepository(factory);

            List<MarkInfo> markInfoSample = GetMarkInfoSample(factory);
            using (var rep = factory.CreateMarkInfoRep())
            {
                rep.Create(markInfoSample[0]);
                rep.Truncate();
            }

            using (var rep = factory.CreateMarkInfoRep())
            {
                rep.Create(markInfoSample[0]);
                rep.Create(markInfoSample[1]);
                rep.Create(markInfoSample[2]);
            }
        }

        public static void ReleaseMarkInfoRepository(SqlRepositoryFactory factory)
        {
            using (var rep = factory.CreateMarkInfoRep())
                rep.Truncate();

            TestIncomeProductRepository.ReleaseIncomeProductRepository(factory);
            TestEmployeeRepository.ReleaseEmployeeRepository(factory);
        }

        public static List<MarkInfo> GetMarkInfoSample(SqlRepositoryFactory factory)
        {
            List<Employee> employees = null;
            List<IncomeProduct> incomeProducts = null;
            using (var rep = factory.CreateEmployeeRep())
                employees = new List<Employee>(rep.GetEmployees());
            using (var rep = factory.CreateIncomeProductRep())
                incomeProducts = new List<IncomeProduct>(rep.GetIncomeProducts());

            return new List<MarkInfo>
            {
                new MarkInfo
                {
                    Storage_location = "MISL1", approved_date = DateTime.Now,
                    Approved_employee = employees[0], Income_product = incomeProducts[0],
                    markup = 16, P_count = 100
                },
                new MarkInfo
                {
                    Storage_location = "MISL2", approved_date = DateTime.Now,
                    Approved_employee = employees[1], Income_product = incomeProducts[1],
                    markup = 16, P_count = 100
                },
                new MarkInfo
                {
                    Storage_location = "MISL3", approved_date = DateTime.Now,
                    Approved_employee = employees[2], Income_product = incomeProducts[2],
                    markup = 16, P_count = 100
                }
            };
        }
    }
}
