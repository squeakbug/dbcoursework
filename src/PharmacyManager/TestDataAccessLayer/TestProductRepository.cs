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
    public class TestProductRepository
    {
        [Fact]
        public void TestProductCRUD()
        {
            var factory = new SqlRepositoryFactory
            (
                dataSource: "127.0.0.1,1433",
                initialCatalog: "pharmacy",
                user: "SA",
                passwd: "P@ssword"
            );

            SetupProductRepository(factory);

            Product tmpProduct = null;
            using (var rep = factory.CreateProductRep())
            {
                tmpProduct = rep.GetProductById(1);
                Assert.NotNull(tmpProduct);
                Assert.Equal("PN1", tmpProduct.P_name);

                var products = new List<Product>(rep.GetProducts());
                Assert.Equal(3, products.Count);
                tmpProduct = rep.GetProductById(1);
            }

            using (var rep = factory.CreateProductRep())
            {
                tmpProduct.P_name = "PN4";
                rep.Update(tmpProduct);

                tmpProduct = rep.GetProductById(1);
                Assert.Equal("PN4", tmpProduct.P_name);

                rep.Delete(tmpProduct.Id);
                var products = new List<Product>(rep.GetProducts());
                Assert.Equal(2, products.Count);
            }

            ReleaseProductRepository(factory);
        }

        public static void SetupProductRepository(SqlRepositoryFactory factory)
        {
            TestManufacturerRepository.SetupManufacturerRepository(factory);
            TestCategoryRepository.SetupCategoryRepository(factory);

            List<Product> productSample = GetProductSample(factory);
            using (var rep = factory.CreateProductRep())
            {
                rep.Create(productSample[0]);
                rep.Truncate();
            }

            using (var rep = factory.CreateProductRep())
            {
                rep.Create(productSample[0]);
                rep.Create(productSample[1]);
                rep.Create(productSample[2]);
            }
        }

        public static void ReleaseProductRepository(SqlRepositoryFactory factory)
        {
            using (var rep = factory.CreateProductRep())
                rep.Truncate();

            TestManufacturerRepository.ReleaseManufacturerRepository(factory);
            TestCategoryRepository.ReleaseCategoryRepository(factory);
        }

        public static List<Product> GetProductSample(SqlRepositoryFactory factory)
        {
            List<Category> categories = null;
            List<Manufacturer> manufacturers = null;
            using (var rep = factory.CreateCategoryRep())
                categories = new List<Category>(rep.GetCategories());
            using (var rep = factory.CreateManufacturerRep())
                manufacturers = new List<Manufacturer>(rep.GetManufacturers());

            return new List<Product>
            {
                new Product
                {
                    Categories = categories, Articul = "1000", Count_in_package = 100,
                    Default_markup = 18, Dosage = 20, Dosage_form = "tablets", Gtin = "1000",
                    Instruction = null, International_name = "PN1", Leave_condition = "PLC1",
                    Manufacturer = manufacturers[0], Maximum_markup = 20, Maximum_shelf_life = 60,
                    Photo = null, Primacy_packaging = "PPP1", P_description = null, P_name = "PN1",
                    Storage_temperature = 25, Threashold_count = 0, Trademark = "PT1"
                },
                new Product
                {
                    Categories = categories, Articul = "1000", Count_in_package = 100,
                    Default_markup = 18, Dosage = 20, Dosage_form = "tablets", Gtin = "1000",
                    Instruction = null, International_name = "PN2", Leave_condition = "PLC2",
                    Manufacturer = manufacturers[1], Maximum_markup = 20, Maximum_shelf_life = 60,
                    Photo = null, Primacy_packaging = "PPP2", P_description = null, P_name = "PN2",
                    Storage_temperature = 25, Threashold_count = 0, Trademark = "PT2"
                },
                new Product
                {
                    Categories = categories, Articul = "1000", Count_in_package = 100,
                    Default_markup = 18, Dosage = 20, Dosage_form = "tablets", Gtin = "1000",
                    Instruction = null, International_name = "PN3", Leave_condition = "PLC3",
                    Manufacturer = manufacturers[2], Maximum_markup = 20, Maximum_shelf_life = 60,
                    Photo = null, Primacy_packaging = "PPP3", P_description = null, P_name = "PN3",
                    Storage_temperature = 25, Threashold_count = 0, Trademark = "PT3"
                }
            };
        }
    }
}
