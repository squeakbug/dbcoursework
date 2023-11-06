using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using BusinessSpecification.Entities;
using DataAccessLayer;
using DataAccessLayer.RepositoriesImp;
using BusinessSpecification.Filters;

namespace DataAccessLayer.Converters
{
    internal class IncomeProductConverter
    {
        public IncomeProduct MapToBusinessEntity(Entities.IncomeProduct incomeProduct, Context globalCtx)
        {
            var result = new IncomeProduct()
            {
                Id = incomeProduct.Id,
                Invoice_id = incomeProduct.Invoice_id,
                Measure_unit = incomeProduct.Measure_unit,
                Production_date = incomeProduct.Production_date,
                Product_id = incomeProduct.Product_id,
                P_count = incomeProduct.P_count,
                Series = incomeProduct.Series,
                Storage_location = incomeProduct.Storage_location,
                Vendor_price = incomeProduct.Vendor_price,
                Vendor_vax = incomeProduct.Vendor_vax
            };
            return result;
        }

        public Entities.IncomeProduct MapFromBusinessEntity(IncomeProduct incomeProduct)
        {
            return new Entities.IncomeProduct()
            {
                Id = incomeProduct.Id,
                Invoice_id = incomeProduct.Invoice_id,
                Measure_unit = incomeProduct.Measure_unit,
                Production_date = incomeProduct.Production_date,
                Product_id = incomeProduct.Product_id,
                P_count = incomeProduct.P_count,
                Series = incomeProduct.Series,
                Storage_location = incomeProduct.Storage_location,
                Vendor_price = incomeProduct.Vendor_price,
                Vendor_vax = incomeProduct.Vendor_vax
            };
        }
    }
}
