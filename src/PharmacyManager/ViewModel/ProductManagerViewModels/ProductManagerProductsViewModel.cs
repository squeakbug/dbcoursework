using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using BusinessSpecification.Entities;
using ViewModel.Entities;
using ViewModel.Converters;

namespace ViewModel.ProductManagerViewModels
{
    public class ProductManagerProductsViewModel : ProductManagerBaseViewModel
    {
        private Product _selectedProduct;
        public ObservableCollection<Product> Products { get; set; }

        public ProductManagerProductsViewModel()
            : base()
        {
            Products = new ObservableCollection<Product>(ProductManagerModel.GetProducts());
        }

        public void LoadProducts()
        {
            Products.Clear();
            IEnumerable<Product> products = ProductManagerModel.GetProducts();
            foreach (var item in products)
                Products.Add(item);
        }

        public void UpdateProduct(ProductVM productVM)
        {
            Product productBL = ProductConverter.MapToModel(productVM);
            ProductManagerModel.UpdateProduct(productBL);
            LoadProducts();
        }

        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                NotifyPropertyChanged(nameof(SelectedProduct));
            }
        }
    }
}
