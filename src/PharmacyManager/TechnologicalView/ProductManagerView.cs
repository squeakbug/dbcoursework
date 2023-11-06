using System;
using System.Collections.Generic;
using System.Text;

using ViewModel;
using ViewModel.Entities;

namespace TechnologicalView
{
    internal class ProductManagerView : BaseView
    {
        private AuthView _authView;
        private ProductManagerViewModel _productManagerVM;
        public ProductManagerView(AuthView authView)
        {
            _authView = authView;
            _productManagerVM = new ProductManagerViewModel();
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
                        ShowStoredProducts();
                        break;
                    case "2":
                        ShowProducts();
                        break;
                    case "3":
                        ShowVendors();
                        break;
                    case "4":
                        ShowEmployeesStat();
                        break;
                    case "5":
                        ShowProductsStat();
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

        private void ShowStoredProducts()
        {
            WriteNotImplementedMessage();
        }

        private void ShowProducts()
        {
            WriteNotImplementedMessage();
        }

        private void ShowVendors()
        {
            WriteNotImplementedMessage();
        }

        private void ShowEmployeesStat()
        {
            WriteNotImplementedMessage();
        }

        private void ShowProductsStat()
        {
            WriteNotImplementedMessage();
        }

        private void PrintMenu()
        {
            string menu = "Выбирите один из пунктов: \n"
                + "\t1) Хранимые ЛС\n"
                + "\t2) Лекарственные средства\n"
                + "\t3) Поставщики\n"
                + "\t4) Статистика сотрудников\n"
                + "\t5) Статистика продаж\n"
                + "\tq) Выход\n";
            Console.WriteLine(menu);
        }
    }
}
