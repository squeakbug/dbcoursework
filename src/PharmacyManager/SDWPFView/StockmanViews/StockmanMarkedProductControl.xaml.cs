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
using ViewModel.Entities.StockmanEntities.TableEntities;
using ViewModel.StockmanViewModels;

namespace SDWPFView.StockmanViews
{
    public partial class StockmanMarkedProductControl : UserControl
    {
        private StockmanMarkedProductsViewModel _stockmanVM;
        public StockmanMarkedProductControl()
        {
            InitializeComponent();
            _stockmanVM = new StockmanMarkedProductsViewModel();
            MarkedProductTable.ItemsSource = _stockmanVM.MarkedProducts;
        }

        private void ShowMarkedProductDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            var markedProduct = (MarkedProductTableVM)MarkedProductTable.SelectedItem;
            if (markedProduct == null)
            {
                MessageBox.Show("Лекарственное средство не выбрано");
                return;
            }

            new MarkedProductDetailsWindow(_stockmanVM, markedProduct.Id).Show();
        }
    }
}
