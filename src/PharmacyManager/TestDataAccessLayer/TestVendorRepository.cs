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
    public class TestVendorRepository
    {
        [Fact]
        public void TestVendorCRUD()
        {
            var factory = new SqlRepositoryFactory
            (
                dataSource: "127.0.0.1,1433",
                initialCatalog: "pharmacy",
                user: "SA",
                passwd: "P@ssword"
            );

            SetupVendorRepository(factory);

            Vendor tmpVendor = null;
            using (var rep = factory.CreateVendorRep())
            {
                tmpVendor = rep.GetVendorById(1);
                Assert.NotNull(tmpVendor);
                Assert.Equal("VSN1", tmpVendor.Short_name);

                var vendors = new List<Vendor>(rep.GetVendors());
                Assert.Equal(3, vendors.Count);
                tmpVendor = rep.GetVendorById(1);
            }

            using (var rep = factory.CreateVendorRep())
            {
                tmpVendor.Short_name = "VSN4";
                rep.Update(tmpVendor);

                tmpVendor = rep.GetVendorById(1);
                Assert.Equal("VSN4", tmpVendor.Short_name);

                rep.Delete(tmpVendor.Id);
                var vendors = new List<Vendor>(rep.GetVendors());
                Assert.Equal(2, vendors.Count);
            }

            ReleaseVendorRepository(factory);
        }

        public static void SetupVendorRepository(SqlRepositoryFactory factory)
        {
            List<Vendor> vendorSample = GetVendorSample();
            using (var rep = factory.CreateVendorRep())
            {
                rep.Create(vendorSample[0]);
                rep.Truncate();
            }

            using (var rep = factory.CreateVendorRep())
            {
                rep.Create(vendorSample[0]);
                rep.Create(vendorSample[1]);
                rep.Create(vendorSample[2]);
            }
        }

        public static void ReleaseVendorRepository(SqlRepositoryFactory factory)
        {
            using (var rep = factory.CreateVendorRep())
                rep.Truncate();
        }

        public static List<Vendor> GetVendorSample()
        {
            return new List<Vendor>
            {
                new Vendor { Full_name = "VFN1", Inn = 1000, Kpp = 1000, Phone = "VP1", Short_name = "VSN1" },
                new Vendor { Full_name = "VFN2", Inn = 1000, Kpp = 1000, Phone = "VP2", Short_name = "VSN2" },
                new Vendor { Full_name = "VFN3", Inn = 1000, Kpp = 1000, Phone = "VP3", Short_name = "VSN3" }
            };
        }
    }
}
