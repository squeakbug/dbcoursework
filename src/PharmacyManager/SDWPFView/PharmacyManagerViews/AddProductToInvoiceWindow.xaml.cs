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
using BusinessSpecification.Entities;
using ViewModel.Entities.PharmacyManagerEntities;
using ViewModel.Entities.PharmacyManagerEntities.TableEntities;
using ViewModel.PharmacyManagerViewModels;

namespace SDWPFView.PharmacyManagerViews
{
    public partial class AddProductToInvoiceWindow : Window
    {
        private PharmacyManagerInvoicesViewModel _pharmacyManagerVM;
        public AddProductToInvoiceWindow(PharmacyManagerInvoicesViewModel pharmacyManagerVM)
        {
            InitializeComponent();
            _pharmacyManagerVM = pharmacyManagerVM;
            _pharmacyManagerVM.LoadProducts();
            ProductTable.ItemsSource = _pharmacyManagerVM.Products;
        }

        private void ChooseBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = (ProductTableVM)ProductTable.SelectedItem;
            if (selectedProduct == null)
            {
                MessageBox.Show("Не выбрано лекарственное средтво");
                return;
            }

            var productVM = new AddProductToInvoiceVM
            {
                Count = CountTextBox.Text,
                MeasureUnit = MeasureUnitTextBox.Text,
                ProductionDate = ProductionDateTextBox.Text,
                Series = SeriesTextBox.Text,
                StorageLocation = StorageLocationTextBox.Text,
                VendorPrice = VendorPriceTextBox.Text,
                VendorVax = VendorVaxTextBox.Text
            };
            try
            {
                _pharmacyManagerVM.SelectedProductId = selectedProduct.Id;
                _pharmacyManagerVM.AddProductToInvoice(productVM);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                _pharmacyManagerVM.UpdateStatus("Ошибка при добавлении ЛС в накладную");
                return;
            }
            _pharmacyManagerVM.UpdateStatus("ЛС добавлено в накладную");
            Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
