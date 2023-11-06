using System;
using System.Collections.Generic;
using System.Text;

using ViewModel.Entities;
using BusinessSpecification.Entities;

namespace ViewModel.Converters
{
    public static class ManufacturerConverter
    {
        public static ManufacturerVM MapToViewModel(Manufacturer manufacturer)
        {
            return new ManufacturerVM
            {
                Concern = manufacturer.Concern,
                Country = manufacturer.Country,
                Id = manufacturer.Id.ToString(),
                Name = manufacturer.M_name
            };
        }
    }
}
