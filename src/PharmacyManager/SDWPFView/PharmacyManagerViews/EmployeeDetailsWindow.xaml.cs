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
using BusinessSpecification.Entities;
using ViewModel.PharmacyManagerViewModels;

namespace SDWPFView.PharmacyManagerViews
{
    public partial class EmployeeDetailsWindow : Window
    {
        private PharmacyManagerEmployeeViewModel _pharmacyManagerVM;
        private string _employeeId;

        public EmployeeDetailsWindow(PharmacyManagerEmployeeViewModel pharmacyManagerVM, string employeeId)
        {
            InitializeComponent();
            _pharmacyManagerVM = pharmacyManagerVM;
            _employeeId = employeeId;

            EmployeeVM employee = _pharmacyManagerVM.GetEmployeeById(_employeeId);
            FirstNameTextBox.Text = employee.PersonMetadata.First_name;
            MiddleNameTextBox.Text = employee.PersonMetadata.Middle_name;
            LastNameTextBox.Text = employee.PersonMetadata.Last_name;
            EmailTextBox.Text = employee.PersonMetadata.Email;
            PhoneNumberTextBox.Text = employee.PersonMetadata.Phone;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            EmployeeVM employee = _pharmacyManagerVM.GetEmployeeById(_employeeId);

            employee.PersonMetadata.First_name = FirstNameTextBox.Text;
            employee.PersonMetadata.Middle_name = MiddleNameTextBox.Text;
            employee.PersonMetadata.Last_name = LastNameTextBox.Text;
            employee.PersonMetadata.Email = EmailTextBox.Text;
            employee.PersonMetadata.Phone = PhoneNumberTextBox.Text;

            try
            {
                _pharmacyManagerVM.UpdateEmployee(employee);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            _pharmacyManagerVM.UpdateStatus("Информация о работнике обновлена");
            Close();
        }
    }
}
