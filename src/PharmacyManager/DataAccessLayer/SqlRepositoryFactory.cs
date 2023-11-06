using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using BusinessSpecification;
using BusinessSpecification.RepositoryInterfaces;
using DataAccessLayer.RepositoriesImp;

namespace DataAccessLayer
{
    public class SqlRepositoryFactory : IRepositoryFactory
    {
        public string DataSource { get; set; }
        public string InitialCatalog { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public string ConnectionString
        {
            get
            {
                var builder = new SqlConnectionStringBuilder
                {
                    DataSource = DataSource,
                    InitialCatalog = InitialCatalog,
                    UserID = User,
                    Password = Password,
                    //MultipleActiveResultSets = true
                };
                return builder.ToString();
            }
        }

        public SqlRepositoryFactory(string dataSource, string initialCatalog, string user, string passwd)
        {
            DataSource = dataSource;
            InitialCatalog = initialCatalog;
            User = user;
            Password = passwd;
        }

        public IEmployeeRepository CreateEmployeeRep()
        {
            var options = SqlServerDbContextOptionsExtensions
                .UseSqlServer(new DbContextOptionsBuilder(), ConnectionString)
                .UseLazyLoadingProxies()
                .Options;
            var globalCtx = new Context(options);
            return new SqlEmployeeRepository(globalCtx);
        }
        public ICategoryRepository CreateCategoryRep()
        {
            var options = SqlServerDbContextOptionsExtensions
                .UseSqlServer(new DbContextOptionsBuilder(), ConnectionString)
                .UseLazyLoadingProxies()
                .Options;
            var globalCtx = new Context(options);
            return new SqlCategoryRepository(globalCtx);
        }
        public IIncomeProductRepository CreateIncomeProductRep()
        {
            var options = SqlServerDbContextOptionsExtensions
                .UseSqlServer(new DbContextOptionsBuilder(), ConnectionString)
                .UseLazyLoadingProxies()
                .Options;
            var globalCtx = new Context(options);
            return new SqlIncomeProductRepository(globalCtx);
        }
        public IInvoiceRepository CreateInvoiceRep()
        {
            var options = SqlServerDbContextOptionsExtensions
                .UseSqlServer(new DbContextOptionsBuilder(), ConnectionString)
                .UseLazyLoadingProxies()
                .Options;
            var globalCtx = new Context(options);
            return new SqlInvoiceRepository(globalCtx);
        }
        public IManufacturerRepository CreateManufacturerRep()
        {
            var options = SqlServerDbContextOptionsExtensions
                .UseSqlServer(new DbContextOptionsBuilder(), ConnectionString)
                .UseLazyLoadingProxies()
                .Options;
            var globalCtx = new Context(options);
            return new SqlManufacturerRepository(globalCtx);
        }
        public IMarkedProductRepository CreateMarkedProductRep()
        {
            var options = SqlServerDbContextOptionsExtensions
                .UseSqlServer(new DbContextOptionsBuilder(), ConnectionString)
                .UseLazyLoadingProxies()
                .Options;
            var globalCtx = new Context(options);
            return new SqlMarkedProductRepository(globalCtx);
        }
        public IMarkInfoRepository CreateMarkInfoRep()
        {
            var options = SqlServerDbContextOptionsExtensions
                .UseSqlServer(new DbContextOptionsBuilder(), ConnectionString)
                .UseLazyLoadingProxies()
                .Options;
            var globalCtx = new Context(options);
            return new SqlMarkInfoRepository(globalCtx);
        }
        public IPersonMetadataRepository CreatePersonMetadataRep()
        {
            var options = SqlServerDbContextOptionsExtensions
                .UseSqlServer(new DbContextOptionsBuilder(), ConnectionString)
                .UseLazyLoadingProxies()
                .Options;
            var globalCtx = new Context(options);
            return new SqlPersonMetadataRepository(globalCtx);
        }
        public IProductRepository CreateProductRep()
        {
            var options = SqlServerDbContextOptionsExtensions
                .UseSqlServer(new DbContextOptionsBuilder(), ConnectionString)
                .UseLazyLoadingProxies()
                .Options;
            var globalCtx = new Context(options);
            return new SqlProductRepository(globalCtx);
        }
        public IProductRequestRepository CreateProductRequestRep()
        {
            var options = SqlServerDbContextOptionsExtensions
                .UseSqlServer(new DbContextOptionsBuilder(), ConnectionString)
                .UseLazyLoadingProxies()
                .Options;
            var globalCtx = new Context(options);
            return new SqlProductRequestRepository(globalCtx);
        }
        public IReceiptRepository CreateReceiptRep()
        {
            var options = SqlServerDbContextOptionsExtensions
                 .UseSqlServer(new DbContextOptionsBuilder(), ConnectionString)
                 .UseLazyLoadingProxies()
                 .Options;
            var globalCtx = new Context(options);
            return new SqlReceiptRepository(globalCtx);
        }
        public IVendorRepository CreateVendorRep()
        {
            var options = SqlServerDbContextOptionsExtensions
                .UseSqlServer(new DbContextOptionsBuilder(), ConnectionString)
                .UseLazyLoadingProxies()
                .Options;
            var globalCtx = new Context(options);
            return new SqlVendorRepository(globalCtx);
        }
        public IWriteOffProductRepository CreateWriteOffProductRep()
        {
            var options = SqlServerDbContextOptionsExtensions
                .UseSqlServer(new DbContextOptionsBuilder(), ConnectionString)
                .UseLazyLoadingProxies()
                .Options;
            var globalCtx = new Context(options);
            return new SqlWriteOffProductRepository(globalCtx);
        }
    }
}
