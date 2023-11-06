using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using BusinessSpecification.Entities;
using DefaultBusinessLogic;
using ViewModel.Entities;
using ViewModel.Entities.PharmacyManagerEntities;
using ViewModel.Entities.PharmacyManagerEntities.TableEntities;
using ViewModel.Converters;

namespace ViewModel.PharmacyManagerViewModels
{
    public class PharmacyManagerProductsViewModel : PharmacyManagerBaseViewModel
    {
        public string SelectedManufacturerId { get; set; }
        public ObservableCollection<ProductTableVM> Products { get; set; }
        public ObservableCollection<ManufacturerTableVM> Manufacturers { get; set; }

        public PharmacyManagerProductsViewModel()
            : base()
        {
            Products = new ObservableCollection<ProductTableVM>();
            Manufacturers = new ObservableCollection<ManufacturerTableVM>();

            LoadProducts();
        }

        public void LoadProducts()
        {
            IEnumerable<Product> products = PharmacyManagerModel.GetProducts();
            foreach (var item in products)
            {
                var productVM = new ProductTableVM
                {
                    Articul = item.Articul,
                    Dosage = item.Dosage.ToString(),
                    Gtin = item.Gtin,
                    Id = item.Id.ToString(),
                    Leave_condition = item.Leave_condition,
                    Maximum_shelf_life = item.Maximum_shelf_life.ToString(),
                    P_name = item.P_description,
                    Storage_temperature = item.Storage_temperature.ToString(),
                    Threashold_count = item.Threashold_count.ToString(),
                    Trademark = item.Trademark
                };
                Products.Add(productVM);
            }
        }

        public void UpdateProduct(ProductVM productVM)
        {
            Product productBL = ProductConverter.MapToModel(productVM);
            if (SelectedManufacturerId != null && SelectedManufacturerId.Length != 0)
            {
                if (!int.TryParse(SelectedManufacturerId, out int manufacturerIdBL))
                    throw new ApplicationException("Bad manufacturer id");

                productBL.Manufacturer_id = manufacturerIdBL;
            }
            PharmacyManagerModel.UpdateProduct(productBL);
        }

        public void AddProduct(ProductVM productVM)
        {
            Product productBL = ProductConverter.MapToModel(productVM);
            PharmacyManagerModel.AddProduct(productBL);
        }
    }
}
