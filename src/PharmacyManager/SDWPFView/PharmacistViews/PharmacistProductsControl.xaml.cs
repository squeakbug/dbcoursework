using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using BusinessSpecification.Entities;
using ViewModel;
using ViewModel.Entities;
using ViewModel.Entities.PharmacistEntities;
using ViewModel.Entities.PharmacistEntities.TableEntities;
using ViewModel.PharmacistViewModels;

namespace SDWPFView.PharmacistViews
{
    public partial class PharmacistProductsControl : UserControl
    {
        private PharmacistProductsViewModel _pharmacistVM;
        public PharmacistProductsControl()
        {
            InitializeComponent();
            _pharmacistVM = new PharmacistProductsViewModel();
            ProductsTable.ItemsSource = _pharmacistVM.Products;
        }

        private void ShowProductInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = (ProductTableVM)ProductsTable.SelectedItem;
            if (selectedProduct == null)
            {
                _pharmacistVM.UpdateStatus("Не выбрано лекарственное средство");
                return;
            }

            new ProductDetailsWindow(_pharmacistVM, selectedProduct.Id).Show();
        }
    }
}
