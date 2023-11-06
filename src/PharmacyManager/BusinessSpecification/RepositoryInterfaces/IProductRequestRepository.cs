using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification.Entities;

namespace BusinessSpecification.RepositoryInterfaces
{
    public interface IProductRequestRepository : IDisposable
    {
        IEnumerable<ProductRequest> GetProductRequests();
        ProductRequest GetProductRequestById(int id);
        void CreateRequestWithPerson(PersonMetadata person, ProductRequest request);
        void Create(ProductRequest productRequest);
        void Update(ProductRequest productRequest);
        void Delete(int id);
        void Approve(int productRequestId, int employeeId);
        void Truncate();
    }
}
