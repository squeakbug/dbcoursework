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
    public partial class ProductRequestDetailsWindow : Window
    {
        private PharmacyManagerProductRequestViewModel _pharmacyManagerVM;
        private string _productRequestId;

        public ProductRequestDetailsWindow(PharmacyManagerProductRequestViewModel pharmacyManagerVM,
            string productRequestId)
        {
            InitializeComponent();
            _pharmacyManagerVM = pharmacyManagerVM;
            _productRequestId = productRequestId;

            ProductRequestDetailsVM productRequest = 
                _pharmacyManagerVM.GetProductRequestDetailsById(productRequestId);
            ApprovedDateTextBox.Text = productRequest.Approved_date;
            ApprovedTextBox.Text = productRequest.Approved;
            ConsumerNameTextBox.Text = productRequest.ConsumerMiddleName + " " + productRequest.ConsumerFirstName + " "
                + productRequest.ConsumerLastName;
            CountTextBox.Text = productRequest.Count;
            ProductNameTextBox.Text = productRequest.ProductName;
            RequestDateTextBox.Text = productRequest.RequestDate;
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
