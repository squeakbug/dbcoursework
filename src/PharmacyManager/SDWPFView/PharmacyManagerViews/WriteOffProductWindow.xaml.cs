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
using ViewModel.PharmacyManagerViewModels;

namespace SDWPFView.PharmacyManagerViews
{
    public partial class WriteOffProductWindow : Window
    {
        private PharmacyManagerMarkedProductsViewModel _pharmacyManagerVM;
        private string _markedProductId;

        public WriteOffProductWindow(PharmacyManagerMarkedProductsViewModel pharmacyManagerVM, 
            string markedProductId)
        {
            InitializeComponent();
            _pharmacyManagerVM = pharmacyManagerVM;
            _markedProductId = markedProductId;

            MarkedProductVM markedProductVM = _pharmacyManagerVM.GetMarkedProductById(markedProductId);
            MarkInfoVM markInfoVM = _pharmacyManagerVM.GetMarkInfoById(markedProductVM.Mark_info_id);
            IncomeProductVM incomeProductVM = _pharmacyManagerVM.GetIncomeProductById(markInfoVM.Income_product_id);
            ProductVM productVM = _pharmacyManagerVM.GetProductById(incomeProductVM.Product_id);
            ProductNameLabel.Content = productVM.P_name;
        }

        private void WriteOffProductBtn_Click(object sender, RoutedEventArgs e)
        {
            string cnt = CountTextBox.Text;
            string reason = new TextRange(ReasonRichTextBox.Document.ContentStart,
                ReasonRichTextBox.Document.ContentEnd).Text;

            try
            {
                _pharmacyManagerVM.DoWriteOffProduct(_markedProductId, cnt, reason);
            }
            catch (ApplicationException ex)
            {
                _pharmacyManagerVM.UpdateStatus("Ошибка при списании товара");
                MessageBox.Show(ex.Message);
                return;
            }
            Close();
        }

        private void CancelProductBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
