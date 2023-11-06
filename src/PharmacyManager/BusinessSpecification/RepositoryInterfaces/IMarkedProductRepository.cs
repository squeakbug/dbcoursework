using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification.Entities;

namespace BusinessSpecification.RepositoryInterfaces
{
    public interface IMarkedProductRepository : IDisposable
    {
        IEnumerable<MarkedProduct> GetMarkedProducts();
        MarkedProduct GetMarkedProductById(int id);
        IEnumerable<MarkedProduct> GetSellingProducts();
        IEnumerable<MarkedProduct> GetSoldProducts();
        IEnumerable<MarkedProduct> GetOverdueProducts();
        Product GetProductByMarkedProductId(int id);
        Decimal GetMarkedProductTotalPrice(IEnumerable<(int, uint)> productList);
        void SellProduct(int markedProductId, int cnt, int receiptId);
        void WriteOffProduct(int markedProductId , int cnt, int employeeId, string reason);
        void Create(MarkedProduct markedProduct);
        void Update(MarkedProduct markedProduct);
        void Delete(int id);
        void Truncate();
    }
}
