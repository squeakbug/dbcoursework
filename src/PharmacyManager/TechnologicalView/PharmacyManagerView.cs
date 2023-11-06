using System;
using System.Collections.Generic;
using System.Text;

using ViewModel;
using ViewModel.Entities;

namespace TechnologicalView
{
    internal class PharmacyManagerView : BaseView
    {
        private AuthView _authView;
        private PharmacyManagerViewModel _pharmacyManagerVM;
        public PharmacyManagerView(AuthView authView)
        {
            _authView = authView;
            _pharmacyManagerVM = new PharmacyManagerViewModel();
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
                        RunStoredProductsView();
                        break;
                    case "2":
                        RunProductsView();
                        break;
                    case "3":
                        RunWriteOffProductsView();
                        break;
                    case "4":
                        RunInvoicesView();
                        break;
                    case "5":
                        RunProductRequestsView();
                        break;
                    case "6":
                        RunEmployeesView();
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

        private void RunStoredProductsView()
        {
            WriteNotImplementedMessage();
        }

        private void RunProductsView()
        {
            WriteNotImplementedMessage();
        }

        private void RunWriteOffProductsView()
        {
            WriteNotImplementedMessage();
        }

        private void RunInvoicesView()
        {
            new PharmacyManagerInvoicesView(_pharmacyManagerVM).Run();
        }

        private void RunProductRequestsView()
        {
            WriteNotImplementedMessage();
        }

        private void RunEmployeesView()
        {
            new PharmacyManagerEmployeeView(_pharmacyManagerVM).Run();
        }

        private void PrintMenu()
        {
            string menu = "Выбирите один из пунктов: \n"
                + "\t1) Хранимые ЛС\n"
                + "\t2) Лекарственные средства\n"
                + "\t3) Списанные ЛС\n"
                + "\t4) Накладные\n"
                + "\t5) Запросы на ЛС\n"
                + "\t6) Персонал\n"
                + "\tq) Выход\n";
            Console.WriteLine(menu);
        }
    }
}
