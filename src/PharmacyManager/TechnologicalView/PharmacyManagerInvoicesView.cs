using System;
using System.Collections.Generic;
using System.Text;

using ViewModel;
using ViewModel.Entities;

namespace TechnologicalView
{
    internal class PharmacyManagerInvoicesView : BaseView
    {
        private PharmacyManagerViewModel _pharmacyManagerVM;
        public PharmacyManagerInvoicesView(PharmacyManagerViewModel pharmacyManagerVM)
        {
            _pharmacyManagerVM = pharmacyManagerVM;
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
                        AddInvoice();
                        break;
                    case "2":
                        ApproveInvoice();
                        break;
                    case "3":
                        ShowInvoices();
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
            var invoices = _pharmacyManagerVM.Invoices;
            WriteTableHeader("Id", "Номер", "Поставщик", "Время создания", "Состояние");
            foreach (var item in invoices)
            {
                var vendor = _pharmacyManagerVM.GetVendorById(item.vendor_id);
                WriteTableRow(item.Id.ToString(), item.invoice_number.ToString(), vendor.ShortName,
                    item.invoice_date.ToString(), item.i_state);
            }
            WriteTableFoot("Id", "Номер", "Поставщик", "Время создания", "Состояние");
        }

        private void ApproveInvoice()
        {
            Console.WriteLine("Введите id накладной:");
            string invoiceId = Console.ReadLine();
            var invoiceVM = new ApproveInvoiceVM { Id = invoiceId };
            try
            {
                _pharmacyManagerVM.ApproveInvoice(invoiceVM);
            }
            catch (ApplicationException ex)
            {
                WriteWarningMessage(ex.Message);
                return;
            }
            WriteInfoMessage("Товар поступил в продажу");
        }

        private void AddInvoice()
        {
            _pharmacyManagerVM.InitAddInvoice();
            Console.WriteLine("Введите номер накладной:");
            string invoiceNumberStr = Console.ReadLine();
            Console.WriteLine("Введите id поставщика:");
            string vendorId = Console.ReadLine();
            _pharmacyManagerVM.SelectedVendor = new InvoiceVendorVM { Id = vendorId };
            Console.WriteLine("Введите id лекарственных средств:");
            int counter = 1;
            Console.Write("{0}) ", counter);
            string productId = Console.ReadLine();
            while (productId.Length != 0)
            {
                var product = new AddProductToInvoiceVM
                {
                    Count = "1",
                    MeasureUnit = "g",
                    ProductionDate = DateTime.Now,
                    Series = "2",
                    StorageLocation = "MainStorage",
                    VendorPrice = "100",
                    VendorVax = "18"
                };
                _pharmacyManagerVM.SelectedProduct = new InvoiceProductVM { Id = productId };
                _pharmacyManagerVM.AddProductToInvoice(product);
                counter++;
                Console.Write("{0}) ", counter);
                productId = Console.ReadLine();
            }
            var invoice = new AddNewInvoiceVM
            {
                InvoiceNumber = invoiceNumberStr
            };
            try
            {
                _pharmacyManagerVM.AddInvoice(invoice);
            }
            catch (ApplicationException ex)
            {
                WriteWarningMessage(ex.Message);
                return;
            }
            WriteInfoMessage("Накладная успешно добавлена");
        }

        private void PrintMenu()
        {
            string menu = "Выбирите один из пунктов: \n"
                + "\t1) Добавить накладную\n"
                + "\t2) Подтвердить накладную\n"
                + "\t3) Накладные\n"
                + "\tq) Назад\n";
            Console.WriteLine(menu);
        }
    }
}
