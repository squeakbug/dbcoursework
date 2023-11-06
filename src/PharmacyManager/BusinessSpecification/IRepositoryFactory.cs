using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification.RepositoryInterfaces;

namespace BusinessSpecification
{
    public interface IRepositoryFactory
    {
        IEmployeeRepository CreateEmployeeRep();
        ICategoryRepository CreateCategoryRep();
        IIncomeProductRepository CreateIncomeProductRep();
        IInvoiceRepository CreateInvoiceRep();
        IManufacturerRepository CreateManufacturerRep();
        IMarkedProductRepository CreateMarkedProductRep();
        IMarkInfoRepository CreateMarkInfoRep();
        IPersonMetadataRepository CreatePersonMetadataRep();
        IProductRepository CreateProductRep();
        IProductRequestRepository CreateProductRequestRep();
        IReceiptRepository CreateReceiptRep();
        IVendorRepository CreateVendorRep();
        IWriteOffProductRepository CreateWriteOffProductRep();
    }
}
