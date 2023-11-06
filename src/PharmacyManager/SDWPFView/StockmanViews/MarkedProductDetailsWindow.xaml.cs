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
using System.Windows.Shapes;

using ViewModel;
using ViewModel.Entities;
using ViewModel.Entities.StockmanEntities;
using ViewModel.StockmanViewModels;

namespace SDWPFView.StockmanViews
{
    public partial class MarkedProductDetailsWindow : Window
    {
        private StockmanMarkedProductsViewModel _stockmanVM;
        private string _markedProductId;

        public MarkedProductDetailsWindow(StockmanMarkedProductsViewModel stockmanVM, string markedProductId)
        {
            InitializeComponent();
            _stockmanVM = stockmanVM;
            _markedProductId = markedProductId;

            MarkedProductDetailsVM markedProductDetails = _stockmanVM.GetMarkedProductDetailsById(markedProductId);
            ApprovedDateTextBox.Text = markedProductDetails.ApprovedDate;
            CountTextBox.Text = markedProductDetails.Count;
            ProductNameTextBox.Text = markedProductDetails.ProductName;
            StorageLocationTextBox.Text = markedProductDetails.StorageLocation;
        }

        private void ShowProductDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            MarkedProductVM markedProductVM = _stockmanVM.GetMarkedProductById(_markedProductId);
            MarkInfoVM markInfoVM = _stockmanVM.GetMarkInfoById(markedProductVM.Mark_info_id);
            IncomeProductVM incomeProductVM = _stockmanVM.GetIncomeProductById(markInfoVM.Income_product_id);
            new ProductDetailsWindow(_stockmanVM, incomeProductVM.Product_id).Show();
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            MarkedProductVM markedProduct = _stockmanVM.GetMarkedProductById(_markedProductId);
            MarkInfoVM markInfo = _stockmanVM.GetMarkInfoById(markedProduct.Mark_info_id);
            markInfo.Storage_location = StorageLocationTextBox.Text;

            try
            {
                _stockmanVM.UpdateMarkedProduct(markedProduct, markInfo);
            }
            catch (ApplicationException ex)
            {
                _stockmanVM.UpdateStatus("Ошибка при обновлении информации о ЛС");
                MessageBox.Show(ex.Message);
                return;
            }
            _stockmanVM.UpdateStatus("Информация о ЛС обновлена");
            Close();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
