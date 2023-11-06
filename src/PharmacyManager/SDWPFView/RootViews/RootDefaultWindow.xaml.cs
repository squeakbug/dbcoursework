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
using ViewModel.RootViewModels;

namespace SDWPFView
{
    public partial class RootDefaultWindow : Window
    {
        private Window _authWindow;
        private RootViewModel _rootVM;
        public RootDefaultWindow(Window authWindow)
        {
            InitializeComponent();
            _authWindow = authWindow;
            _rootVM = new RootViewModel();
            _rootVM.UpdateEmployeeName();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OpenEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            HideAllControls();
            RegistrateEmployeeControl.Visibility = Visibility.Visible;
            _rootVM.UpdateCurrentControlName("Регистрация персонала");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _rootVM.Logout();
            _authWindow.Show();
        }

        private void HideAllControls()
        {
            DefaultControl.Visibility = Visibility.Hidden;
            RegistrateEmployeeControl.Visibility = Visibility.Hidden;
        }
    }
}
