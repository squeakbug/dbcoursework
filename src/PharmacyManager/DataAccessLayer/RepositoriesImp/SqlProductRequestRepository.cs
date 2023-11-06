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
    class SqlProductRequestRepository : IProductRequestRepository
    {
        private Context _globalCtx;

        public SqlProductRequestRepository(Context globalCtx)
        {
            _globalCtx = globalCtx;
        }

        public IEnumerable<ProductRequest> GetProductRequests()
        {
            var converter = new ProductRequestConverter();
            var result = new List<ProductRequest>();
            foreach (var item in _globalCtx.Product_request)
                result.Add(converter.MapToBusinessEntity(item, _globalCtx));
            return result;
        }

        public ProductRequest GetProductRequestById(int id)
        {
            var converter = new ProductRequestConverter();
            var productRequest = _globalCtx.Product_request.Find(id);
            return productRequest == null ? null : converter.MapToBusinessEntity(productRequest, _globalCtx);
        }

        public void Create(ProductRequest productRequest)
        {
            var converter = new ProductRequestConverter();
            _globalCtx.Product_request.Add(converter.MapFromBusinessEntity(productRequest));
            _globalCtx.SaveChanges();
        }

        public void Update(ProductRequest productRequest)
        {
            var converter = new ProductRequestConverter();
            _globalCtx.Product_request.Update(converter.MapFromBusinessEntity(productRequest));
            _globalCtx.SaveChanges();
        }

        public void Delete(int id)
        {
            var productRequest = _globalCtx.Product_request.Find(id);
            if (productRequest == null)
                throw new ProductRequestException("No product request with such id");

            _globalCtx.Product_request.Remove(productRequest);
            _globalCtx.SaveChanges();
        }

        public void Approve(int productRequestId, int employeeId)
        {
            _globalCtx.Database.ExecuteSqlInterpolated(
                $"EXEC dbo.approveProductRequest {productRequestId}, {employeeId}");
        }

        public void CreateRequestWithPerson(PersonMetadata person, ProductRequest request)
        {
            var prodReqConverter = new ProductRequestConverter();
            var metadataConverter = new PersonMetadataConverter();
            Entities.PersonMetadata metadata = metadataConverter.MapFromBusinessEntity(person);
            _globalCtx.Person_metadata.Add(metadata);
            _globalCtx.SaveChanges();

            request.Customer_metadata_id = metadata.Id;
            _globalCtx.Product_request.Add(prodReqConverter.MapFromBusinessEntity(request));
            _globalCtx.SaveChanges();
        }

        public void Truncate()
        {
            _globalCtx.Database.ExecuteSqlInterpolated($"DELETE dbo.product_request");
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
