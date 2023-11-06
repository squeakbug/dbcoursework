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
    public class TestManufacturerRepository
    {
        [Fact]
        public void TestManufacturerCRUD()
        {
            var factory = new SqlRepositoryFactory
            (
                dataSource: "127.0.0.1,1433",
                initialCatalog: "pharmacy",
                user: "SA",
                passwd: "P@ssword"
            );

            SetupManufacturerRepository(factory);

            Manufacturer tmpManufacturer = null;
            using (var rep = factory.CreateManufacturerRep())
            {
                tmpManufacturer = rep.GetManufacturerById(1);
                Assert.NotNull(tmpManufacturer);
                Assert.Equal("MN1", tmpManufacturer.M_name);

                var manufacturers = new List<Manufacturer>(rep.GetManufacturers());
                Assert.Equal(3, manufacturers.Count);
                tmpManufacturer = rep.GetManufacturerById(1);
            }

            using (var rep = factory.CreateManufacturerRep())
            {
                tmpManufacturer.M_name = "MN4";
                rep.Update(tmpManufacturer);

                tmpManufacturer = rep.GetManufacturerById(1);
                Assert.Equal("MN4", tmpManufacturer.M_name);

                rep.Delete(tmpManufacturer.Id);
                var manufacturers = new List<Manufacturer>(rep.GetManufacturers());
                Assert.Equal(2, manufacturers.Count);
            }

            ReleaseManufacturerRepository(factory);
        }

        public static void SetupManufacturerRepository(SqlRepositoryFactory factory)
        {
            List<Manufacturer> manufacturerSample = GetManufacturerSample();
            using (var rep = factory.CreateManufacturerRep())
            {
                rep.Create(manufacturerSample[0]);
                rep.Truncate();
            }

            using (var rep = factory.CreateManufacturerRep())
            {
                rep.Create(manufacturerSample[0]);
                rep.Create(manufacturerSample[1]);
                rep.Create(manufacturerSample[2]);
            }
        }

        public static void ReleaseManufacturerRepository(SqlRepositoryFactory factory)
        {
            using (var rep = factory.CreateManufacturerRep())
                rep.Truncate();
        }
        public static List<Manufacturer> GetManufacturerSample()
        {
            return new List<Manufacturer>
            {
                new Manufacturer { M_name = "MN1", Concern = "MC1", Country = "MC1" },
                new Manufacturer { M_name = "MN1", Concern = "MC1", Country = "MC1" },
                new Manufacturer { M_name = "MN1", Concern = "MC1", Country = "MC1" }
            };
        }
    }
}
