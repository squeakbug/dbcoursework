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
using ViewModel.Entities.StockmanEntities;
using ViewModel.StockmanViewModels;

namespace SDWPFView.StockmanViews
{
    public partial class InvoiceDetailsWindow : Window
    {
        private StockmanInvoicesViewModel _stockmanVM;
        private string _invoiceId;

        public InvoiceDetailsWindow(StockmanInvoicesViewModel stockmanVM, string invoiceId)
        {
            InitializeComponent();
            _stockmanVM = stockmanVM;
            _invoiceId = invoiceId;

            InvoiceDetailsVM invoiceDetails = _stockmanVM.GetInvoiceDetailsById(invoiceId);
            DocumentGTINTextBox.Text = invoiceDetails.DocumentGTIN;
            InvoiceDateTextBox.Text = invoiceDetails.InvoiceDate;
            InvoiceNumberTextBox.Text = invoiceDetails.InvoiceNumber;
            StateTextBox.Text = invoiceDetails.State;
            VendorTextBox.Text = invoiceDetails.VendorName;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
