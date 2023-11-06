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
    public partial class VendorListWindow : Window
    {
        private PharmacyManagerInvoicesViewModel _pharmacyManagerVM;

        public VendorListWindow(PharmacyManagerInvoicesViewModel pharmacyManagerVM)
        {
            InitializeComponent();
            _pharmacyManagerVM = pharmacyManagerVM;
            VendorTable.ItemsSource = _pharmacyManagerVM.Vendors;
            _pharmacyManagerVM.LoadVendors();
        }

        private void ChooseBtn_Click(object sender, RoutedEventArgs e)
        {
            var vendor = (VendorTableVM)VendorTable.SelectedItem;
            if (vendor == null)
            {
                MessageBox.Show("Поставщик не выбран");
                return;
            }

            _pharmacyManagerVM.SelectedVendorId = vendor.Id.ToString();
            Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
