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
using ViewModel.Entities.PharmacyManagerEntities;
using ViewModel.PharmacyManagerViewModels;

namespace SDWPFView.PharmacyManagerViews
{
    public partial class ProductDetailsWindow : Window
    {
        public ProductDetailsWindow(PharmacyManagerBaseViewModel pharmacyManagerVM, string productId)
        {
            InitializeComponent();

            ProductDetailsVM productDetails = pharmacyManagerVM.GetProductDetailsVMById(productId);
            NameTextBox.Text = productDetails.P_name;
            InternationalNameTextBox.Text = productDetails.International_name;
            GTINTextBox.Text = productDetails.Gtin;
            ArticulTextBox.Text = productDetails.Articul;
            PrimacyPackagingTextBox.Text = productDetails.Primacy_packaging;
            TrademarkTextBox.Text = productDetails.Trademark;
            Threashold_countTextBox.Text = productDetails.Threashold_count;
            DosageTextBox.Text = productDetails.Dosage;
            ManufacturerTextBox.Text = productDetails.Manufacturer;
            StorageTemperatureTextBox.Text = productDetails.Storage_temperature;
            MaximumShelfLifeTextBox.Text = productDetails.Maximum_shelf_life;
            LeaveConditionsTextBox.Text = productDetails.Leave_condition;
            CountInPackageTextBox.Text = productDetails.Count_in_package;
            DosageFormTextBox.Text = productDetails.Dosage_form;
            MaximumMarkupTextBox.Text = productDetails.Maximum_markup;
            DefaultMarkupTextBox.Text = productDetails.Default_markup;
            InstructionTextBox.Text = productDetails.Instruction;
            DescriptionTextBox.Text = productDetails.P_description;
        }
    }
}
