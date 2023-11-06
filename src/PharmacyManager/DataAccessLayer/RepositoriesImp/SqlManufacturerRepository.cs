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
    class SqlManufacturerRepository : IManufacturerRepository
    {
        private Context _globalCtx;

        public SqlManufacturerRepository(Context globalCtx)
        {
            _globalCtx = globalCtx;
        }

        public IEnumerable<Manufacturer> GetManufacturers()
        {
            var result = new List<Manufacturer>();
            var converter = new ManufacturerConverter();
            foreach (var manufacturer in _globalCtx.Manufacturer)
                result.Add(converter.MapToBusinessEntity(manufacturer));
            return result;
        }

        public Manufacturer GetManufacturerById(int id)
        {
            var manufacturer = _globalCtx.Manufacturer.Find(id);
            var converter = new ManufacturerConverter();
            return manufacturer == null ? null : converter.MapToBusinessEntity(manufacturer);
        }

        public void Create(Manufacturer manufacturer)
        {
            var converter = new ManufacturerConverter();
            _globalCtx.Manufacturer.Add(converter.MapFromBusinessEntity(manufacturer));
            _globalCtx.SaveChanges();
        }

        public void Update(Manufacturer manufacturer)
        {
            var converter = new ManufacturerConverter();
            _globalCtx.Manufacturer.Update(converter.MapFromBusinessEntity(manufacturer));
            _globalCtx.SaveChanges();
        }

        public void Delete(int id)
        {
            var manufacturer = _globalCtx.Manufacturer.Find(id);
            if (manufacturer == null)
                throw new ManufacturerException("No manufacturer with such id");

            _globalCtx.Manufacturer.Remove(manufacturer);
            _globalCtx.SaveChanges();
        }

        public void Truncate()
        {
            _globalCtx.Database.ExecuteSqlInterpolated($"DELETE dbo.manufacturer");
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
