using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification.Entities;
using BusinessSpecification.Filters;

namespace BusinessSpecification.RepositoryInterfaces
{
    public interface IIncomeProductRepository : IDisposable
    {
        IEnumerable<IncomeProduct> GetIncomeProducts(IncomeProductFilter filter);
        IncomeProduct GetIncomeProductById(int id);
        IEnumerable<IncomeProduct> GetIncomeProductByInvoiceId(int invoiceId);
        void Create(IncomeProduct incomeProduct);
        void Update(IncomeProduct incomeProduct);
        void Delete(int id);
        void Truncate();
    }
}
