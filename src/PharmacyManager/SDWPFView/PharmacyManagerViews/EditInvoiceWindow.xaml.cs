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
using ViewModel.Entities.PharmacyManagerEntities;
using ViewModel.PharmacyManagerViewModels;

namespace SDWPFView.PharmacyManagerViews
{
    public partial class EditInvoiceWindow : Window
    {
        private PharmacyManagerInvoicesViewModel _pharmacyManagerVM;
        private readonly string _invoiceId;

        public EditInvoiceWindow(PharmacyManagerInvoicesViewModel pharmacyManagerVM, string invoiceId)
        {
            InitializeComponent();
            _pharmacyManagerVM = pharmacyManagerVM;
            _invoiceId = invoiceId;

            InvoiceVM invoiceVM = _pharmacyManagerVM.GetInvoiceById(invoiceId);
            VendorVM vendorVM = _pharmacyManagerVM.GetVendorById(invoiceVM.vendor_id);
            _pharmacyManagerVM.LoadEditIncomeProducts(invoiceId);
            InvoiceNumberTextBox.Text = invoiceVM.invoice_number;
            VendorLabel.Content = vendorVM.ShortName;
        }

        private void RemoveProductBtn_Click(object sender, RoutedEventArgs e)
        {
            return;
            //throw new NotImplementedException();
        }

        private void AddProductBtn_Click(object sender, RoutedEventArgs e)
        {
            return;
            //throw new NotImplementedException();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UpdateInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            InvoiceVM invoiceVM = _pharmacyManagerVM.GetInvoiceById(_invoiceId);

            invoiceVM.invoice_number = InvoiceNumberTextBox.Text;

            try
            {
                _pharmacyManagerVM.UpdateInvoice(invoiceVM);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            _pharmacyManagerVM.UpdateStatus("Накладная успешно обновлена");
            Close();
        }
    }
}
