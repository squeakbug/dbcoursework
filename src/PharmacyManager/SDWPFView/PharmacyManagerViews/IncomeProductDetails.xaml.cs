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
    public partial class IncomeProductDetails : Window
    {
        private PharmacyManagerInvoicesViewModel _pharmacyManagerVM;
        private string _incomeProductId;

        public IncomeProductDetails(PharmacyManagerInvoicesViewModel pharmacyManagerVM, string incomeProductId)
        {
            InitializeComponent();
            _pharmacyManagerVM = pharmacyManagerVM;
            _incomeProductId = incomeProductId;

            IncomeProductDetailsVM incomeProductDetailsVM =
                _pharmacyManagerVM.GetIncomeProductDetailsById(incomeProductId);
            CountTextBox.Text = incomeProductDetailsVM.Count;
            MeasureUnitTextBox.Text = incomeProductDetailsVM.MeasureUnit;
            ProductionDateTextBox.Text = incomeProductDetailsVM.ProductionDate;
            ProductNameTextBox.Text = incomeProductDetailsVM.ProductName;
            SeriesTextBox.Text = incomeProductDetailsVM.Series;
            StorageLocationTextBox.Text = incomeProductDetailsVM.StorageLocation;
        }

        private void ShowProductDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            IncomeProductVM incomeProductVM = _pharmacyManagerVM.GetIncomeProductById(_incomeProductId);
            new ProductDetailsWindow(_pharmacyManagerVM, incomeProductVM.Product_id).Show();
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
