using System;
using System.Collections.Generic;
using System.Text;

using ViewModel;
using ViewModel.Entities;

namespace TechnologicalView
{
    public class AuthView
    {
        private AuthManagerViewModel _authManagerVM;
        public AuthView()
        {
            _authManagerVM = new AuthManagerViewModel();
        }

        private AuthEmployeeVM Getpass()
        {
            Console.Write("Логин: ");
            string login = Console.ReadLine();
            Console.Write("Пароль: ");
            string passwd = Console.ReadLine();
            return _authManagerVM.Auth(login, passwd);
        }

        public void Run()
        {
            AuthEmployeeVM emplVM = null;
            while (emplVM == null)
            {
                try
                {
                    emplVM = Getpass();
                }
                catch (ApplicationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            switch (emplVM.Appointment)
            {
                case "root":
                    new RootView(this).Run();
                    break;
                case "pharmacist":
                    new PharmacistView(this).Run();
                    break;
                case "pharmacy_manager":
                    new PharmacyManagerView(this).Run();
                    break;
                case "product_manager":
                    new ProductManagerView(this).Run();
                    break;
                case "stockman":
                    new StockmanView(this).Run();
                    break;
            }
        }
    }
}
