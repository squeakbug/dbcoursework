using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

using DataAccessLayer.Converters;
using DataAccessLayer.Exceptions;
using BusinessSpecification.Entities;
using BusinessSpecification.RepositoryInterfaces;

namespace DataAccessLayer.RepositoriesImp
{
    public class SqlCategoryRepository : ICategoryRepository
    {
        private Context _globalCtx;

        public SqlCategoryRepository(Context globalCtx)
        {
            _globalCtx = globalCtx;
        }

        public IEnumerable<Category> GetCategories()
        {
            var result = new List<Category>();
            var converter = new CategoryConverter();
            foreach (var category in _globalCtx.Category)
                result.Add(converter.MapToBusinessEntity(category));
            return result;
        }

        public IEnumerable<int> GetCategoriesByProduct(int productId)
        {
            var result = new List<int>();
            foreach (var category in _globalCtx.Category)
            {
                if (_globalCtx.Database.ExecuteSqlRaw("SELECT * FROM has_category WHERE product_id = {0}", productId) != 0)
                    result.Add(category.Id);
            }
            return result;
        }

        public Category GetCategoryById(int id)
        {
            var converter = new CategoryConverter();
            var category = _globalCtx.Category.Find(id);
            return category == null ? null : converter.MapToBusinessEntity(category);
        }

        public void Create(Category category)
        {
            var converter = new CategoryConverter();
            _globalCtx.Category.Add(converter.MapFromBusinessEntity(category));
            _globalCtx.SaveChanges();
        }

        public void AddCategoryToProduct(int categoryId, int productId)
        {
            var categoryBound = new Entities.HasCategory
            {
                category_id = categoryId,
                product_id = productId
            };
            _globalCtx.Add(categoryBound);
            _globalCtx.SaveChanges();
        }

        public void Update(Category category)
        {
            var converter = new CategoryConverter();
            _globalCtx.Category.Update(converter.MapFromBusinessEntity(category));
            _globalCtx.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = _globalCtx.Category.Find(id);
            if (category == null)
                throw new CategoryException("No category with such id");

            _globalCtx.Category.Remove(category);
            _globalCtx.SaveChanges();
        }

        public void Truncate()
        {
            _globalCtx.Database.ExecuteSqlInterpolated($"DELETE dbo.category");
            _globalCtx.SaveChanges();
        }

        private bool disposed = false;

        private void CleanUp(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _globalCtx.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            CleanUp(true);
            GC.SuppressFinalize(this);
        }
    }
}
