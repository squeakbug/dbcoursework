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
using ViewModel.PharmacistViewModels;

namespace SDWPFView.PharmacistViews
{
    public partial class PharmacistDefaultWindow : Window
    {
        private Window _authWindow;
        private PharmacistBaseViewModel _pharmacistViewModel;
        public PharmacistDefaultWindow(Window authWindow)
        {
            InitializeComponent();
            _authWindow = authWindow;
            _pharmacistViewModel = new PharmacistBaseViewModel();
            _pharmacistViewModel.UpdateEmployeeName();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _pharmacistViewModel.Logout();
            _authWindow.Show();
        }

        private void OpenProductBtn_Click(object sender, RoutedEventArgs e)
        {
            HideAllControls();
            ProductsControl.Visibility = Visibility.Visible;
            _pharmacistViewModel.UpdateCurrentControlName("Лекарственные средства");
        }

        private void OpenProductRequestFormerBtn_Click(object sender, RoutedEventArgs e)
        {
            HideAllControls();
            ProductRequestFormerControl.Visibility = Visibility.Visible;
            _pharmacistViewModel.UpdateCurrentControlName("Оформление запроса на ЛС");
        }

        private void OpenMarkedProductBtn_Click(object sender, RoutedEventArgs e)
        {
            HideAllControls();
            MarkedProductControl.Visibility = Visibility.Visible;
            _pharmacistViewModel.UpdateCurrentControlName("Хранимые ЛС");
        }

        private void OpenReceiptFormerBtn_Click(object sender, RoutedEventArgs e)
        {
            HideAllControls();
            ReceiptFormerControl.Visibility = Visibility.Visible;
            _pharmacistViewModel.UpdateCurrentControlName("Оформление чека");
        }
        private void HideAllControls()
        {
            DefaultControl.Visibility = Visibility.Hidden;
            MarkedProductControl.Visibility = Visibility.Hidden;
            ProductRequestFormerControl.Visibility = Visibility.Hidden;
            ProductsControl.Visibility = Visibility.Hidden;
            ReceiptFormerControl.Visibility = Visibility.Hidden;
        }
    }
}
