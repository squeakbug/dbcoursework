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

using ViewModel;
using ViewModel.Entities;
using ViewModel.Entities.PharmacyManagerEntities;
using ViewModel.Entities.PharmacyManagerEntities.TableEntities;
using ViewModel.PharmacyManagerViewModels;

namespace SDWPFView.PharmacyManagerViews
{
    public partial class PharmacyManagerProductsControl : UserControl
    {
        private PharmacyManagerProductsViewModel _pharmacyManagerVM;

        public PharmacyManagerProductsControl()
        {
            InitializeComponent();
            _pharmacyManagerVM = new PharmacyManagerProductsViewModel();
            ProductsTable.ItemsSource = _pharmacyManagerVM.Products;
        }

        private void UpdateProductBtn_Click(object sender, RoutedEventArgs e)
        {
            var product = (ProductTableVM)ProductsTable.SelectedItem;
            if (product == null)
            {
                MessageBox.Show("Лекарственное средство не выбрано");
                return;
            }

            new EditProductWindow(_pharmacyManagerVM, product.Id).Show();
        }
    }
}
