using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using DataAccessLayer.Converters;
using DataAccessLayer.Exceptions;
using BusinessSpecification.Entities;
using BusinessSpecification.RepositoryInterfaces;

namespace DataAccessLayer.RepositoriesImp
{
    class SqlMarkedProductRepository : IMarkedProductRepository
    {
        private Context _globalCtx;

        public SqlMarkedProductRepository(Context globalCtx)
        {
            _globalCtx = globalCtx;
        }

        public IEnumerable<MarkedProduct> GetMarkedProducts()
        {
            var converter = new MarkedProductConverter();
            var result = new List<MarkedProduct>();
            foreach (var item in _globalCtx.Marked_Product)
                result.Add(converter.MapToBusinessEntity(item, _globalCtx));
            return result;
        }

        public IEnumerable<MarkedProduct> GetSellingProducts()
        {
            var converter = new MarkedProductConverter();
            var query = from product in _globalCtx.Marked_Product
                        where product.Sold == 0
                        select product;
            var result = new List<MarkedProduct>();
            foreach (var markedProduct in query)
                result.Add(converter.MapToBusinessEntity(markedProduct, _globalCtx));
            return result;
        }

        public IEnumerable<MarkedProduct> GetSoldProducts()
        {
            var converter = new MarkedProductConverter();
            var query = from product in _globalCtx.Marked_Product
                        where product.Sold == 1
                        select product;
            var result = new List<MarkedProduct>();
            foreach (var markedProduct in query)
                result.Add(converter.MapToBusinessEntity(markedProduct, _globalCtx));
            return result;
        }

        public Product GetProductByMarkedProductId(int id)
        {
            var converter = new ProductConverter();
            var query = from mp in _globalCtx.Marked_Product
                        join mi in _globalCtx.Mark_info
                            on mp.Mark_info_id equals mi.Id
                        join ip in _globalCtx.Income_product
                            on mi.Income_product_id equals ip.Id
                        join p in _globalCtx.Product
                            on ip.Product_id equals p.Id
                        select new { p.Id };
            var product = _globalCtx.Product.Find(query.FirstOrDefault());
            return converter.MapToBusinessEntity(product, _globalCtx);
        }

        public decimal GetMarkedProductTotalPrice(IEnumerable<(int, uint)> productList)
        {
            decimal result = 0;
            IMarkInfoRepository markInfoRep = new SqlMarkInfoRepository(_globalCtx);
            foreach (var item in productList)
            {
                int markInfoId = _globalCtx.Marked_Product.Find(item.Item1).Mark_info_id;
                var markInfo = markInfoRep.GetMarkInfoById(markInfoId);
                var incomeProduct = _globalCtx.Income_product.Find(markInfo.Income_product_id);
                result += incomeProduct.Vendor_price * (1 + markInfo.markup) * item.Item2;
            }
            return result;
        }

        public IEnumerable<MarkedProduct> GetOverdueProducts()
        {
            throw new NotImplementedException();
        }

        public void SellProduct(int markedProductId, int cnt, int receiptId)
        {
            _globalCtx.Database.ExecuteSqlInterpolated(
                $"EXEC dbo.sellProduct {markedProductId}, {cnt}, {receiptId}");
            _globalCtx.SaveChanges();
        }

        public void WriteOffProduct(int markedProductId, int cnt, int employeeId, string reason)
        {
            _globalCtx.Database.ExecuteSqlInterpolated(
                $"EXEC dbo.writeOffProduct {markedProductId}, {cnt}, {employeeId}, {reason}");
            _globalCtx.SaveChanges();
        }

        public MarkedProduct GetMarkedProductById(int id)
        {
            var converter = new MarkedProductConverter();
            var markedProduct = _globalCtx.Marked_Product.Find(id);
            return markedProduct == null ? null : converter.MapToBusinessEntity(markedProduct, _globalCtx);
        }

        public void Create(MarkedProduct markedProduct)
        {
            var converter = new MarkedProductConverter();
            _globalCtx.Marked_Product.Add(converter.MapFromBusinessEntity(markedProduct));
            _globalCtx.SaveChanges();
        }

        public void Update(MarkedProduct markedProduct)
        {
            var converter = new MarkedProductConverter();
            _globalCtx.Marked_Product.Update(converter.MapFromBusinessEntity(markedProduct));
            _globalCtx.SaveChanges();
        }

        public void Delete(int id)
        {
            var markedProduct = _globalCtx.Marked_Product.Find(id);
            if (markedProduct == null)
                throw new MarkedProductException("No marked product with such id");

            _globalCtx.Marked_Product.Remove(markedProduct);
            _globalCtx.SaveChanges();
        }

        public void Truncate()
        {
            _globalCtx.Database.ExecuteSqlInterpolated($"DELETE dbo.marked_product");
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
