using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using BusinessSpecification.Entities;
using DefaultBusinessLogic;
using ViewModel.Entities;
using ViewModel.Entities.PharmacyManagerEntities;
using ViewModel.Entities.PharmacyManagerEntities.TableEntities;
using ViewModel.Converters;

namespace ViewModel.PharmacyManagerViewModels
{
    public class PharmacyManagerEmployeeViewModel : PharmacyManagerBaseViewModel
    {
        public ObservableCollection<EmployeeTableVM> Employees { get; set; }
        public ObservableCollection<string> Appointments { get; set; }

        public PharmacyManagerEmployeeViewModel()
        {
            Employees = new ObservableCollection<EmployeeTableVM>();
            Appointments = new ObservableCollection<string>(Facade.AuthManager.GetAppointments());

            LoadEmployees();
        }

        public void LoadEmployees()
        {
            IEnumerable<Employee> employees = PharmacyManagerModel.GetEmployees();
            foreach (var item in employees)
            {
                var employee = new EmployeeTableVM
                {
                    appointment = item.appointment,
                    e_login = item.e_login,
                    id = item.id.ToString()
                };
                Employees.Add(employee);
            }
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
            PharmacyManagerModel.Registrate(employeeBL, metadataBL, employeeVM.Passwd, employeeVM.RepeatPasswd);
        }

        public void UpdateEmployee(EmployeeVM employeeVM)
        {
            if (!int.TryParse(employeeVM.Id, out int employeeIdBL))
                throw new ApplicationException("Bad employee id");

            var employeeBL = PharmacyManagerModel.GetEmployeeById(employeeIdBL);
            var personMetadataBL = PharmacyManagerModel.GetPersonMetadataById(employeeBL.person_metadata_id);

            personMetadataBL.Email = employeeVM.PersonMetadata.Email;
            personMetadataBL.First_name = employeeVM.PersonMetadata.First_name;
            personMetadataBL.Middle_name = employeeVM.PersonMetadata.Middle_name;
            personMetadataBL.Last_name = employeeVM.PersonMetadata.Last_name;
            personMetadataBL.Phone = employeeVM.PersonMetadata.Phone;

            PharmacyManagerModel.UpdateEmployee(employeeBL);
            PharmacyManagerModel.UpdatePersonMetadata(personMetadataBL);
        }

        public void Fire(FireEmployeeVM employeeVM)
        {
            if (!int.TryParse(employeeVM.Id, out int employeeId))
                throw new ApplicationException("Bad employee Id");
            PharmacyManagerModel.Fire(employeeId);
        }
    }
}
