using System;
using System.ComponentModel;
using System.Collections.ObjectModel;

using BusinessSpecification.Entities;
using DefaultBusinessLogic;
using ViewModel.Entities;

namespace ViewModel.RootViewModels
{
    public class RootViewModel : BaseNotify
    {
        private CommonStatusBarViewModel _commonStatusBarViewModel = new CommonStatusBarViewModel();
        private IRoot RootModel { get; }
        public ObservableCollection<string> Appointments { get; set; }

        public RootViewModel()
        {
            RootModel = Facade.Root;
            Appointments = new ObservableCollection<string>(Facade.AuthManager.GetAppointments());
        }

        public void Registrate(RegistrateEmployeeVM employeeVM)
        {
            var employeeBL = new Employee
            {
                appointment = employeeVM.Appointment,
                e_login = employeeVM.Login,
                p_hash = employeeVM.Passwd
            };
            var metadataVM = employeeVM.PersonMetadata;
            var metadataBL = new PersonMetadata
            {
                Email = metadataVM.Email,
                First_name = metadataVM.First_name,
                Last_name = metadataVM.Last_name,
                Middle_name = metadataVM.Middle_name,
                Phone = metadataVM.Phone
            };
            RootModel.Registrate(employeeBL,metadataBL, employeeVM.Passwd, employeeVM.RepeatPasswd);
        }

        public void UpdateCurrentControlName(string controlName)
        {
            _commonStatusBarViewModel.CurrentControlName = controlName;
        }

        public void UpdateStatus(string newStatus)
        {
            _commonStatusBarViewModel.StatusString = newStatus;
        }

        public void UpdateEmployeeName()
        {
            PersonMetadata metadata = RootModel.GetInnerEmployeeMetadata();
            _commonStatusBarViewModel.EmployeeName =
                $"{metadata.Middle_name} {metadata.First_name} {metadata.Last_name}";
        }

        public void Logout()
        {
            Facade.Logout();
        }
    }
}
