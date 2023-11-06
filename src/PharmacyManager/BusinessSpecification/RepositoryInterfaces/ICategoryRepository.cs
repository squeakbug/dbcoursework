using System;
using System.Collections.Generic;

using BusinessSpecification.Entities;

namespace BusinessSpecification.RepositoryInterfaces
{
    public interface ICategoryRepository : IDisposable
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<int> GetCategoriesByProduct(int productId);
        Category GetCategoryById(int id);
        void Create(Category category);
        void AddCategoryToProduct(int categoryId, int productId);
        void Update(Category category);
        void Delete(int id);
        void Truncate();
    }
}
