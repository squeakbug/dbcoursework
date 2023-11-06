using System;
using System.Collections.Generic;

using BusinessSpecification.Entities;

namespace BusinessSpecification.RepositoryInterfaces
{
    public interface IPersonMetadataRepository : IDisposable
    {
        IEnumerable<PersonMetadata> GetPersonMetadatas();
        PersonMetadata GetPersonMetadataById(int id);
        void Create(PersonMetadata personMetadata);
        void Update(PersonMetadata personMetadata);
        void Delete(int id);
        void Truncate();
    }
}
