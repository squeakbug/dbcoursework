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
    public partial class EditProductWindow : Window
    {
        private PharmacyManagerProductsViewModel _pharmacyManagerVM;
        private readonly string _productId;

        public EditProductWindow(PharmacyManagerProductsViewModel pharmacytManagerVM, string productId)
        {
            InitializeComponent();
            _pharmacyManagerVM = pharmacytManagerVM;
            _productId = productId;

            ProductVM productVM = _pharmacyManagerVM.GetProductById(_productId);
            ManufacturerVM manufacturerVM = _pharmacyManagerVM.GetManufacturerById(productVM.ManufacturerId);

            ArticulTextBox.Text = productVM.Articul;
            CountInPackageTextBox.Text = productVM.Count_in_package;
            DefaultMarkupTextBox.Text = productVM.Default_markup;
            DescriptionTextBox.Text = productVM.P_description;
            DosageFormTextBox.Text = productVM.Dosage_form;
            GTINTextBox.Text = productVM.Gtin;
            InstructionTextBox.Text = productVM.Instruction;
            InternationalNameTextBox.Text = productVM.International_name;
            LeaveConditionsTextBox.Text = productVM.Leave_condition;
            ManufacturerNameTextBox.Text = manufacturerVM.Name;
            MaximumMarkupTextBox.Text = productVM.Maximum_markup;
            MaximumShelfLifeTextBox.Text = productVM.Maximum_shelf_life;
            PrimacyPackagingTextBox.Text = productVM.Primacy_packaging;
            DosageTextBox.Text = productVM.Dosage;
            ProductNameTextBox.Text = productVM.P_name;
            StorageTemperatureTextBox.Text = productVM.Storage_temperature;
            ThreasholdCountTextBox.Text = productVM.Threashold_count;
            TrademarkTextBox.Text = productVM.Trademark;
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            ProductVM productVM = _pharmacyManagerVM.GetProductById(_productId);
            IEnumerable<string> categories_id = _pharmacyManagerVM.GetProductCategoiesId(productVM.Id);
            ManufacturerVM manufacturerVM = _pharmacyManagerVM.GetManufacturerById(productVM.ManufacturerId);

            productVM.Articul = ArticulTextBox.Text;
            productVM.Count_in_package = CountInPackageTextBox.Text;
            productVM.Default_markup = DefaultMarkupTextBox.Text;
            productVM.CategoriesId = categories_id;
            productVM.ManufacturerId = manufacturerVM.Id;
            productVM.Dosage = DosageTextBox.Text;
            productVM.Dosage_form = DosageFormTextBox.Text;
            productVM.Gtin = GTINTextBox.Text;
            productVM.Instruction = InstructionTextBox.Text;
            productVM.International_name = InternationalNameTextBox.Text;
            productVM.Leave_condition = LeaveConditionsTextBox.Text;
            productVM.Maximum_markup = MaximumMarkupTextBox.Text;
            productVM.Maximum_shelf_life = MaximumShelfLifeTextBox.Text;
            productVM.Primacy_packaging = PrimacyPackagingTextBox.Text;
            productVM.P_description = DescriptionTextBox.Text;
            productVM.P_name = ProductNameTextBox.Text;
            productVM.Storage_temperature = StorageTemperatureTextBox.Text;
            productVM.Threashold_count = ThreasholdCountTextBox.Text;
            productVM.Trademark = TrademarkTextBox.Text;

            try
            {
                _pharmacyManagerVM.UpdateProduct(productVM);
                _pharmacyManagerVM.SelectedManufacturerId = null;
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                _pharmacyManagerVM.UpdateStatus("Ошибка при обновлении информации о ЛС");
                return;
            }
            _pharmacyManagerVM.UpdateStatus("Информация о ЛС обновлена");
            Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
