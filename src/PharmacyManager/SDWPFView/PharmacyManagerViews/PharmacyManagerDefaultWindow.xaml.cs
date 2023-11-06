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

using SDWPFView;
using ViewModel;
using ViewModel.Entities;
using ViewModel.PharmacyManagerViewModels;

namespace SDWPFView.PharmacyManagerViews
{
    public partial class PharmacyManagerDefaultWindow : Window
    {
        private Window _authWindow;
        private PharmacyManagerBaseViewModel _pharmacyManagerVM;
        public PharmacyManagerDefaultWindow(Window authWindow)
        {
            InitializeComponent();
            _authWindow = authWindow;
            _pharmacyManagerVM = new PharmacyManagerBaseViewModel();
            _pharmacyManagerVM.UpdateEmployeeName();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OpenEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            HideAllControls();
            EmployeeControl.Visibility = Visibility.Visible;
            _pharmacyManagerVM.UpdateCurrentControlName("Персонал");
        }

        private void OpenProductRequestBtn_Click(object sender, RoutedEventArgs e)
        {
            HideAllControls();
            ProductRequestControl.Visibility = Visibility.Visible;
            _pharmacyManagerVM.UpdateCurrentControlName("Запросы на ЛС");
        }

        private void OpenInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            HideAllControls();
            InvoiceControl.Visibility = Visibility.Visible;
            _pharmacyManagerVM.UpdateCurrentControlName("Накладные");
        }

        private void OpenWriteOffProductBtn_Click(object sender, RoutedEventArgs e)
        {
            HideAllControls();
            WriteOffProductsControl.Visibility = Visibility.Visible;
            _pharmacyManagerVM.UpdateCurrentControlName("Списанные ЛС");
        }

        private void OpenProductBtn_Click(object sender, RoutedEventArgs e)
        {
            HideAllControls();
            ProductsControl.Visibility = Visibility.Visible;
            _pharmacyManagerVM.UpdateCurrentControlName("Лекарственные средства");
        }

        private void OpenMarkedProductBtn_Click(object sender, RoutedEventArgs e)
        {
            HideAllControls();
            MarkedProductsControl.Visibility = Visibility.Visible;
            _pharmacyManagerVM.UpdateCurrentControlName("Хранимые ЛС");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _pharmacyManagerVM.Logout();
            _authWindow.Show();
        }

        private void HideAllControls()
        {
            DefaultControl.Visibility = Visibility.Hidden;
            EmployeeControl.Visibility = Visibility.Hidden;
            InvoiceControl.Visibility = Visibility.Hidden;
            MarkedProductsControl.Visibility = Visibility.Hidden;
            ProductRequestControl.Visibility = Visibility.Hidden;
            ProductRequestControl.Visibility = Visibility.Hidden;
            ProductsControl.Visibility = Visibility.Hidden;
            WriteOffProductsControl.Visibility = Visibility.Hidden;
        }
    }
}
