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
    public partial class StockmanWriteOffProductControl : UserControl
    {
        private StockmanWriteOffProductsViewModel _stockmanVM;
        public StockmanWriteOffProductControl()
        {
            InitializeComponent();
            _stockmanVM = new StockmanWriteOffProductsViewModel();
            WriteOffProductsTable.ItemsSource = _stockmanVM.WriteOffProducts;
        }

        private void ShowWriteOffProductDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            var writeOffProduct = (WriteOffProductTableVM)WriteOffProductsTable.SelectedItem;
            if (writeOffProduct == null)
            {
                MessageBox.Show("Лекарственное средство не выбрано");
                return;
            }

            new WriteOffProductDetailsWindow(_stockmanVM, writeOffProduct.Id).Show();
        }
    }
}
