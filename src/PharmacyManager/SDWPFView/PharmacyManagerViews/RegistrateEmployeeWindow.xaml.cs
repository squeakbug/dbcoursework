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
using ViewModel.PharmacyManagerViewModels;

namespace SDWPFView.PharmacyManagerViews
{
    public partial class RegistrateEmployeeWindow : Window
    {
        private PharmacyManagerEmployeeViewModel _pharmacyManagerVM;

        public RegistrateEmployeeWindow(PharmacyManagerEmployeeViewModel pharmacyManagerVM)
        {
            InitializeComponent();
            _pharmacyManagerVM = pharmacyManagerVM;
            AppointmentComboBox.ItemsSource = _pharmacyManagerVM.Appointments;
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            var appointment = (string)AppointmentComboBox.SelectedItem;
            if (appointment == null)
            {
                MessageBox.Show("Должность не выбрана");
                return;
            }

            var employeeVM = new RegistrateEmployeeVM
            {
                Appointment = appointment,
                Login = LoginTextBox.Text,
                Passwd = PasswordBox.Password,
                RepeatPasswd = RepeatPasswordBox.Password,
                PersonMetadata = new PersonMetadataVM
                {
                    Email = EmailTextBox.Text,
                    First_name = FirstNameTextBox.Text,
                    Last_name = LastNameTextBox.Text,
                    Middle_name = MiddleNameTextBox.Text,
                    Phone = PhoneTextBox.Text
                }
            };
            try
            {
                _pharmacyManagerVM.Registrate(employeeVM);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                _pharmacyManagerVM.UpdateStatus("Ошибка при создании учетной записи");
                return;
            }
            _pharmacyManagerVM.UpdateStatus("Учетная запись создана");
            Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearAllForms();
            Close();
        }

        private void ClearAllForms()
        {
            FirstNameTextBox.Clear();
            MiddleNameTextBox.Clear();
            LastNameTextBox.Clear();
            EmailTextBox.Clear();
            PhoneTextBox.Clear();
            LoginTextBox.Clear();
            PasswordBox.Clear();
            RepeatPasswordBox.Clear();
        }
    }
}
