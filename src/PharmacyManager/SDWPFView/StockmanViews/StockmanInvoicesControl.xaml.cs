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
using ViewModel.Entities.StockmanEntities;
using ViewModel.Entities.StockmanEntities.TableEntities;
using ViewModel.StockmanViewModels;

namespace SDWPFView.StockmanViews
{
    public partial class StockmanInvoicesControl : UserControl
    {
        private StockmanInvoicesViewModel _stockmanVM;

        public StockmanInvoicesControl()
        {
            InitializeComponent();
            _stockmanVM = new StockmanInvoicesViewModel();
            InvoiceTable.ItemsSource = _stockmanVM.Invoices;
            InvoiceProductsTable.ItemsSource = _stockmanVM.InvoiceIncomeProducts;
        }

        private void AcceptBtn_Click(object sender, RoutedEventArgs e)
        {
            var invoice = (InvoiceTableVM)InvoiceTable.SelectedItem;
            if (invoice == null)
            {
                _stockmanVM.UpdateStatus("Не выбрана накладная");
                return;
            }

            try
            {
                _stockmanVM.AcceptInvoice(invoice.Id);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                _stockmanVM.UpdateStatus("Ошибка при принятии накладной");
                return;
            }
            _stockmanVM.UpdateStatus("Накладная успешно принята");
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
                _stockmanVM.LoadIncomeProducts(invoice.Id);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                _stockmanVM.UpdateStatus("Ошибка при просмотре товаров накладной");
                return;
            }
        }

        private void ShowMarkedProductDetails_Click(object sender, RoutedEventArgs e)
        {
            var incomeProduct = (IncomeProductTableVM)InvoiceProductsTable.SelectedItem;
            if (incomeProduct == null)
            {
                MessageBox.Show("Лекарственное средство не выбрано");
                return;
            }

            new IncomeProductDetails(_stockmanVM, incomeProduct.Id).Show();
        }

        private void ShowInvoiceDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            var invoice = (InvoiceTableVM)InvoiceTable.SelectedItem;
            if (invoice == null)
            {
                MessageBox.Show("Накладная не выбрана");
                return;
            }

            new InvoiceDetailsWindow(_stockmanVM, invoice.Id).Show();
        }
    }
}
