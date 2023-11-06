using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification.Entities;
using DataAccessLayer;
using DataAccessLayer.RepositoriesImp;

namespace DataAccessLayer.Converters
{
    internal class ManufacturerConverter
    {
        public Manufacturer MapToBusinessEntity(Entities.Manufacturer manufacturer)
        {
            return new Manufacturer()
            {
                Id = manufacturer.Id,
                Concern = manufacturer.Concern,
                Country = manufacturer.Country,
                M_name = manufacturer.M_name
            };
        }

        public Entities.Manufacturer MapFromBusinessEntity(Manufacturer manufacturer)
        {
            return new Entities.Manufacturer()
            {
                Id = manufacturer.Id,
                Concern = manufacturer.Concern,
                Country = manufacturer.Country,
                M_name = manufacturer.M_name
            };
        }
    }
}
