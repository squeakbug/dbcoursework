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
using ViewModel.ProductManagerViewModels;

namespace SDWPFView
{
    public partial class EditVendorWindow : Window
    {
        private ProductManagerVendorsViewModel _productManagerVM;
        private string _vendorId;

        public EditVendorWindow(ProductManagerVendorsViewModel productManagerVM, string vendorId)
        {
            InitializeComponent();
            _productManagerVM = productManagerVM;
            _vendorId = vendorId;

            VendorVM vendor = _productManagerVM.GetVendorById(vendorId);
            ShortNameTextBox.Text = vendor.ShortName;
            FullNameTextBox.Text = vendor.FullName;
            INNTextBox.Text = vendor.Inn;
            KPPTextBox.Text = vendor.Kpp;
            PhoneTextBox.Text = vendor.Phone;
        }

        private void UpdateVendorBtn_Click(object sender, RoutedEventArgs e)
        {
            VendorVM vendorVM = _productManagerVM.GetVendorById(_vendorId);

            vendorVM.ShortName = ShortNameTextBox.Text;
            vendorVM.FullName = FullNameTextBox.Text;
            vendorVM.Phone = PhoneTextBox.Text;
            vendorVM.Inn = INNTextBox.Text;
            vendorVM.Kpp = KPPTextBox.Text;

            try
            {
                _productManagerVM.UpdateVendor(vendorVM);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                _productManagerVM.UpdateStatus("Ошибка при обновлении данных поставщика");
                return;
            }
            _productManagerVM.UpdateStatus("Данные о поставщике обновлены");
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
