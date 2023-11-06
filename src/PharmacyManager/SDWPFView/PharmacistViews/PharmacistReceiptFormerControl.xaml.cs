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
using ViewModel.PharmacistViewModels;

namespace SDWPFView.PharmacistViews
{
    public partial class PharmacistReceiptFormerControl : UserControl
    {
        private PharmacistReceiptFormerViewModel _pharmacistVM;

        public PharmacistReceiptFormerControl()
        {
            InitializeComponent();
            _pharmacistVM = new PharmacistReceiptFormerViewModel();
            ReceiptMarkedProductsTable.ItemsSource = _pharmacistVM.ReceiptMarkedProducts;
            TotalPriceTextBox.DataContext = _pharmacistVM;
            TotalPriceTextBox.Text = _pharmacistVM.TotalPrice.ToString();
        }

        private void AddProductToReceipt_Click(object sender, RoutedEventArgs e)
        {
            new AddProductToReceiptWindow(_pharmacistVM).Show();
        }

        private void CancelOperation_Click(object sender, RoutedEventArgs e)
        {
            CleanupFields();
        }

        private void ConfirmOperation_Click(object sender, RoutedEventArgs e)
        {
            string discountVM = DiscountTextBox.Text;
            string cashSizeVM = CashValueTextBox.Text;
            string cardSizeVM = CardValueTextBox.Text;

            try
            {
                _pharmacistVM.ConfirmReceiptOperation(discountVM, cashSizeVM, cardSizeVM);
                _pharmacistVM.ReceiptMarkedProducts.Clear();
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            CleanupFields();
        }

        private void CleanupFields()
        {
            CardValueTextBox.Clear();
            CashValueTextBox.Clear();
            DiscountTextBox.Clear();
            _pharmacistVM.TotalPrice = "0";
            _pharmacistVM.ReceiptMarkedProducts.Clear();
            TotalPriceTextBox.Text = "0";
        }
    }
}
