using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

using BusinessSpecification.Entities;
using ViewModel.Entities;
using ViewModel.Entities.ProductManagerEntities;
using ViewModel.Entities.ProductManagerEntities.TableEntities;

namespace ViewModel.ProductManagerViewModels
{
    public class ProductManagerMarkedProductsViewModel : ProductManagerBaseViewModel
    {
        private MarkedProduct _selectedMarkedProduct;
        public ObservableCollection<MarkedProductTableVM> MarkedProducts { get; set; }

        public ProductManagerMarkedProductsViewModel()
            : base()
        {
            MarkedProducts = new ObservableCollection<MarkedProductTableVM>();

            LoadMarkedProducts();
        }

        public void LoadMarkedProducts()
        {
            MarkedProducts.Clear();
            var storedProducts = ProductManagerModel.GetStoredProducts();
            var markInfos = ProductManagerModel.GetMarkInfos();

            foreach (var item in storedProducts)
            {
                MarkInfo markInfoBL = markInfos.Where(x => x.Id == item.Mark_info_id).FirstOrDefault();

                var markedProductTableVM = new MarkedProductTableVM
                {
                    ApprovedDate = markInfoBL.approved_date.ToString(),
                    Count = markInfoBL.P_count.ToString(),
                    Id = item.Id.ToString(),
                    StorageLocation = markInfoBL.Storage_location
                };
                MarkedProducts.Add(markedProductTableVM);
            }
        }

        public MarkedProduct SelectedMarkedProduct
        {
            get => _selectedMarkedProduct;
            set
            {
                _selectedMarkedProduct = value;
                NotifyPropertyChanged(nameof(SelectedMarkedProduct));
            }
        }
    }
}
