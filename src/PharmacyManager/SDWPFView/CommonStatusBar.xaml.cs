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

namespace SDWPFView
{
    public partial class CommonStatusBar : UserControl
    {
        private CommonStatusBarViewModel _commonStatusBarVM;

        public CommonStatusBar()
        {
            InitializeComponent();
            _commonStatusBarVM = new CommonStatusBarViewModel();
            CurrentControlNameLabel.DataContext = _commonStatusBarVM.CurrentControlName;
            CurrentEmployeeNameLabel.DataContext = _commonStatusBarVM.EmployeeName;
            LastOperationStatusLabel.DataContext = _commonStatusBarVM.StatusString;
        }

        public void SetStatus(string status)
        {
            LastOperationStatusLabel.Content = status;
        }

        public void SetEmployeeName(string name)
        {
            CurrentEmployeeNameLabel.Content = name;
        }

        public void SetCurrentControlName(string name)
        {
            CurrentControlNameLabel.Content = name;
        }
    }
}
