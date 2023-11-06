using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

using DataAccessLayer.Converters;
using DataAccessLayer.Exceptions;
using BusinessSpecification.Entities;
using BusinessSpecification.RepositoryInterfaces;

namespace DataAccessLayer.RepositoriesImp
{
    class SqlWriteOffProductRepository : IWriteOffProductRepository
    {
        private Context _globalCtx;

        public SqlWriteOffProductRepository(Context globalCtx)
        {
            _globalCtx = globalCtx;
        }

        public IEnumerable<WriteOffProduct> GetWriteOffProducts()
        {
            var converter = new WriteOffProductConverter();
            var result = new List<WriteOffProduct>();
            foreach (var item in _globalCtx.Write_off_product)
                result.Add(converter.MapToBusinessEntity(item, _globalCtx));
            return result;
        }

        public WriteOffProduct GetWriteOffProductById(int id)
        {
            var writeOffProduct = _globalCtx.Write_off_product.Find(id);
            var converter = new WriteOffProductConverter();
            return  writeOffProduct == null ? null : converter.MapToBusinessEntity(writeOffProduct, _globalCtx);
        }

        public void Create(WriteOffProduct writeOffProduct)
        {
            var converter = new WriteOffProductConverter();
            _globalCtx.Write_off_product.Add(converter.MapFromBusinessEntity(writeOffProduct));
            _globalCtx.SaveChanges();
        }

        public void Update(WriteOffProduct writeOffProduct)
        {
            var converter = new WriteOffProductConverter();
            _globalCtx.Write_off_product.Update(converter.MapFromBusinessEntity(writeOffProduct));
            _globalCtx.SaveChanges();
        }

        public void Delete(int id)
        {
            var writeOffProduct = _globalCtx.Write_off_product.Find(id);
            if (writeOffProduct == null)
                throw new WriteOffProductException("No write-off product with such id");

            _globalCtx.Write_off_product.Remove(writeOffProduct);
            _globalCtx.SaveChanges();
        }

        public void Truncate()
        {
            _globalCtx.Database.ExecuteSqlInterpolated($"DELETE dbo.write_off_product");
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
