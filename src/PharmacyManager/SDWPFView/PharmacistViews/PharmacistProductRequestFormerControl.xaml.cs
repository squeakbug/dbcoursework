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
using ViewModel.PharmacistViewModels;

namespace SDWPFView.PharmacistViews
{
    public partial class PharmacistProductRequestFormerControl : UserControl
    {
        private PharmacistProductRequestFormerViewModel _pharmacistVM;
        public PharmacistProductRequestFormerControl()
        {
            InitializeComponent();
            _pharmacistVM = new PharmacistProductRequestFormerViewModel();
        }

        private void ConfirmRequestBtn_Click(object sender, RoutedEventArgs e)
        {
            var productRequest = new AddProductRequestVM();

            productRequest.Approximate_name = ApproxNameTextBox.Text;
            productRequest.ConsumerFirstName = FirstNameTextBox.Text;
            productRequest.ConsumerMiddleName = MiddleNameTextBox.Text;
            productRequest.ConsumerLastName = LastNameTextBox.Text;
            productRequest.Email = EmailTextBox.Text;
            productRequest.PhoneNumber = PhoneTextBox.Text;
            productRequest.Pr_count = CountTextBox.Text;

            try
            {
                _pharmacistVM.ConfirmProductRequest(productRequest);
                _pharmacistVM.SelectedProductToRequestId = null;
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                _pharmacistVM.UpdateStatus("Ошибка при выборе лекарственного средства");
                return;
            }
            _pharmacistVM.UpdateStatus("Запрос сформирован");
            MessageBox.Show("Запрос сформирован");
        }

        private void ChooseProductBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddProductToProductRequestWindow(_pharmacistVM).Show();
        }
    }
}
