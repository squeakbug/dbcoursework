using System;
using System.Collections.Generic;
using System.Text;

using ViewModel;
using ViewModel.Entities;

namespace TechnologicalView
{
    internal class RootView : BaseView
    {
        private AuthView _authView;
        private RootViewModel _rootVM = new RootViewModel();
        public RootView(AuthView authView)
        {
            _authView = authView;
        }

        public void Run()
        {
            WriteWelcomeMessage("Добро пожаловать!");
            bool isShouldClose = false;
            while (!isShouldClose)
            {
                PrintMenu();
                string choise = Console.ReadLine();
                switch (choise)
                {
                    case "1":
                        RegistrateUser();
                        break;
                    case "q":
                        isShouldClose = true;
                        break;
                    default:
                        WriteWarningMessage("Выбран несуществующий пункт меню");
                        break;
                }
            }
            _authView.Run();
        }

        private string GetAppointment()
        {
            Console.WriteLine("Выберите должность*: ");
            int menuNum = 1;
            var appointments = new List<string>(_rootVM.Appointments);
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

        private void RegistrateUser()
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
                _rootVM.Registrate(employeeVM);
            }
            catch (ApplicationException ex)
            {
                WriteWarningMessage(ex.Message);
                return;
            }
            WriteInfoMessage("Сотрудник зарегистрирован");
        }

        private void PrintMenu()
        {
            string menu = "Выберите один из пунктов: \n"
                + "\t1) Регистрация сотрудника\n"
                + "\tq) Выход\n";
            Console.WriteLine(menu);
        }
    }
}
