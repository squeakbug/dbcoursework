using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification.Entities;
using DataAccessLayer;
using DataAccessLayer.RepositoriesImp;

namespace DataAccessLayer.Converters
{
    internal class VendorConverter
    {
        public Vendor MapToBusinessEntity(Entities.Vendor vendor)
        {
            return new Vendor
            {
                Id = vendor.Id,
                Full_name = vendor.Full_name,
                Inn = vendor.Inn,
                Kpp = vendor.Kpp,
                Phone = vendor.Phone,
                Short_name = vendor.Short_name
            };
        }

        public Entities.Vendor MapFromBusinessEntity(Vendor vendor)
        {
            return new Entities.Vendor
            {
                Id = vendor.Id,
                Full_name = vendor.Full_name,
                Inn = vendor.Inn,
                Kpp = vendor.Kpp,
                Phone = vendor.Phone,
                Short_name = vendor.Short_name
            };
        }
    }
}
