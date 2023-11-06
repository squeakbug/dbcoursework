using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;

using DefaultBusinessLogic;
using BusinessSpecification.Entities;
using ViewModel.Entities;
using ViewModel.Entities.StockmanEntities;
using ViewModel.Entities.StockmanEntities.TableEntities;
using ViewModel.Converters;

namespace ViewModel.StockmanViewModels
{
    public class StockmanInvoicesViewModel : StockmanBaseViewModel
    {
        public Invoice SelectedInvoice { get; set; }
        public IncomeProductTableVM SelectedIncomeProduct {get;set; }
        public ObservableCollection<InvoiceTableVM> Invoices { get; set; }
        public ObservableCollection<IncomeProductTableVM> InvoiceIncomeProducts { get; set; }

        public StockmanInvoicesViewModel()
            : base()
        {
            Invoices = new ObservableCollection<InvoiceTableVM>();
            InvoiceIncomeProducts = new ObservableCollection<IncomeProductTableVM>();

            LoadInvoices();
        }

        public void LoadInvoices()
        {
            Invoices.Clear();
            var queryResult = StockmanModel.GetInvoices();
            foreach (var item in queryResult)
            {
                var invoiceVM = new InvoiceTableVM
                {
                    DocumentGTIN = item.document_gtin,
                    Id = item.Id.ToString(),
                    InvoiceDate = item.invoice_date.ToString(),
                    InvoiceNumber = item.invoice_number.ToString(),
                    State = item.i_state,
                };
                Invoices.Add(invoiceVM);
            }
        }

        public void LoadIncomeProducts(string invoiceId)
        {
            if (!int.TryParse(invoiceId, out int intInvoiceId))
                throw new ApplicationException("Bad invoiceId number");

            InvoiceIncomeProducts.Clear();
            var queryResult = StockmanModel.GetIncomeProductsByInvoiceId(intInvoiceId);
            foreach (var item in queryResult)
            {
                var incomeProductTableVM = new IncomeProductTableVM
                {
                    Count = item.P_count.ToString(),
                    Id = item.Id.ToString(),
                    MeasureUnit = item.Measure_unit,
                    ProductionDate = item.Production_date.ToString(),
                    Series = item.Series.ToString(),
                    StorageLocation = item.Storage_location,
                    VendorPrice = item.Vendor_price.ToString(),
                    VendorVax = item.Vendor_vax.ToString()
                };
                InvoiceIncomeProducts.Add(incomeProductTableVM);
            }
        }

        public void AcceptInvoice(string invoiceId, DateTime? shippingDate = null)
        {
            if (!int.TryParse(invoiceId, out int intInvoiceId))
                throw new ApplicationException("Bad invoiceId number");

            StockmanModel.AcceptInvoice(intInvoiceId, shippingDate);
        }

        public InvoiceDetailsVM GetInvoiceDetailsById(string invoiceIdVM)
        {
            if (!int.TryParse(invoiceIdVM, out int invoiceIdBL))
                throw new ApplicationException("Bad invoice id");

            Invoice invoiceBL = StockmanModel.GetInvoiceById(invoiceIdBL);
            Vendor vendor = StockmanModel.GetVendorById(invoiceBL.vendor_id);
            return new InvoiceDetailsVM
            {
                State = invoiceBL.i_state,
                DocumentGTIN = invoiceBL.document_gtin,
                VendorName = vendor.Short_name,
                Id = invoiceBL.Id.ToString(),
                InvoiceDate = invoiceBL.invoice_date.ToString(),
                InvoiceNumber = invoiceBL.invoice_number.ToString()
            };
        }

        public IncomeProductDetailsVM GetIncomeProductDetailsById(string incomeProductIdVM)
        {
            if (!int.TryParse(incomeProductIdVM, out int incomeProductIdBL))
                throw new ApplicationException("Bad income product id");

            IncomeProduct incomeProductBL = StockmanModel.GetIncomeProductById(incomeProductIdBL);
            Product productBL = StockmanModel.GetProductById(incomeProductBL.Product_id);
            return new IncomeProductDetailsVM
            {
                Count = incomeProductBL.P_count.ToString(),
                Id = incomeProductBL.Id.ToString(),
                MeasureUnit = incomeProductBL.Measure_unit,
                ProductionDate = incomeProductBL.Production_date.ToString(),
                ProductName = productBL.P_name,
                Series = incomeProductBL.Series.ToString(),
                StorageLocation = incomeProductBL.Storage_location
            };
        }

        public void UpdateIncomeProduct(IncomeProductVM incomeProduct)
        {
            if (!int.TryParse(incomeProduct.Id, out int incomeProductIdBL))
                throw new ApplicationException("Bad write off product id");

            IncomeProduct incomeProductBL = StockmanModel.GetIncomeProductById(incomeProductIdBL);

            incomeProductBL.Storage_location = incomeProduct.Storage_location;

            StockmanModel.UpdateIncomeProduct(incomeProductBL);
            LoadIncomeProducts(incomeProduct.Invoice_id);
        }
    }
}
