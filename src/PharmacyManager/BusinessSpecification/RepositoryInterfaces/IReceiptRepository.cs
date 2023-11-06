using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification.Entities;

namespace BusinessSpecification.RepositoryInterfaces
{
    public interface IReceiptRepository : IDisposable
    {
        IEnumerable<Receipt> GetReceipts();
        Receipt GetReceiptById(int id);
        void AddReceipWithProducts(Receipt receipt, IEnumerable<(int, uint)> markedProducts);
        void Create(Receipt receipt);
        void Update(Receipt receipt);
        void Delete(int id);
        void Truncate();
    }
}
