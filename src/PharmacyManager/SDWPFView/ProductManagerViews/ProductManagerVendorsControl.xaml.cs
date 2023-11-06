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
using ViewModel.ProductManagerViewModels;

namespace SDWPFView.ProductManagerViews
{
    public partial class ProductManagerVendorsControl : UserControl
    {
        private ProductManagerVendorsViewModel _productManagerVM;

        public ProductManagerVendorsControl()
        {
            InitializeComponent();
            _productManagerVM = new ProductManagerVendorsViewModel();
            VendorTable.ItemsSource = _productManagerVM.Vendors;
        }

        private void AddVendorBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddVendorWindow(_productManagerVM).Show();
        }

        private void EditVendorBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedVendor = (Vendor)VendorTable.SelectedItem;
            if (selectedVendor == null)
            {
                MessageBox.Show("Поставщик не выбран");
                return;
            }

            new EditVendorWindow(_productManagerVM, selectedVendor.Id.ToString()).Show();
        }
    }
}
