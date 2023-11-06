using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification;
using BusinessSpecification.Entities;
using BusinessSpecification.RepositoryInterfaces;
using DefaultBusinessLogic.Exceptions;

namespace DefaultBusinessLogic
{
    public class DefaultProductManager : BaseEmployee, IProductManager
    {
        public DefaultProductManager(IRepositoryFactory factory, Employee employee)
            : base(factory, employee)
        {

        }

        public IEnumerable<Vendor> GetVendors()
        {
            using (var vendorRep = RepositoryFactory.CreateVendorRep())
            {
                return vendorRep.GetVendors();
            }
        }

        public void UpdateVendor(Vendor vendor)
        {
            using (var vendorRep = RepositoryFactory.CreateVendorRep())
            {
                vendorRep.Update(vendor);
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var productRep = RepositoryFactory.CreateProductRep())
            {
                productRep.Update(product);
            }
        }

        public void UpdateMarkedProduct(MarkedProduct markedProduct)
        {
            using (var markedProductRep = RepositoryFactory.CreateMarkedProductRep())
            {
                markedProductRep.Update(markedProduct);
            }
        }

        public IEnumerable<MarkedProduct> GetSellingProducts()
        {
            using (var markedProductRep = RepositoryFactory.CreateMarkedProductRep())
            {
                return markedProductRep.GetSellingProducts();
            }
        }

        public void UpdateMarkup(int markedProductId, Decimal newMarkup)
        {
            if (newMarkup < 0)
                throw new ProductManagerException("New markup must be greater than zero") { ProductManager = this };

            MarkedProduct markedProduct = null;
            using (var markedProductRep = RepositoryFactory.CreateMarkedProductRep())
            {
                if (newMarkup > markedProductRep.GetProductByMarkedProductId(markedProductId).Maximum_markup)
                {
                    throw new ProductManagerException("New markup must be less than maximul markup") 
                        { ProductManager = this };
                }

                markedProduct = markedProductRep.GetMarkedProductById(markedProductId);
                if (markedProduct == null)
                    throw new ProductManagerException("No marked product with such id") { ProductManager = this };
            }

            using (var markInfoRep = RepositoryFactory.CreateMarkInfoRep())
            {
                var markInfo = markInfoRep.GetMarkInfoById(markedProduct.Mark_info_id);
                markInfo.markup = newMarkup;
                markInfoRep.Update(markInfo);
            }
        }

        public void UpdateDefaultMarkup(int productId, Decimal newMarkup)
        {
            if (newMarkup < 0)
            {
                throw new ProductManagerException("New markup must be greater than zero")
                    { ProductManager = this };
            }
            using (var productRep = RepositoryFactory.CreateProductRep())
            {
                Product product = productRep.GetProductById(productId);
                if (product == null)
                    throw new ProductManagerException("No product with such id") { ProductManager = this };
                if (product.Maximum_markup < newMarkup)
                {
                    throw new ProductManagerException("New markup must be less than maximum markup")
                        { ProductManager = this };
                }
                product.Default_markup = newMarkup;
                productRep.Update(product);
            }
        }

        public void UpdateMaxMarkup(int productId, decimal newMarkup)
        {
            if (newMarkup < 0)
            {
                throw new ProductManagerException("New max markup must be greater than zero")
                    { ProductManager = this };
            }
            using (var productRep = RepositoryFactory.CreateProductRep())
            {
                Product product = productRep.GetProductById(productId);
                if (product == null)
                    throw new ProductManagerException("No product with such id") { ProductManager = this };
                product.Maximum_markup = newMarkup;
                productRep.Update(product);
            }
        }

        public void AddVendor(Vendor vendor)
        {
            using (var vendorRep = RepositoryFactory.CreateVendorRep())
            {
                vendorRep.Create(vendor);
            }
        }

        public Vendor GetVendorById(int id)
        {
            using (var vendorRep = RepositoryFactory.CreateVendorRep())
            {
                return vendorRep.GetVendorById(id);
            }
        }

        public Product GetProductById(int id)
        {
            using (var productRep = RepositoryFactory.CreateProductRep())
            {
                return productRep.GetProductById(id);
            }
        }

        public Manufacturer GetManufacturerById(int id)
        {
            using (var manufacturerRep = RepositoryFactory.CreateManufacturerRep())
            {
                return manufacturerRep.GetManufacturerById(id);
            }
        }

        public MarkedProduct GetMarkedProductById(int id)
        {
            using (var markedProductRep = RepositoryFactory.CreateMarkedProductRep())
            {
                return markedProductRep.GetMarkedProductById(id);
            }
        }

        public MarkInfo GetMarkInfoById(int id)
        {
            using (var markInfoRep = RepositoryFactory.CreateMarkInfoRep())
            {
                return markInfoRep.GetMarkInfoById(id);
            }
        }

        public void UpdateMarkInfo(MarkInfo markInfo)
        {
            using (var markInfoRep = RepositoryFactory.CreateMarkInfoRep())
            {
                markInfoRep.Update(markInfo);
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            using (var productRep = RepositoryFactory.CreateProductRep())
            {
                return productRep.GetProducts();
            }
        }

        public IEnumerable<MarkedProduct> GetMarkedProducts()
        {
            using (var markedProductRep = RepositoryFactory.CreateMarkedProductRep())
            {
                return markedProductRep.GetMarkedProducts();
            }
        }

        public decimal GetAvgAnualIncome(int year)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MarkedProduct> GetStoredProducts()
        {
            using (var markedProductRep = RepositoryFactory.CreateMarkedProductRep())
            {
                return markedProductRep.GetSellingProducts();
            }
        }
        public IEnumerable<MarkInfo> GetMarkInfos()
        {
            using (var markInfoRep = RepositoryFactory.CreateMarkInfoRep())
            {
                return markInfoRep.GetMarkInfos();
            }
        }
    }
}
