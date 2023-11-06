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
using DefaultBusinessLogic;
using ViewModel;
using ViewModel.Entities;
using ViewModel.ProductManagerViewModels;

namespace SDWPFView.ProductManagerViews
{
    public partial class AddVendorWindow : Window
    {
        private ProductManagerVendorsViewModel _productManagerVM;

        public AddVendorWindow(ProductManagerVendorsViewModel productManagerVM)
        {
            InitializeComponent();
            _productManagerVM = productManagerVM;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddVendorBtn_Click(object sender, RoutedEventArgs e)
        {
            var vendor = new VendorVM
            {
                ShortName = ShortNameTextBox.Text,
                FullName = FullNameTextBox.Text,
                Inn = INNTextBox.Text,
                Kpp = KPPTextBox.Text,
                Phone = PhoneTextBox.Text
            };

            try
            {
                _productManagerVM.AddVendor(vendor);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                _productManagerVM.UpdateStatus("Ошибка при добавлении поставщика");
                return;
            }
            Close();
        }
    }
}
