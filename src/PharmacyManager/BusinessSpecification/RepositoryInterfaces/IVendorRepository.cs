using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification.Entities;

namespace BusinessSpecification.RepositoryInterfaces
{
    public interface IVendorRepository : IDisposable
    {
        IEnumerable<Vendor> GetVendors();
        Vendor GetVendorById(int id);
        void Create(Vendor vendor);
        void Update(Vendor vendor);
        void Delete(int id);
        void Truncate();
    }
}
