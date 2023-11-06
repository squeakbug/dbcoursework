using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification.Entities;
using DataAccessLayer;

namespace DataAccessLayer.Converters
{
    internal class PersonMetadataConverter
    {
        public PersonMetadata MapToBusinessEntity(Entities.PersonMetadata personMetadata)
        {
            return new PersonMetadata
            {
                Id = personMetadata.Id,
                Email = personMetadata.Email,
                First_name = personMetadata.First_name,
                Last_name = personMetadata.Last_name,
                Middle_name = personMetadata.Middle_name,
                Phone = personMetadata.Phone
            };
        }

        public Entities.PersonMetadata MapFromBusinessEntity(PersonMetadata personMetadata)
        {
            return new Entities.PersonMetadata
            {
                Id = personMetadata.Id,
                Email = personMetadata.Email,
                First_name = personMetadata.First_name,
                Last_name = personMetadata.Last_name,
                Middle_name = personMetadata.Middle_name,
                Phone = personMetadata.Phone
            };
        }
    }
}
