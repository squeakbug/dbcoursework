using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification.Entities;

namespace BusinessSpecification.RepositoryInterfaces
{
    public interface IManufacturerRepository : IDisposable
    {
        IEnumerable<Manufacturer> GetManufacturers();
        Manufacturer GetManufacturerById(int id);
        void Create(Manufacturer manufacturer);
        void Update(Manufacturer manufacturer);
        void Delete(int id);
        void Truncate();
    }
}
