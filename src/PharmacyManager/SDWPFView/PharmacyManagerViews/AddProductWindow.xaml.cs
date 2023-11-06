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
using ViewModel.PharmacyManagerViewModels;

namespace SDWPFView.PharmacyManagerViews
{
    public partial class AddProductWindow : Window
    {
        private PharmacyManagerProductsViewModel _pharmacyManagerVM;

        public AddProductWindow(PharmacyManagerProductsViewModel pharmacyManagerVM)
        {
            InitializeComponent();
            _pharmacyManagerVM = pharmacyManagerVM;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            string manufacturerId = _pharmacyManagerVM.SelectedManufacturerId;

            var productVM = new ProductVM
            {
                Articul = ArticulTextBox.Text,
                ManufacturerId = manufacturerId,
                Count_in_package = CountInPackageTextBox.Text,
                Default_markup = DefaultMarkupTextBox.Text,
                Dosage = DosageTextBox.Text,
                Dosage_form = DosageFormTextBox.Text,
                Gtin = GTINTextBox.Text,
                Instruction = InstructionTextBox.Text,
                International_name = InternationalNameTextBox.Text,
                Leave_condition = LeaveConditionsTextBox.Text,
                Maximum_markup = MaximumMarkupTextBox.Text,
                Maximum_shelf_life = MaximumShelfLifeTextBox.Text,
                Primacy_packaging = PrimacyPackageTextBox.Text,
                P_description = DescriptionTextBox.Text,
                P_name = NameTextBox.Text,
                Storage_temperature = StorageTemperatureTextBox.Text,
                Threashold_count = ThreasholdCountTextBox.Text,
                Trademark = TrademarkTextBox.Text
            };
            try
            {
                _pharmacyManagerVM.AddProduct(productVM);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                _pharmacyManagerVM.UpdateStatus("Ошибка при добавлении товара");
                return;
            }
            _pharmacyManagerVM.UpdateStatus("Товар добавлен");
            Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ManufacturerChoiseBtn_Click(object sender, RoutedEventArgs e)
        {
            new ManufacturersListWindow(_pharmacyManagerVM).Show();
        }
    }
}
