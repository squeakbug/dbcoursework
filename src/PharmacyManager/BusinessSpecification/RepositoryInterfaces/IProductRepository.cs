using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using BusinessSpecification.Entities;

namespace BusinessSpecification.RepositoryInterfaces
{
    public interface IProductRepository : IDisposable
    {
        IEnumerable<Product> GetProducts();
        Product GetProductById(int id);
        IEnumerable<Product> GetProductsByName(string name);
        public Product GetProductByGTIN(string gtin);
        void Create(Product product);
        void Update(Product product);
        void Delete(int id);
        void Truncate();
    }
}
