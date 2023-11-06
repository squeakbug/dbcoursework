using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification.Entities;
using ViewModel.Entities;

namespace ViewModel.Converters
{
    public static class ProductConverter
    {
        public static ProductVM MapToViewModel(Product product)
        {
            var productVM = new ProductVM
            {
                Id = product.Id.ToString(),
                ManufacturerId = product.Manufacturer_id.ToString(),
                Articul = product.Articul,
                Count_in_package = product.Count_in_package.ToString(),
                Default_markup = product.Default_markup.ToString(),
                Dosage = product.Dosage.ToString(),
                Dosage_form = product.Dosage_form,
                Gtin = product.Gtin,
                Instruction = product.Instruction,
                International_name = product.International_name,
                Leave_condition = product.Leave_condition,
                Maximum_markup = product.Maximum_markup.ToString(),
                Maximum_shelf_life = product.Maximum_shelf_life.ToString(),
                Primacy_packaging = product.Primacy_packaging,
                P_description = product.P_description,
                P_name = product.P_name,
                Storage_temperature = product.Storage_temperature.ToString(),
                Threashold_count = product.Threashold_count.ToString(),
                Trademark = product.Trademark
            };
            /*
            var categoryIdList = new List<string>();
            foreach (var item in product.Categories_id)
                categoryIdList.Add(item.ToString());
            productVM.CategoriesId = categoryIdList;
            */

            return productVM;
        }

        public static Product MapToModel(ProductVM productVM)
        {
            var productBL = new Product();
            if (productVM.Id != null && productVM.Id.Length != 0)
            {
                if (!int.TryParse(productVM.Id, out int productId))
                    throw new ApplicationException("Bad product id");
                productBL.Id = productId;
            }
            if (productVM.ManufacturerId != null && productVM.ManufacturerId.Length != 0)
            {
                if (!int.TryParse(productVM.ManufacturerId, out int manufacturerId))
                    throw new ApplicationException("Bad product manufacturer id");
                productBL.Manufacturer_id = manufacturerId;
            }
            if (productVM.Maximum_markup != null && productVM.Maximum_markup.Length != 0)
            {
                if (!decimal.TryParse(productVM.Maximum_markup, out decimal maximumMarkup))
                    throw new ApplicationException("Bad product maximum markup");
                productBL.Maximum_markup = maximumMarkup;
            }
            if (productVM.Maximum_shelf_life != null && productVM.Maximum_shelf_life.Length != 0)
            {
                if (!int.TryParse(productVM.Maximum_shelf_life, out int maxShelfLife))
                    throw new ApplicationException("Bad product maximum shelflife");
                productBL.Maximum_shelf_life = maxShelfLife;
            }
            if (productVM.Storage_temperature != null && productVM.Storage_temperature.Length != 0)
            {
                if (!double.TryParse(productVM.Storage_temperature, out double storageTemperature))
                    throw new ApplicationException("Bad product storage temperature");
                productBL.Storage_temperature = storageTemperature;
            }
            if (productVM.Threashold_count != null && productVM.Threashold_count.Length != 0)
            {
                if (!int.TryParse(productVM.Threashold_count, out int threasholdCount))
                    throw new ApplicationException("Bad product threashold count");
                productBL.Threashold_count = threasholdCount;
            }
            if (productVM.Count_in_package != null && productVM.Count_in_package.Length != 0)
            {
                if (!int.TryParse(productVM.Count_in_package, out int countInPackage))
                    throw new ApplicationException("Bad product count in package");
                productBL.Count_in_package = countInPackage;
            }
            if (productVM.Default_markup != null && productVM.Default_markup.Length != 0)
            {
                if (!int.TryParse(productVM.Default_markup, out int defaultMarkup))
                    throw new ApplicationException("Bad product default markup");
                productBL.Default_markup = defaultMarkup;
            }
            if (productVM.Dosage != null && productVM.Default_markup.Length != 0)
            {
                if (!double.TryParse(productVM.Dosage, out double dosage))
                    throw new ApplicationException("Bad product dosage");
                productBL.Dosage = dosage;
            }

            productBL.Gtin = productVM.Gtin;
            productBL.Instruction = productVM.Instruction;
            productBL.International_name = productVM.International_name;
            productBL.Leave_condition = productVM.Leave_condition;
            productBL.Primacy_packaging = productVM.Primacy_packaging;
            productBL.P_description = productVM.P_description;
            productBL.P_name = productVM.P_name;
            productBL.Trademark = productVM.Trademark;
            productBL.Dosage_form = productVM.Dosage_form;
            productBL.Articul = productVM.Articul;

            /*
            var categoryIdList = new List<int>();
            foreach (var item in productVM.CategoriesId)
            {
                if (item != null && item.Length != 0)
                {
                    if (!int.TryParse(item, out int categoryId))
                        throw new ApplicationException("Bad product category id");
                    categoryIdList.Add(categoryId);
                }
            }
            productBL.Categories_id = categoryIdList;
            */

            return productBL;
        }
    };
}
