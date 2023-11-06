using System;
using Xunit;
using System.Collections.Generic;

using DataAccessLayer;
using DataAccessLayer.RepositoriesImp;
using BusinessSpecification;
using BusinessSpecification.RepositoryInterfaces;
using BusinessSpecification.Entities;

namespace TestDataAccessLayer
{
    public class TestProductRequestRepository
    {
        [Fact]
        public void TestProductRequestCRUD()
        {
            var factory = new SqlRepositoryFactory
            (
                dataSource: "127.0.0.1,1433",
                initialCatalog: "pharmacy",
                user: "SA",
                passwd: "P@ssword"
            );

            SetupProductRequestRepository(factory);

            ProductRequest tmpProductRequest = null;
            using (var rep = factory.CreateProductRequestRep())
            {
                tmpProductRequest = rep.GetProductRequestById(1);
                Assert.NotNull(tmpProductRequest);
                Assert.Equal("PRAN1", tmpProductRequest.Approximate_name);

                var productRequests = new List<ProductRequest>(rep.GetProductRequests());
                Assert.Equal(3, productRequests.Count);
                tmpProductRequest = rep.GetProductRequestById(1);
            }

            using (var rep = factory.CreateProductRequestRep())
            {
                tmpProductRequest.Approximate_name = "PRAN4";
                rep.Update(tmpProductRequest);

                tmpProductRequest = rep.GetProductRequestById(1);
                Assert.Equal("PRAN4", tmpProductRequest.Approximate_name);

                rep.Delete(tmpProductRequest.Id);
                var productRequests = new List<ProductRequest>(rep.GetProductRequests());
                Assert.Equal(2, productRequests.Count);
            }

            ReleaseProductRequestRepository(factory);
        }

        public static void SetupProductRequestRepository(SqlRepositoryFactory factory)
        {
            TestProductRepository.SetupProductRepository(factory);
            TestPersonMetadataRepository.SetupPersonMetadataRepository(factory);
            TestEmployeeRepository.SetupEmployeeRepository(factory);

            List<ProductRequest> productRequestSample = GetProductRequestSample(factory);
            using (var rep = factory.CreateProductRequestRep())
            {
                rep.Create(productRequestSample[0]);
                rep.Truncate();
            }

            using (var rep = factory.CreateProductRequestRep())
            {
                rep.Create(productRequestSample[0]);
                rep.Create(productRequestSample[1]);
                rep.Create(productRequestSample[2]);
            }
        }

        public static void ReleaseProductRequestRepository(SqlRepositoryFactory factory)
        {
            using (var rep = factory.CreateProductRequestRep())
                rep.Truncate();

            TestProductRepository.ReleaseProductRepository(factory);
            TestEmployeeRepository.ReleaseEmployeeRepository(factory);
            TestPersonMetadataRepository.ReleasePersonMetadataRepository(factory);
        }

        public static List<ProductRequest> GetProductRequestSample(SqlRepositoryFactory factory)
        {
            List<PersonMetadata> personMetadatas = null;
            List<Employee> employees = null;
            List<Product> products = null;
            using (var rep = factory.CreateProductRep())
                products = new List<Product>(rep.GetProducts());
            using (var rep = factory.CreatePersonMetadataRep())
                personMetadatas = new List<PersonMetadata>(rep.GetPersonMetadatas());
            using (var rep = factory.CreateEmployeeRep())
                employees = new List<Employee>(rep.GetEmployees());

            return new List<ProductRequest>
            {
                new ProductRequest
                {
                    approved = 0, Approved_date = DateTime.Now, Approved_employee = employees[0],
                    Approximate_name = "PRAN1", Customer_metadata = personMetadatas[0],
                    Product_id = products[0].Id, Pr_count = 1, Request_Date = DateTime.Now,
                    Request_employee = employees[1]
                },
                new ProductRequest
                {
                    approved = 0, Approved_date = DateTime.Now, Approved_employee = employees[0],
                    Approximate_name = "PRAN2", Customer_metadata = personMetadatas[1],
                    Product_id = products[1].Id, Pr_count = 1, Request_Date = DateTime.Now,
                    Request_employee = employees[1]
                },
                new ProductRequest
                {
                    approved = 0, Approved_date = DateTime.Now, Approved_employee = employees[0],
                    Approximate_name = "PRAN3", Customer_metadata = personMetadatas[2],
                    Product_id = products[2].Id, Pr_count = 1, Request_Date = DateTime.Now,
                    Request_employee = employees[2]
                }
            };
        }
    }
}
