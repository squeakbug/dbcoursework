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
    class SqlReceiptRepository : IReceiptRepository
    {
        private Context _globalCtx;

        public SqlReceiptRepository(Context globalCtx)
        {
            _globalCtx = globalCtx;
        }

        public IEnumerable<Receipt> GetReceipts()
        {
            var converter = new ReceiptConverter();
            var result = new List<Receipt>();
            foreach (var item in _globalCtx.Receipt)
                result.Add(converter.MapToBusinessEntity(item, _globalCtx));
            return result;
        }

        public void AddReceipWithProducts(Receipt receipt, IEnumerable<(int, uint)> productList)
        {
            var converter = new ReceiptConverter();
            using (var transaction = _globalCtx.Database.BeginTransaction())
            {
                var receiptDAL = converter.MapFromBusinessEntity(receipt);
                _globalCtx.Receipt.Add(receiptDAL);
                _globalCtx.SaveChanges();
                int receiptId = receiptDAL.Id;
                foreach (var item in productList)
                {
                    _globalCtx.Database.ExecuteSqlInterpolated(
                        $"EXEC dbo.sellProduct {item.Item1}, {item.Item2}, {receiptId}");
                }
                transaction.Commit();
            }
            _globalCtx.SaveChanges();
        }

        public Receipt GetReceiptById(int id)
        {
            var res = _globalCtx.Receipt.Find(id);
            var converter = new ReceiptConverter();
            return res == null ? null : converter.MapToBusinessEntity(res, _globalCtx);
        }

        public void Create(Receipt receipt)
        {
            var converter = new ReceiptConverter();
            _globalCtx.Receipt.Add(converter.MapFromBusinessEntity(receipt));
            _globalCtx.SaveChanges();
        }

        public void Update(Receipt receipt)
        {
            var converter = new ReceiptConverter();
            _globalCtx.Receipt.Update(converter.MapFromBusinessEntity(receipt));
            _globalCtx.SaveChanges();
        }

        public void Delete(int id)
        {
            var receipt = _globalCtx.Receipt.Find(id);
            if (receipt == null)
                throw new ReceiptException("No receipt with such id");

            _globalCtx.Receipt.Remove(receipt);
            _globalCtx.SaveChanges();
        }

        public void Truncate()
        {
            _globalCtx.Database.ExecuteSqlInterpolated($"DELETE dbo.receipt");
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
