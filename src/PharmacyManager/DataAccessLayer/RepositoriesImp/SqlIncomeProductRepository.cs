using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using DataAccessLayer.Converters;
using DataAccessLayer.Exceptions;
using BusinessSpecification.Entities;
using BusinessSpecification.RepositoryInterfaces;
using BusinessSpecification.Filters;

namespace DataAccessLayer.RepositoriesImp
{
    public class SqlIncomeProductRepository : IIncomeProductRepository
    {
        private Context _globalCtx;

        public SqlIncomeProductRepository(Context globalCtx)
        {
            _globalCtx = globalCtx;
        }

        public IEnumerable<IncomeProduct> GetIncomeProducts(IncomeProductFilter filter)
        {
            var converter = new IncomeProductConverter();
            var result = new List<IncomeProduct>();
            foreach (var item in _globalCtx.Income_product)
                result.Add(converter.MapToBusinessEntity(item, _globalCtx));
            return result;
        }

        public IncomeProduct GetIncomeProductById(int id)
        {
            var converter = new IncomeProductConverter();
            var incomeProduct = _globalCtx.Income_product.Find(id);
            return incomeProduct == null ? null : converter.MapToBusinessEntity(incomeProduct, _globalCtx);
        }

        public IEnumerable<IncomeProduct> GetIncomeProductByInvoiceId(int invoiceId)
        {
            var converter = new IncomeProductConverter();
            var queryRes = (from incomeProduct in _globalCtx.Income_product
                            where incomeProduct.Invoice_id == invoiceId
                            select incomeProduct);
            var result = new List<IncomeProduct>();
            foreach (var incomeProduct in queryRes)
            {
                result.Add(converter.MapToBusinessEntity(incomeProduct, _globalCtx));
            }
            return result;
        }

        public void Create(IncomeProduct incomeProduct)
        {
            var converter = new IncomeProductConverter();
            _globalCtx.Income_product.Add(converter.MapFromBusinessEntity(incomeProduct));
            _globalCtx.SaveChanges();
        }

        public void Update(IncomeProduct incomeProduct)
        {
            var converter = new IncomeProductConverter();
            _globalCtx.Income_product.Update(converter.MapFromBusinessEntity(incomeProduct));
            _globalCtx.SaveChanges();
        }

        public void Delete(int id)
        {
            var incomeProduct = _globalCtx.Income_product.Find(id);
            if (incomeProduct == null)
                throw new IncomeProductException("No income product with such id");

            _globalCtx.Income_product.Remove(incomeProduct);
            _globalCtx.SaveChanges();
        }

        public void Truncate()
        {
            _globalCtx.Database.ExecuteSqlInterpolated($"DELETE dbo.income_product");
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
