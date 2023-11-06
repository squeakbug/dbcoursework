using System;
using System.Collections.Generic;
using System.Text;

using ViewModel;
using ViewModel.Entities;

namespace TechnologicalView
{
    class PharmacistView : BaseView
    {
        private AuthView _authView;
        private PharmacistViewModel _pharmacistVM = new PharmacistViewModel();
        public PharmacistView(AuthView authView)
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
                        MakeProductOperation();
                        break;
                    case "2":
                        ShowStoredProducts();
                        break;
                    case "3":
                        MakeProductRequest();
                        break;
                    case "4":
                        ShowProducts();
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

        private void ShowProducts()
        {
            var products = _pharmacistVM.Products;
            WriteTableHeader("Id", "Название");
            foreach (var item in products)
            {
                WriteTableRow(item.Id.ToString(), item.P_name);
            }
            WriteTableFoot("Id", "Название");
        }

        private void MakeProductRequest()
        {
            WriteNotImplementedMessage();
        }

        private void ShowStoredProducts()
        {
            WriteNotImplementedMessage();
        }

        private void MakeProductOperation()
        {
            WriteNotImplementedMessage();
        }

        private void PrintMenu()
        {
            string menu = "Выбирите один из пунктов: \n"
                + "\t1) Оформление чека\n"
                + "\t2) Хранимые ЛС\n"
                + "\t3) Оформление запроса на ЛС\n"
                + "\t4) Лекарственные средства\n"
                + "\tq) Выход\n";
            Console.WriteLine(menu);
        }
    }
}
