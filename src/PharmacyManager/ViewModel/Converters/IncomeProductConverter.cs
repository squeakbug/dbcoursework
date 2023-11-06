using System;
using System.Collections.Generic;
using System.Text;

using ViewModel.Entities;
using BusinessSpecification.Entities;

namespace ViewModel.Converters
{ 
    public static class IncomeProductConverter
    {
        public static IncomeProductVM MapToViewModel(IncomeProduct incomeProduct)
        {
            return new IncomeProductVM
            {
                Id = incomeProduct.Id.ToString(),
                Invoice_id = incomeProduct.Invoice_id.ToString(),
                Measure_unit = incomeProduct.Measure_unit.ToString(),
                Production_date = incomeProduct.Production_date.ToString(),
                Product_id = incomeProduct.Product_id.ToString(),
                P_count = incomeProduct.P_count.ToString(),
                Series = incomeProduct.Series.ToString(),
                Storage_location = incomeProduct.Storage_location,
                Vendor_price = incomeProduct.Vendor_price.ToString(),
                Vendor_vax = incomeProduct.Vendor_vax.ToString()
            };
        }
    }
}
