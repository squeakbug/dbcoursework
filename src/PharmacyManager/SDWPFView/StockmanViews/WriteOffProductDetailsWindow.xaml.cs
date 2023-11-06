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
    public partial class WriteOffProductDetailsWindow : Window
    {
        private StockmanWriteOffProductsViewModel _stockmanVM;
        private string _writeOffProductId;

        public WriteOffProductDetailsWindow(StockmanWriteOffProductsViewModel stockmanVM, string writeOffProductId)
        {
            InitializeComponent();
            _stockmanVM = stockmanVM;
            _writeOffProductId = writeOffProductId;

            WriteOffProductDetailsVM writeOffProductDetails =
                _stockmanVM.GetWriteOffProductDetailsById(writeOffProductId);

            CountTextBox.Text = writeOffProductDetails.Count;
            ProductNameTextBox.Text = writeOffProductDetails.ProductName;
            StorageLocationTextBox.Text = writeOffProductDetails.StorageLocation;
            WriteOffDateTextBox.Text = writeOffProductDetails.WriteOffDate;
            ReasonRichTextBox.Document.Blocks.Clear();
            ReasonRichTextBox.Document.Blocks.Add(new Paragraph(new Run(writeOffProductDetails.Reason)));
        }

        private void ShowProductDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            WriteOffProductVM writeOffProductVM = _stockmanVM.GetWriteOffProductById(_writeOffProductId);
            MarkInfoVM markInfoVM = _stockmanVM.GetMarkInfoById(writeOffProductVM.Mark_info_id);
            IncomeProductVM incomeProductVM = _stockmanVM.GetIncomeProductById(markInfoVM.Income_product_id);
            new ProductDetailsWindow(_stockmanVM, incomeProductVM.Product_id).Show();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            WriteOffProductVM writeOffProduct = _stockmanVM.GetWriteOffProductById(_writeOffProductId);
            MarkInfoVM markInfo = _stockmanVM.GetMarkInfoById(writeOffProduct.Mark_info_id);
            markInfo.Storage_location = StorageLocationTextBox.Text;

            try
            {
                _stockmanVM.UpdateWriteOffProduct(writeOffProduct, markInfo);
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
