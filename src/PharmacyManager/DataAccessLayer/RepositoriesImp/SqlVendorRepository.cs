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
    class SqlVendorRepository : IVendorRepository
    {
        private Context _globalCtx;

        public SqlVendorRepository(Context globalCtx)
        {
            _globalCtx = globalCtx;
        }

        public IEnumerable<Vendor> GetVendors()
        {
            var result = new List<Vendor>();
            var converter = new VendorConverter();
            foreach (var vendor in _globalCtx.Vendor)
                result.Add(converter.MapToBusinessEntity(vendor));
            return result;
        }

        public Vendor GetVendorById(int id)
        {
            var vendor = _globalCtx.Vendor.Find(id);
            var converter = new VendorConverter();
            return vendor == null ? null : converter.MapToBusinessEntity(vendor);
        }

        public void Create(Vendor vendor)
        {
            var converter = new VendorConverter();
            _globalCtx.Vendor.Add(converter.MapFromBusinessEntity(vendor));
            _globalCtx.SaveChanges();
        }

        public void Update(Vendor vendor)
        {
            var converter = new VendorConverter();
            _globalCtx.Vendor.Update(converter.MapFromBusinessEntity(vendor));
            _globalCtx.SaveChanges();
        }

        public void Delete(int id)
        {
            var vendor = _globalCtx.Vendor.Find(id);
            if (vendor != null)
            {
                _globalCtx.Vendor.Remove(vendor);
                _globalCtx.SaveChanges();
            }
        }

        public void Truncate()
        {
            _globalCtx.Database.ExecuteSqlInterpolated($"DELETE dbo.vendor");
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
