using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using BusinessSpecification.Entities;
using DataAccessLayer;
using DataAccessLayer.RepositoriesImp;

namespace DataAccessLayer.Converters
{
    internal class ProductConverter
    {
        public Product MapToBusinessEntity(Entities.Product product, Context globalCtx)
        {
            var manufacturerRep = new SqlManufacturerRepository(globalCtx);
            //var categoryRep = new SqlCategoryRepository(globalCtx);
            var result = new Product
            {
                Id = product.Id,
                Manufacturer_id = product.Manufacturer_id,
                //Categories_id = categoryRep.GetCategoriesByProduct(product.Id),
                Articul = product.Articul,
                Count_in_package = product.Count_in_package,
                Default_markup = product.Default_markup,
                Dosage = product.Dosage,
                Dosage_form = product.Dosage_form,
                Gtin = product.Gtin,
                Instruction = product.Instruction,
                International_name = product.International_name,
                Leave_condition = product.Leave_condition,
                Maximum_markup = product.Maximum_markup,
                Maximum_shelf_life = product.Maximum_shelf_life,
                Photo = product.Photo,
                Primacy_packaging = product.Primacy_packaging,
                P_description = product.P_description,
                P_name = product.P_name,
                Storage_temperature = product.Storage_temperature,
                Threashold_count = product.Threashold_count,
                Trademark = product.Trademark
            };
            return result;
        }

        public Entities.Product MapFromBusinessEntity(Product product)
        {
            return new Entities.Product
            {
                Id = product.Id,
                Articul = product.Articul,
                Count_in_package = product.Count_in_package,
                Default_markup = product.Default_markup,
                Dosage = product.Dosage,
                Dosage_form = product.Dosage_form,
                Gtin = product.Gtin,
                Instruction = product.Instruction,
                International_name = product.International_name,
                Leave_condition = product.Leave_condition,
                Manufacturer_id = product.Manufacturer_id,
                Maximum_markup = product.Maximum_markup,
                Maximum_shelf_life = product.Maximum_shelf_life,
                Photo = product.Photo,
                Primacy_packaging = product.Primacy_packaging,
                P_description = product.P_description,
                P_name = product.P_name,
                Storage_temperature = product.Storage_temperature,
                Threashold_count = product.Threashold_count,
                Trademark = product.Trademark
            };
        }
    }
}
