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

using BusinessSpecification.Entities;
using ViewModel;
using ViewModel.Entities;
using ViewModel.Entities.PharmacyManagerEntities;
using ViewModel.Entities.PharmacyManagerEntities.TableEntities;
using ViewModel.PharmacyManagerViewModels;

namespace SDWPFView.PharmacyManagerViews
{
    public partial class PharmacyManagerInvoiceControl : UserControl
    {
        private PharmacyManagerInvoicesViewModel _pharmacyManagerVM;
        public PharmacyManagerInvoiceControl()
        {
            InitializeComponent();
            _pharmacyManagerVM = new PharmacyManagerInvoicesViewModel();
            InvoiceTable.ItemsSource = _pharmacyManagerVM.Invoices;
            InvoiceProductsTable.ItemsSource = _pharmacyManagerVM.InvoiceIncomeProducts;
        }

        private void UpdateInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            var invoice = (InvoiceTableVM)InvoiceTable.SelectedItem;
            if (invoice == null)
            {
                MessageBox.Show("Накладная не выбрана");
                return;
            }

            var newWindow = new EditInvoiceWindow(_pharmacyManagerVM, invoice.Id.ToString());
            newWindow.Show();
        }

        private void AddInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddNewInvoiceWindow(_pharmacyManagerVM).Show();
        }

        private void ShowIncomeProductsBtn_Click(object sender, RoutedEventArgs e)
        {
            var invoice = (InvoiceTableVM)InvoiceTable.SelectedItem;
            if (invoice == null)
            {
                MessageBox.Show("Накладная не выбрана");
                return;
            }

            try
            {
                _pharmacyManagerVM.LoadIncomeProducts(invoice.Id);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                _pharmacyManagerVM.UpdateStatus("Ошибка при просмотре товаров накладной");
                return;
            }
        }

        private void ApproveBtn_Click(object sender, RoutedEventArgs e)
        {
            var invoice = (InvoiceTableVM)InvoiceTable.SelectedItem;
            if (invoice == null)
            {
                MessageBox.Show("Накладная не выбрана");
                return;
            }

            try
            {
                _pharmacyManagerVM.ApproveInvoice(invoice.Id);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                _pharmacyManagerVM.UpdateStatus("Ошибка при отправке в продажу");
                return;
            }
            _pharmacyManagerVM.UpdateStatus("Лекарственные средства поступили в продажу");
        }

        private void ShowIncomeProductDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            var incomeProduct = (IncomeProductTableVM)InvoiceProductsTable.SelectedItem;
            if (incomeProduct == null)
            {
                MessageBox.Show("Лекарственное средство не выбрано");
                return;
            }

            new IncomeProductDetails(_pharmacyManagerVM, incomeProduct.Id).Show();
        }
    }
}
