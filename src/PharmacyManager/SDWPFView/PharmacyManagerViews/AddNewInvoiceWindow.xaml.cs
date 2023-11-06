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

using BusinessSpecification.Entities;
using ViewModel;
using ViewModel.Entities;
using ViewModel.Entities.PharmacyManagerEntities;
using ViewModel.Entities.PharmacyManagerEntities.TableEntities;
using ViewModel.PharmacyManagerViewModels;

namespace SDWPFView.PharmacyManagerViews
{
    public partial class AddNewInvoiceWindow : Window
    {
        private PharmacyManagerInvoicesViewModel _pharmacyManagerViewModel;
        public AddNewInvoiceWindow(PharmacyManagerInvoicesViewModel pharmacyManagerViewModel)
        {
            InitializeComponent();
            _pharmacyManagerViewModel = pharmacyManagerViewModel;
            IncomeProductsTable.ItemsSource = _pharmacyManagerViewModel.IncomeProducts;
        }

        private void AddProductBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddProductToInvoiceWindow(_pharmacyManagerViewModel).Show();
        }

        private void RemoveProductBtn_Click(object sender, RoutedEventArgs e)
        {
            var incomeProduct = (IncomeProduct)IncomeProductsTable.SelectedItem;
            if (incomeProduct == null)
            {
                MessageBox.Show("Лекарственное средство не выбрано");
                return;
            }

            try
            {
                _pharmacyManagerViewModel.DeleteIncomeProductFromInvoice(incomeProduct.Id);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void AddInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            var invoiceVM = new AddNewInvoiceVM();
            invoiceVM.InvoiceNumber = InvoiceNumberTextBox.Text;
            try
            {
                _pharmacyManagerViewModel.AddInvoice(invoiceVM);
            }
            catch (ApplicationException ex)
            {
                _pharmacyManagerViewModel.UpdateStatus("Ошибка при добавлении накладной");
                MessageBox.Show(ex.Message);
                return;
            }
            _pharmacyManagerViewModel.UpdateStatus("Накладная успешно добавлена");
            Cleanup();
            Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Cleanup();
            Close();
        }

        private void GetVendorBtn_Click(object sender, RoutedEventArgs e)
        {
            new VendorListWindow(_pharmacyManagerViewModel).Show();
        }

        private void Cleanup()
        {
            _pharmacyManagerViewModel.InvoiceIncomeProducts.Clear();
            _pharmacyManagerViewModel.SelectedVendorId = null;
        }
    }
}
