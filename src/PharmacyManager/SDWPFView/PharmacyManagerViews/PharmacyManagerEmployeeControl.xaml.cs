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

using BusinessSpecification.Entities;
using ViewModel;
using ViewModel.Entities;
using ViewModel.Entities.PharmacyManagerEntities;
using ViewModel.Entities.PharmacyManagerEntities.TableEntities;
using ViewModel.PharmacyManagerViewModels;

namespace SDWPFView.PharmacyManagerViews
{
    public partial class PharmacyManagerEmployeeControl : UserControl
    {
        private PharmacyManagerEmployeeViewModel _pharmacyManagerVM;

        public PharmacyManagerEmployeeControl()
        {
            InitializeComponent();
            _pharmacyManagerVM = new PharmacyManagerEmployeeViewModel();
            EmployeesTable.ItemsSource = _pharmacyManagerVM.Employees;
        }

        private void AddEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            new RegistrateEmployeeWindow(_pharmacyManagerVM).Show();
        }

        private void ShowEmployeeInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedEmployee = (EmployeeTableVM)EmployeesTable.SelectedItem;
            if (selectedEmployee == null)
            {
                MessageBox.Show("Сотрудник не выбран");
                return;
            }

            new EmployeeDetailsWindow(_pharmacyManagerVM, selectedEmployee.id.ToString()).Show();
        }
    }
}
