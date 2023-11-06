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
using ViewModel.PharmacyManagerViewModels;

namespace SDWPFView.PharmacyManagerViews
{
    public partial class ManufacturersListWindow : Window
    {
        private PharmacyManagerProductsViewModel _pharmacyManagerVM;

        public ManufacturersListWindow(PharmacyManagerProductsViewModel pharmacyManagerVM)
        {
            InitializeComponent();
            _pharmacyManagerVM = pharmacyManagerVM;
            ManufacturerTable.DataContext = _pharmacyManagerVM.Manufacturers;
        }

        private void ChooseBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedManufacturer = (Manufacturer)ManufacturerTable.SelectedItem;
            if (selectedManufacturer == null)
            {
                MessageBox.Show("Производитель на выбран");
                return;
            }

            _pharmacyManagerVM.SelectedManufacturerId = selectedManufacturer.Id.ToString();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
