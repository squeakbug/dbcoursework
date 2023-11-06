using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification.Entities;

namespace DefaultBusinessLogic
{
    public interface IPharmacist : IEmployee
    {
        void InitOperation();
        void AddProductToOperation(int productid, uint cnt);
        void DeleteProductFromOperation(int productId);
        void SetDiscount(Decimal discount);
        void ConfirmOperation(Decimal cardSize, Decimal cashSize, string paymentType);
        void CancelOperation();
        void ConfirmProductRequest(ProductRequest request, PersonMetadata personMetadata);
        IEnumerable<Product> GetProducts();
        IEnumerable<MarkedProduct> GetStoredProducts();
        Manufacturer GetManufacturerById(int id);
        IEnumerable<MarkInfo> GetMarkInfos();
        MarkedProduct GetMarkedProductById(int id);
        MarkInfo GetMarkInfoById(int id);
        IncomeProduct GetIncomeProductById(int id);
        Product GetProductById(int id);
    }
}
