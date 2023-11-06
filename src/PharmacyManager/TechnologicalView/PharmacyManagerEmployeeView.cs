using System;
using System.Collections.Generic;
using System.Text;

using ViewModel;
using ViewModel.Entities;

namespace TechnologicalView
{
    internal class PharmacyManagerEmployeeView : BaseView
    {
        private PharmacyManagerViewModel _pharmacyManagerVM;
        public PharmacyManagerEmployeeView(PharmacyManagerViewModel pharmacyManagerVM)
        {
            _pharmacyManagerVM = pharmacyManagerVM;
            _pharmacyManagerVM.LoadEmployees();
        }

        public void Run()
        {
            bool isShouldClose = false;
            while (!isShouldClose)
            {
                PrintMenu();
                string choise = Console.ReadLine();
                switch (choise)
                {
                    case "1":
                        RegistrateEmployee();
                        break;
                    case "2":
                        FireEmployee();
                        break;
                    case "3":
                        ShowEmployees();
                        break;
                    case "q":
                        isShouldClose = true;
                        break;
                    default:
                        WriteWarningMessage("Выбран несуществующий пункт меню");
                        break;
                }
            }
        }

        private void ShowEmployees()
        {
            var employees = _pharmacyManagerVM.Employees;
            WriteTableHeader("Id", "Фамилия", "Имя", "Отчество", "Должность");
            foreach (var item in employees)
            {
                PersonMetadataVM metadataVM = _pharmacyManagerVM.GetPersonMetadataById(item.person_metadata_id);
                WriteTableRow(item.id.ToString(), metadataVM.Middle_name, metadataVM.First_name, 
                    metadataVM.Last_name, item.appointment);
            }
            WriteTableFoot("Id", "Фамилия", "Имя", "Отчество", "Должность");
        }

        private string GetAppointment()
        {
            Console.WriteLine("Выберите должность*: ");
            int menuNum = 1;
            var appointments = new List<string>(_pharmacyManagerVM.Appointments);
            foreach (var item in appointments)
            {
                Console.WriteLine($"{menuNum}) {item}");
                menuNum++;
            }
            string choice = Console.ReadLine();
            int choiceNum = 0;
            if (int.TryParse(choice, out choiceNum) == false)
                return null;
            if (choiceNum > appointments.Count)
                return null;
            return appointments[choiceNum - 1];
        }

        private void RegistrateEmployee()
        {
            var metadataVM = new PersonMetadataVM();
            var employeeVM = new RegistrateEmployeeVM();

            Console.Write("Имя*: ");
            metadataVM.First_name = Console.ReadLine();
            Console.Write("Фамилия*: ");
            metadataVM.Middle_name = Console.ReadLine();
            Console.Write("Отчество*: ");
            metadataVM.Last_name = Console.ReadLine();
            if (metadataVM.Last_name.Length == 0)
                metadataVM.Last_name = null;
            Console.Write("Электронная почта: ");
            metadataVM.Email = Console.ReadLine();
            if (metadataVM.Email.Length == 0)
                metadataVM.Email = null;
            Console.Write("Номер телефона*: ");
            metadataVM.Phone = Console.ReadLine();
            employeeVM.PersonMetadata = metadataVM;
            string appointment = GetAppointment();
            while (appointment == null)
            {
                WriteWarningMessage("Выбран несуществующий пункт меню");
                appointment = GetAppointment();
            }
            employeeVM.Appointment = appointment;
            Console.Write("Логин*: ");
            employeeVM.Login = Console.ReadLine();
            Console.Write("Пароль*: ");
            employeeVM.Passwd = Console.ReadLine();
            Console.Write("Повтор пароля*: ");
            employeeVM.RepeatPasswd = Console.ReadLine();
            try
            {
                _pharmacyManagerVM.Registrate(employeeVM);
            }
            catch (ApplicationException ex)
            {
                WriteWarningMessage(ex.Message);
                return;
            }
            WriteInfoMessage("Сотрудник зарегистрирован");
        }

        private void FireEmployee()
        {
            Console.WriteLine("Введите id сотрудника");
            string employeeId = Console.ReadLine();
            var employeeVM = new FireEmployeeVM { Id = employeeId };
            try
            {
                _pharmacyManagerVM.Fire(employeeVM);
            }
            catch (ApplicationException ex)
            {
                WriteWarningMessage(ex.Message);
            }
        }

        private void PrintMenu()
        {
            string menu = "Выбирите один из пунктов: \n"
                + "\t1) Создать учетную запись\n"
                + "\t2) Удалить учетную запись\n"
                + "\t3) Персонал\n"
                + "\tq) Назад\n";
            Console.WriteLine(menu);
        }
    }
}
