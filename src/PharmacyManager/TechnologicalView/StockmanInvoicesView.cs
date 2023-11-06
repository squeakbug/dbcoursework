using System;
using System.Collections.Generic;
using System.Text;

using ViewModel;
using ViewModel.Entities;
using ViewModel.Converters;
using BusinessSpecification.Entities;

namespace TechnologicalView
{
    internal class StockmanInvoicesView : BaseView
    {
        private StockmanViewModel _stockmanVM;
        public StockmanInvoicesView(StockmanViewModel stockmanVM)
        {
            _stockmanVM = stockmanVM;
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
                        AcceptInvoice();
                        break;
                    case "2":
                        ShowInvoices();
                        break;
                    case "3":
                        ShowInvoiceProducts();
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

        private void ShowInvoices()
        {
            var invoices = _stockmanVM.Invoices;
            WriteTableHeader("Id", "Номер", "Поставщик", "Время создания", "Состояние");
            foreach (var item in invoices)
            {
                var vendorBL = _stockmanVM.GetVendorById(item.vendor_id);
                WriteTableRow(item.Id.ToString(), item.invoice_number.ToString(), vendorBL.ShortName,
                    item.invoice_date.ToString(), item.i_state);
            }
            WriteTableFoot("Id", "Номер", "Поставщик", "Время создания", "Состояние");
        }

        void ShowInvoiceProducts()
        {
            Console.WriteLine("Введите Id накладной");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int invoiceId))
            {
                WriteWarningMessage("Ошибка ввода");
                return;
            }

            var invoices = new List<Invoice>(_stockmanVM.Invoices);
            var selectedInvoice = invoices.Find(x => x.Id == invoiceId);
            if (selectedInvoice == null)
            {
                WriteWarningMessage("Выбрана несуществующая запись");
                return;
            }
            try
            {
                _stockmanVM.LoadIncomeProducts(selectedInvoice.Id);
            }
            catch (ApplicationException ex)
            {
                WriteWarningMessage(ex.Message);
                return;
            }
            WriteTableHeader("Id", "Название", "Количество", "Серия", "Дата производства");
            foreach (var item in _stockmanVM.InvoiceIncomeProducts)
            {
                var productBL = _stockmanVM.GetProductById(item.Product_id);
                WriteTableRow(item.Id.ToString(), productBL.P_name, item.P_count.ToString(),
                    item.Series.ToString(), item.Production_date.ToString());
            }
            WriteTableFoot("Id", "Название", "Количество", "Серия", "Дата производства");
        }

        private void AcceptInvoice()
        {
            Console.WriteLine("Введите id накладной: ");
            string invoiceId = Console.ReadLine();
            try
            {
                _stockmanVM.AcceptInvoice(invoiceId);
            }
            catch (ApplicationException ex)
            {
                WriteWarningMessage(ex.Message);
                return;
            }
            Console.WriteLine("Приход товара подтвержден");
        }


        private void PrintMenu()
        {
            string menu = "Выбирите один из пунктов: \n"
                + "\t1) Подтвердить приход\n"
                + "\t2) Накладные\n"
                + "\t3) Товары по накладной\n"
                + "\tq) Назад\n";
            Console.WriteLine(menu);
        }
    }
}
