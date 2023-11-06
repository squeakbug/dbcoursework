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
    public partial class IncomeProductDetails : Window
    {
        private StockmanInvoicesViewModel _stockmanVM;
        private string _incomeProductId;

        public IncomeProductDetails(StockmanInvoicesViewModel stockmanVM, string incomeProductId)
        {
            InitializeComponent();
            _stockmanVM = stockmanVM;
            _incomeProductId = incomeProductId;

            IncomeProductDetailsVM incomeProductDetailsVM = _stockmanVM.GetIncomeProductDetailsById(incomeProductId);
            CountTextBox.Text = incomeProductDetailsVM.Count;
            MeasureUnitTextBox.Text = incomeProductDetailsVM.MeasureUnit;
            ProductionDateTextBox.Text = incomeProductDetailsVM.ProductionDate;
            ProductNameTextBox.Text = incomeProductDetailsVM.ProductName;
            SeriesTextBox.Text = incomeProductDetailsVM.Series;
            StorageLocationTextBox.Text = incomeProductDetailsVM.StorageLocation;
        }

        private void ShowProductDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            IncomeProductVM incomeProductVM = _stockmanVM.GetIncomeProductById(_incomeProductId);
            new ProductDetailsWindow(_stockmanVM, incomeProductVM.Product_id).Show();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            IncomeProductVM incomeProduct = _stockmanVM.GetIncomeProductById(_incomeProductId);
            incomeProduct.Storage_location = StorageLocationTextBox.Text;

            try
            {
                _stockmanVM.UpdateIncomeProduct(incomeProduct);
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
    }
}
