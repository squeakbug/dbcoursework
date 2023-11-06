using System;
using System.Collections.Generic;
using System.Text;

using ViewModel;
using ViewModel.Entities;
using BusinessSpecification.Entities;

namespace TechnologicalView
{
    internal class StockmanView : BaseView
    {
        private AuthView _authView;
        private StockmanViewModel _stockmanVM = new StockmanViewModel();
        public StockmanView(AuthView authView)
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
                        RunInvoicesView();
                        break;
                    case "2":
                        ShowStoredProducts();
                        break;
                    case "3":
                        ShowWriteOffProducts();
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

        private void RunInvoicesView()
        {
            new StockmanInvoicesView(_stockmanVM).Run();
        }

        void ShowStoredProducts()
        {
            var markedProducts = _stockmanVM.MarkedProducts;
            WriteTableHeader("Id", "Название", "Количество", "Место хранения");
            foreach (var item in markedProducts)
            {
                var markInfoVM = _stockmanVM.GetMarkInfoById(item.Mark_info_id);
                var productVM = _stockmanVM.GetProductByMarkInfotId(item.Mark_info_id);
                WriteTableRow(item.Id.ToString(), productVM.P_name, markInfoVM.P_count, markInfoVM.Storage_location);
            }
            WriteTableFoot("Id", "Номер", "Поставщик", "Время создания", "Состояние");
        }

        void ShowWriteOffProducts()
        {
            var writeOffProducts = _stockmanVM.WriteOffProducts;
            WriteTableHeader("Id", "Название", "Дата списания", "Причина списания");
            foreach (var item in writeOffProducts)
            {
                var productVM = _stockmanVM.GetProductByMarkInfotId(item.Mark_info_id);
                WriteTableRow(item.Id.ToString(), productVM.P_name, item.Write_off_date.ToString(), item.Reason);
            }
            WriteTableFoot("Id", "Название", "Дата списания", "Причина списания");
        }

        private void PrintMenu()
        {
            string menu = "Выбeрите один из пунктов: \n"
                + "\t1) Накладные\n"
                + "\t2) Хранимые ЛС\n"
                + "\t3) Списанные ЛС\n"
                + "\tq) Выход\n";
            Console.WriteLine(menu);
        }
    }
}
