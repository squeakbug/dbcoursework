using System;
using System.Collections.Generic;
using System.Text;

using ViewModel.Entities;
using BusinessSpecification.Entities;

namespace ViewModel.Converters
{
    public static class EmployeeConverter
    {
        public static EmployeeVM MapToViewModel(Employee employeeBL, PersonMetadata metadataBL)
        {
            return new EmployeeVM
            {
                Appointment = employeeBL.appointment,
                Login = employeeBL.e_login,
                Passwd = employeeBL.p_hash,
                PersonMetadata = new PersonMetadataVM
                {
                    Email = metadataBL.Email,
                    First_name = metadataBL.First_name,
                    Id = metadataBL.Id,
                    Last_name = metadataBL.Last_name,
                    Middle_name = metadataBL.Middle_name,
                    Phone = metadataBL.Phone
                },
            };
        }
    }
}
