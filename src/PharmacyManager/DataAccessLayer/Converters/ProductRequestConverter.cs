using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using BusinessSpecification.Entities;
using DataAccessLayer;
using DataAccessLayer.RepositoriesImp;

namespace DataAccessLayer.Converters
{
    internal class ProductRequestConverter
    {
        public ProductRequest MapToBusinessEntity(Entities.ProductRequest productRequest, Context  globalCtx)
        {
            return new ProductRequest
            {
                Id = productRequest.Id,
                approved = productRequest.approved,
                Approved_date = productRequest.Approved_date,
                Approved_employee_id = productRequest.Approved_employee_id,
                Approximate_name = productRequest.Approximate_name,
                Customer_metadata_id = productRequest.Customer_metadata_id,
                Product_id = productRequest.Product_id,
                Pr_count = productRequest.Pr_count,
                Request_Date = productRequest.Request_Date,
                Request_employee_id = productRequest.Request_employee_id
            };
        }

        public Entities.ProductRequest MapFromBusinessEntity(ProductRequest productRequest)
        {
            return new Entities.ProductRequest
            {
                Id = productRequest.Id,
                approved = productRequest.approved,
                Approved_date = productRequest.Approved_date,
                Approved_employee_id = productRequest.Approved_employee_id,
                Approximate_name = productRequest.Approximate_name,
                Customer_metadata_id = productRequest.Customer_metadata_id,
                Product_id = productRequest.Product_id,
                Pr_count = productRequest.Pr_count,
                Request_Date = productRequest.Request_Date,
                Request_employee_id = productRequest.Request_employee_id
            };
        }
    }
}
