using System;
using System.Collections.Generic;
using System.Text;

using ViewModel.Entities;
using BusinessSpecification.Entities;

namespace ViewModel.Converters
{
    public static class PersonMetadataConverter
    {
        public static PersonMetadataVM MapToViewModel(PersonMetadata metadata)
        {
            return new PersonMetadataVM
            {
                Email = metadata.Email,
                First_name = metadata.First_name,
                Id = metadata.Id,
                Last_name = metadata.Last_name,
                Middle_name = metadata.Middle_name,
                Phone = metadata.Phone
            };
        }
    }
}
