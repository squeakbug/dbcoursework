using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification.Entities;

namespace BusinessSpecification.RepositoryInterfaces
{
    public interface IWriteOffProductRepository : IDisposable
    {
        IEnumerable<WriteOffProduct> GetWriteOffProducts();
        WriteOffProduct GetWriteOffProductById(int id);
        void Create(WriteOffProduct writeOffProduct);
        void Update(WriteOffProduct writeOffProduct);
        void Delete(int id);
        void Truncate();
    }
}
