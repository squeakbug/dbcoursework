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
using ViewModel.ProductManagerViewModels;

namespace SDWPFView.ProductManagerViews
{
    public partial class ProductManagerDefaultWindow : Window
    {
        private Window _authWindow;
        private ProductManagerBaseViewModel _productManagerVM;
        public ProductManagerDefaultWindow(Window authWindow)
        {
            InitializeComponent();
            _authWindow = authWindow;
            _productManagerVM = new ProductManagerBaseViewModel();
            _productManagerVM.UpdateEmployeeName();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OpenProductStatisticBtn_Click(object sender, RoutedEventArgs e)
        {
            HideAllControls();
            ProductStatisticControl.Visibility = Visibility.Visible;
            _productManagerVM.UpdateCurrentControlName("Статистика продаж");
        }

        private void OpenEmployeeStatisticBtn_Click(object sender, RoutedEventArgs e)
        {
            HideAllControls();
            EmployeeStatisticsControl.Visibility = Visibility.Visible;
            _productManagerVM.UpdateCurrentControlName("Статистика сотрудников");
        }

        private void OpenVendorBtn_Click(object sender, RoutedEventArgs e)
        {
            HideAllControls();
            VendorsControl.Visibility = Visibility.Visible;
            _productManagerVM.UpdateCurrentControlName("Поставщики");
        }

        private void OpenProductBtn_Click(object sender, RoutedEventArgs e)
        {
            HideAllControls();
            ProductsControl.Visibility = Visibility.Visible;
            _productManagerVM.UpdateCurrentControlName("Лекарственные средства");
        }

        private void OpenMarkedProductBtn_Click(object sender, RoutedEventArgs e)
        {
            HideAllControls();
            MarkedProductsControl.Visibility = Visibility.Visible;
            _productManagerVM.UpdateCurrentControlName("Хранимые ЛС");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _productManagerVM.Logout();
            _authWindow.Show();
        }

        private void HideAllControls()
        {
            DefaultControl.Visibility = Visibility.Hidden;
            EmployeeStatisticsControl.Visibility = Visibility.Hidden;
            ProductStatisticControl.Visibility = Visibility.Hidden;
            MarkedProductsControl.Visibility = Visibility.Hidden;
            ProductsControl.Visibility = Visibility.Hidden;
            VendorsControl.Visibility = Visibility.Hidden;
        }
    }
}
