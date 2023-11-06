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
    public class TestIncomeProductRepository
    {
        [Fact]
        public void TestIncomeProductCRUD()
        {
            var factory = new SqlRepositoryFactory
            (
                dataSource : "127.0.0.1,1433",
                initialCatalog : "pharmacy",
                user : "SA",
                passwd : "P@ssword"
            );

            SetupIncomeProductRepository(factory);

            IncomeProduct tmpIncomeProduct = null;
            using (var rep = factory.CreateIncomeProductRep())
            {
                tmpIncomeProduct = rep.GetIncomeProductById(1);
                Assert.NotNull(tmpIncomeProduct);
                Assert.Equal("IPSL1", tmpIncomeProduct.Storage_location);

                var incomeProducts = new List<IncomeProduct>(rep.GetIncomeProducts());
                Assert.Equal(3, incomeProducts.Count);
                tmpIncomeProduct = rep.GetIncomeProductById(1);
            }

            using (var rep = factory.CreateIncomeProductRep())
            {
                tmpIncomeProduct.Storage_location = "IPSL4";
                rep.Update(tmpIncomeProduct);

                tmpIncomeProduct = rep.GetIncomeProductById(1);
                Assert.Equal("IPSL4", tmpIncomeProduct.Storage_location);

                rep.Delete(tmpIncomeProduct.Id);
                var incomeProducts = new List<IncomeProduct>(rep.GetIncomeProducts());
                Assert.Equal(2, incomeProducts.Count);
            }

            ReleaseIncomeProductRepository(factory);
        }

        public static void SetupIncomeProductRepository(SqlRepositoryFactory factory)
        {
            TestInvoiceRepository.SetupInvoiceRepository(factory);
            TestProductRepository.SetupProductRepository(factory);

            List<IncomeProduct> incomeProductSample = GetIncomeProductSample(factory);
            using (var rep = factory.CreateIncomeProductRep())
            {
                rep.Create(incomeProductSample[0]);
                rep.Truncate();
            }

            using (var rep = factory.CreateIncomeProductRep())
            {
                rep.Create(incomeProductSample[0]);
                rep.Create(incomeProductSample[1]);
                rep.Create(incomeProductSample[2]);
            }
        }

        public static void ReleaseIncomeProductRepository(SqlRepositoryFactory factory)
        {
            using (var rep = factory.CreateIncomeProductRep())
                rep.Truncate();

            TestProductRepository.ReleaseProductRepository(factory);
            TestInvoiceRepository.ReleaseInvoiceRepository(factory);
        }

        public static List<IncomeProduct> GetIncomeProductSample(SqlRepositoryFactory factory)
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
                    Measure_unit = "g", Invoice = invoices[0], Product = products[0],
                    Production_date = DateTime.Now, P_count = 100, Series = 100, Vendor_vax = 10,
                    Storage_location = "IPSL1", Vendor_price = 100
                },
                new IncomeProduct
                {
                    Measure_unit = "g", Invoice = invoices[1], Product = products[1],
                    Production_date = DateTime.Now, P_count = 100, Series = 100, Vendor_vax = 10,
                    Storage_location = "IPSL2", Vendor_price = 100
                },
                new IncomeProduct
                {
                    Measure_unit = "g", Invoice = invoices[2], Product = products[2],
                    Production_date = DateTime.Now, P_count = 100, Series = 100, Vendor_vax = 10,
                    Storage_location = "IPSL3", Vendor_price = 100
                }
            };
        }
    }
}
