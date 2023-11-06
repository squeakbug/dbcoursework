using System;
using Xunit;
using System.Collections.Generic;
using System.Configuration;

using DataAccessLayer;
using DataAccessLayer.RepositoriesImp;
using BusinessSpecification;
using BusinessSpecification.Entities;

namespace TestDataAccessLayer
{
    public class TestCategoryRepository
    {
        [Fact]
        public void TestCategoryCRUD()
        {
            var factory = new SqlRepositoryFactory
            (
                dataSource : "127.0.0.1,1433",
                initialCatalog : "pharmacy",
                user : "SA",
                passwd : "P@ssword"
            );

            SetupCategoryRepository(factory);

            Category tmpCategory = null;
            using (var rep = factory.CreateCategoryRep())
            {
                tmpCategory = rep.GetCategoryById(1);
                Assert.NotNull(tmpCategory);
                Assert.Equal("Category_1", tmpCategory.Title);

                var categories = new List<Category>(rep.GetCategories());
                Assert.Equal(3, categories.Count);
                tmpCategory = rep.GetCategoryById(1);
            }

            // См. Примечание [15]
            using (var rep = factory.CreateCategoryRep())
            {
                tmpCategory.Title = "Category_4";
                rep.Update(tmpCategory);

                tmpCategory = rep.GetCategoryById(1);
                Assert.Equal("Category_4", tmpCategory.Title);

                rep.Delete(tmpCategory.Id);
                var categories = new List<Category>(rep.GetCategories());
                Assert.Equal(2, categories.Count);
            }

            ReleaseCategoryRepository(factory);
        }

        public static void SetupCategoryRepository(SqlRepositoryFactory factory)
        {
            List<Category> categorySample = GetCategorySample();
            using (var rep = factory.CreateCategoryRep())
            {
                rep.Create(categorySample[0]);
                rep.Truncate();
            }

            using (var rep = factory.CreateCategoryRep())
            {
                rep.Create(categorySample[0]);
                rep.Create(categorySample[1]);
                rep.Create(categorySample[2]);
            }
        }

        public static void ReleaseCategoryRepository(SqlRepositoryFactory factory)
        {
            using (var rep = factory.CreateCategoryRep())
                rep.Truncate();
        }

        public static List<Category> GetCategorySample()
        {
            return new List<Category>
            {
                new Category { C_description = "Category_1", Title = "Category_1" },
                new Category { C_description = "Category_2", Title = "Category_2" },
                new Category { C_description = "Category_3", Title = "Category_3" },
            };
        }
    }
}
