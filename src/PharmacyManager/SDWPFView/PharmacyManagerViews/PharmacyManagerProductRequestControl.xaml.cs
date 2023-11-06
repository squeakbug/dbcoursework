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

using ViewModel;
using ViewModel.Entities;
using ViewModel.Entities.PharmacyManagerEntities;
using ViewModel.Entities.PharmacyManagerEntities.TableEntities;
using ViewModel.PharmacyManagerViewModels;

namespace SDWPFView.PharmacyManagerViews
{
    public partial class PharmacyManagerProductRequestControl : UserControl
    {
        private PharmacyManagerProductRequestViewModel _pharmacyManagerVM;

        public PharmacyManagerProductRequestControl()
        {
            InitializeComponent();
            _pharmacyManagerVM = new PharmacyManagerProductRequestViewModel();
            ProductRequestsTable.ItemsSource = _pharmacyManagerVM.ProductRequests;
        }

        private void ShowProductRequestDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            var productRequest = (ProductRequestTableVM)ProductRequestsTable.SelectedItem;
            if (productRequest == null)
            {
                MessageBox.Show("Запрос не выбран");
                return;
            }

            new ProductRequestDetailsWindow(_pharmacyManagerVM, productRequest.Id).Show();
        }

        private void ApproveProductRequestBtn_Click(object sender, RoutedEventArgs e)
        {
            var productRequest = (ProductRequestTableVM)ProductRequestsTable.SelectedItem;
            if (productRequest == null)
            {
                MessageBox.Show("Запрос не выбран");
                return;
            }

            try
            {
                _pharmacyManagerVM.ApproveProductRequest(productRequest.Id);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Заявка подтвержена");
        }
    }
}
