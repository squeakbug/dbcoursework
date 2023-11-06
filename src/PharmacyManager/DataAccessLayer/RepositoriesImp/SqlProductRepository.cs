using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

using DataAccessLayer.Converters;
using DataAccessLayer.Exceptions;
using BusinessSpecification.Entities;
using BusinessSpecification.RepositoryInterfaces;

namespace DataAccessLayer.RepositoriesImp
{
    class SqlProductRepository : IProductRepository
    {
        private Context _globalCtx;

        public SqlProductRepository(Context globalCtx)
        {
            _globalCtx = globalCtx;
        }

        public IEnumerable<Product> GetProducts()
        {
            var converter = new ProductConverter();
            var result = new List<Product>();
            foreach (var item in _globalCtx.Product)
                result.Add(converter.MapToBusinessEntity(item, _globalCtx));
            return result;
        }

        public Product GetProductByGTIN(string gtin)
        {
            var converter = new ProductConverter();
            var result = (from product in _globalCtx.Product
                          where product.Gtin == gtin
                          select product).First();
            return converter.MapToBusinessEntity(result, _globalCtx);
        }

        public IEnumerable<Product> GetProductsByName(string name)
        {
            var converter = new ProductConverter();
            SqlParameter param = new SqlParameter("@namePart", name);
            var products = _globalCtx.Product.FromSqlRaw("SELECT * FROM dbo.getProductByName (@namePart)", param);
            var result = new List<Product>();
            foreach (var product in products)
                result.Add(converter.MapToBusinessEntity(product, _globalCtx));
            return result;
        }

        public Product GetProductById(int id)
        {
            var converter = new ProductConverter();
            var product = _globalCtx.Product.Find(id);
            return  product == null ? null : converter.MapToBusinessEntity(product, _globalCtx);
        }

        public void Create(Product product)
        {
            var converter = new ProductConverter();
            _globalCtx.Product.Add(converter.MapFromBusinessEntity(product));
            _globalCtx.SaveChanges();
        }

        public void Update(Product product)
        {
            var converter = new ProductConverter();
            _globalCtx.Product.Update(converter.MapFromBusinessEntity(product));
            _globalCtx.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _globalCtx.Product.Find(id);
            if (product == null)
                throw new ProductException("No product with such id");

            _globalCtx.Product.Remove(product);
            _globalCtx.SaveChanges();
        }

        public void Truncate()
        {
            _globalCtx.Database.ExecuteSqlInterpolated($"DELETE dbo.product");
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
