using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification.Entities;

namespace DefaultBusinessLogic
{
    public interface IProductManager : IEmployee
    {
        IEnumerable<Vendor> GetVendors();
        IEnumerable<Product> GetProducts();
        IEnumerable<MarkedProduct> GetSellingProducts();
        Manufacturer GetManufacturerById(int id);
        MarkedProduct GetMarkedProductById(int id);
        MarkInfo GetMarkInfoById(int id);
        Vendor GetVendorById(int id);
        Product GetProductById(int id);
        IEnumerable<MarkedProduct> GetMarkedProducts();
        void UpdateProduct(Product product);
        void UpdateMarkedProduct(MarkedProduct markedProduct);
        void UpdateVendor(Vendor vendor);
        void UpdateMarkup(int markedProductId, decimal newMarkup);
        void UpdateDefaultMarkup(int productId, decimal newMarkup);
        void UpdateMaxMarkup(int productId, decimal newMarkup);
        void UpdateMarkInfo(MarkInfo markInfo);
        void AddVendor(Vendor vendor);
        decimal GetAvgAnualIncome(int year);
        IEnumerable<MarkedProduct> GetStoredProducts();
        IEnumerable<MarkInfo> GetMarkInfos();
    }
}
