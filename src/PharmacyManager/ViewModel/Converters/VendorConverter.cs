using System;
using System.Collections.Generic;
using System.Text;

using ViewModel.Entities;
using BusinessSpecification.Entities;

namespace ViewModel.Converters
{
    public static class VendorConverter
    {
        public static VendorVM MapToViewModel(Vendor vendor)
        {
            return new VendorVM
            {
                FullName = vendor.Full_name,
                Id = vendor.Id.ToString(),
                Inn = vendor.Inn.ToString(),
                Kpp = vendor.Kpp.ToString(),
                Phone = vendor.Phone,
                ShortName = vendor.Short_name
            };
        }

        public static Vendor MapToModel(VendorVM vendorVM)
        {
            var vendorBL = new Vendor();

            if (vendorVM.Id != null && vendorVM.Id.Length != 0)
            {
                if (!int.TryParse(vendorVM.Id, out int vendorId))
                    throw new ApplicationException("Bad vendor id");
                vendorBL.Id = vendorId;
            }
            if (vendorVM.Inn != null && vendorVM.Inn.Length != 0)
            {
                if (!int.TryParse(vendorVM.Inn, out int tmpInn))
                    throw new ApplicationException("Bad vendor inn");
                vendorBL.Inn = tmpInn;
            }
            if (vendorVM.Kpp.Length != 0)
            {
                if (!int.TryParse(vendorVM.Kpp, out int tmpKpp))
                    throw new ApplicationException("Bad vendor kpp");
                vendorBL.Kpp = tmpKpp;
            }

            vendorBL.Full_name = vendorVM.FullName;
            vendorBL.Phone = vendorVM.Phone;
            vendorBL.Short_name = vendorVM.ShortName;

            return vendorBL;
        }
    }
}
