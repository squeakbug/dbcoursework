using System;
using System.ComponentModel;
using System.Collections.ObjectModel;

using BusinessSpecification.Entities;
using DefaultBusinessLogic;
using ViewModel.Entities;
using ViewModel.Entities.PharmacistEntities;
using ViewModel.Entities.PharmacistEntities.TableEntities;
using ViewModel.Converters;

namespace ViewModel.PharmacistViewModels
{
    public class PharmacistProductsViewModel : PharmacistBaseViewModel
    {
        private DetailProductInfoVM _currentProductInfoVM;
        public ObservableCollection<ProductTableVM> Products { get; set; }

        public PharmacistProductsViewModel()
        {
            Products = new ObservableCollection<ProductTableVM>();

            LoadProducts();
        }

        public void LoadProducts()
        {
            var products = PharmacistModel.GetProducts();
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

        public DetailProductInfoVM CurrentProductInfoVM
        {
            get => _currentProductInfoVM;
            set
            {
                _currentProductInfoVM = value;
                NotifyPropertyChanged(nameof(CurrentProductInfoVM));
            }
        }
    }
}
