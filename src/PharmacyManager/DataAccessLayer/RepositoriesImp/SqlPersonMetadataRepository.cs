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
    class SqlPersonMetadataRepository : IPersonMetadataRepository
    {
        private Context _globalCtx;

        public SqlPersonMetadataRepository(Context globalCtx)
        {
            _globalCtx = globalCtx;
        }

        public IEnumerable<PersonMetadata> GetPersonMetadatas()
        {
            var result = new List<PersonMetadata>();
            var converter = new PersonMetadataConverter();
            foreach (var item in _globalCtx.Person_metadata)
                result.Add(converter.MapToBusinessEntity(item));
            return result;
        }

        public PersonMetadata GetPersonMetadataById(int id)
        {
            var res = _globalCtx.Person_metadata.Find(id);
            var converter = new PersonMetadataConverter();
            return res == null ? null : converter.MapToBusinessEntity(res);
        }

        public void Create(PersonMetadata personMetadata)
        {
            var converter = new PersonMetadataConverter();
            _globalCtx.Person_metadata.Add(converter.MapFromBusinessEntity(personMetadata));
            _globalCtx.SaveChanges();
        }

        public void Update(PersonMetadata personMetadata)
        {
            var converter = new PersonMetadataConverter();
            _globalCtx.Person_metadata.Update(converter.MapFromBusinessEntity(personMetadata));
            _globalCtx.SaveChanges();
        }

        public void Delete(int id)
        {
            var employee = _globalCtx.Person_metadata.Find(id);
            if (employee != null)
            {
                _globalCtx.Person_metadata.Remove(employee);
                _globalCtx.SaveChanges();
            }
        }

        public void Truncate()
        {
            _globalCtx.Database.ExecuteSqlInterpolated($"DELETE dbo.person_metadata");
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
