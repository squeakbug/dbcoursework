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
using ViewModel.RootViewModels;

namespace SDWPFView
{
    public partial class RootRegistrateEmployeeControl : UserControl
    {
        private RootViewModel _rootViewModel;

        public RootRegistrateEmployeeControl()
        {
            InitializeComponent();
            _rootViewModel = new RootViewModel();
            AppointmentComboBox.ItemsSource = _rootViewModel.Appointments;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            var metadata = new PersonMetadataVM
            {
                First_name = FirstNameTextBox.Text,
                Middle_name = MiddleNameTextBox.Text,
                Last_name = LastNameTextBox.Text,
                Phone = PhoneTextBox.Text,
                Email = EmailTextBox.Text
            };

            var employee = new RegistrateEmployeeVM
            {
                Appointment = AppointmentComboBox.Text,
                Login = LoginTextBox.Text,
                Passwd = PasswordBox.Password,
                RepeatPasswd = RepeatPasswordBox.Password,
                PersonMetadata = metadata
            };

            try
            {
                _rootViewModel.Registrate(employee);
            }
            catch (ApplicationException ex)
            {
                _rootViewModel.UpdateStatus("Ошибка при регистрации");
                MessageBox.Show(ex.Message);
                return;
            }
            _rootViewModel.UpdateStatus("Регистрация успешна");
        }
    }
}
