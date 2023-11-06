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
using BusinessSpecification.Entities;
using ViewModel.ProductManagerViewModels;

namespace SDWPFView.ProductManagerViews
{
    public partial class ProductManagerProductsControl : UserControl
    {
        private ProductManagerProductsViewModel _productManagerVM;

        public ProductManagerProductsControl()
        {
            InitializeComponent();
            _productManagerVM = new ProductManagerProductsViewModel();
            ProductsTable.ItemsSource = _productManagerVM.Products;
        }

        private void EditProductBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = (Product)ProductsTable.SelectedItem;
            if (selectedProduct == null)
            {
                MessageBox.Show("Лекарственное средство не выбрано");
                return;
            }

            new EditProductWindow(_productManagerVM, selectedProduct.Id.ToString()).Show();
        }
    }
}
