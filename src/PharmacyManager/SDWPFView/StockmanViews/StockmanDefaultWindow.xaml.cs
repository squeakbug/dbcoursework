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
using ViewModel.StockmanViewModels;

namespace SDWPFView.StockmanViews
{
    public partial class StockmanDefaultWindow : Window
    {
        private Window _authWindow;
        private StockmanBaseViewModel _stockmanVM;
        public StockmanDefaultWindow(Window authWindow)
        {
            InitializeComponent();
            _authWindow = authWindow;
            _stockmanVM = new StockmanBaseViewModel();
            _stockmanVM.UpdateEmployeeName();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ShowWriteOffProduct_Click(object sender, RoutedEventArgs e)
        {
            HideAllControls();
            WriteOffProductControl.Visibility = Visibility.Visible;
            _stockmanVM.UpdateCurrentControlName("Списанные ЛС");
        }

        private void ShowMarkedProduct_Click(object sender, RoutedEventArgs e)
        {
            HideAllControls();
            MarkedProductControl.Visibility = Visibility.Visible;
            _stockmanVM.UpdateCurrentControlName("Хранимые ЛС");
        }

        private void ShowInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            HideAllControls();
            InvoicesControl.Visibility = Visibility.Visible;
            _stockmanVM.UpdateCurrentControlName("Накладные");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _stockmanVM.Logout();
            _authWindow.Show();
        }

        private void HideAllControls()
        {
            DefaultControl.Visibility = Visibility.Hidden;
            WriteOffProductControl.Visibility = Visibility.Hidden;
            MarkedProductControl.Visibility = Visibility.Hidden;
            InvoicesControl.Visibility = Visibility.Hidden;
        }
    }
}
