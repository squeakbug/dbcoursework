using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

using BusinessSpecification.Entities;
using DefaultBusinessLogic;
using ViewModel.Entities;
using ViewModel.Entities.PharmacyManagerEntities;
using ViewModel.Entities.PharmacyManagerEntities.TableEntities;
using ViewModel.Converters;

namespace ViewModel.PharmacyManagerViewModels
{
    public class PharmacyManagerInvoicesViewModel : PharmacyManagerBaseViewModel
    {
        public string SelectedProductId { get; set; }
        public string SelectedVendorId { get; set; }
        public ObservableCollection<IncomeProductTableVM> EditInvoiceIncomeProducts { get; set; }
        public ObservableCollection<VendorTableVM> Vendors { get; set; }
        public ObservableCollection<InvoiceTableVM> Invoices { get; set; }
        public ObservableCollection<ProductTableVM> Products { get; set; }
        public ObservableCollection<IncomeProduct> IncomeProducts { get; set; }
        public ObservableCollection<IncomeProductTableVM> InvoiceIncomeProducts { get; set; }

        public PharmacyManagerInvoicesViewModel()
            : base()
        {
            Invoices = new ObservableCollection<InvoiceTableVM>();
            Vendors = new ObservableCollection<VendorTableVM>();
            Products = new ObservableCollection<ProductTableVM>();
            IncomeProducts = new ObservableCollection<IncomeProduct>();
            InvoiceIncomeProducts = new ObservableCollection<IncomeProductTableVM>();
            EditInvoiceIncomeProducts = new ObservableCollection<IncomeProductTableVM>();

            LoadInvoices();
        }

        public void LoadVendors()
        {
            Vendors.Clear();
            IEnumerable<Vendor> vendors = PharmacyManagerModel.GetVendors();
            foreach (var item in vendors)
            {
                var vendorVM = new VendorTableVM
                {
                    Id = item.Id.ToString(),
                    Full_name = item.Full_name,
                    Inn = item.Inn.ToString(),
                    Kpp = item.Kpp.ToString(),
                    Phone = item.Phone,
                    Short_name = item.Short_name
                };
                Vendors.Add(vendorVM);
            }
        }

        public void UpdateInvoice(InvoiceVM invoiceVM)
        {
            if (!int.TryParse(invoiceVM.Id, out int invoiceIdBL))
                throw new ApplicationException("Bad invoice id");
            if (!int.TryParse(invoiceVM.invoice_number, out int invoiceNumber))
                throw new ApplicationException("Bad invoice number");

            Invoice invoiceBL = PharmacyManagerModel.GetInvoiceById(invoiceIdBL);

            invoiceBL.invoice_number = invoiceNumber;

            PharmacyManagerModel.UpdateInvoice(invoiceBL);
            LoadInvoices();
        }

        public void LoadProducts()
        {
            Products.Clear();
            var products = PharmacyManagerModel.GetProducts();
            foreach (var item in products)
            {
                var productVM = new ProductTableVM
                {
                    Articul = item.Articul,
                    Dosage = item.Dosage.ToString(),
                    Gtin = item.Gtin,
                    Id = item.Id.ToString(),
                    Leave_condition = item.Leave_condition,
                    Maximum_shelf_life = item.Maximum_shelf_life.ToString(),
                    P_name = item.P_description,
                    Storage_temperature = item.Storage_temperature.ToString(),
                    Threashold_count = item.Threashold_count.ToString(),
                    Trademark = item.Trademark
                };
                Products.Add(productVM);
            }
        }

        public void LoadInvoices()
        {
            Invoices.Clear();
            var queryResult = PharmacyManagerModel.GetInvoices();
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

        public void LoadEditIncomeProducts(string invoiceIdVM)
        {
            if (!int.TryParse(invoiceIdVM, out int invoiceIdBL))
                throw new ApplicationException("Bad invoice id");

            EditInvoiceIncomeProducts.Clear();
            var queryResult = PharmacyManagerModel.GetIncomeProductsByInvoiceId(invoiceIdBL);
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
                EditInvoiceIncomeProducts.Add(incomeProductTableVM);
            }
        }

        public void DeleteIncomeProductFromInvoice(int id)
        {
            IncomeProducts.Remove(IncomeProducts.Where(x => x.Id == id).First());
        }

        public void LoadIncomeProducts(string invoiceId)
        {
            if (!int.TryParse(invoiceId, out int intInvoiceId))
                throw new ApplicationException("Bad invoice id");

            InvoiceIncomeProducts.Clear();
            var queryResult = PharmacyManagerModel.GetIncomeProductsByInvoiceId(intInvoiceId);
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

        public void AddProductToInvoice(AddProductToInvoiceVM productVM)
        {
            if (!int.TryParse(productVM.VendorVax, out int vendorVax))
                throw new ApplicationException("Bad vendor vax");
            if (!int.TryParse(productVM.Count, out int productCnt))
                throw new ApplicationException("Bad product count");
            if (!int.TryParse(productVM.Series, out int productSeries))
                throw new ApplicationException("Bad product series");
            if (!decimal.TryParse(productVM.VendorPrice, out decimal vendorProductPrice))
                throw new ApplicationException("Bad vendor product price");
            if (!int.TryParse(SelectedProductId, out int productId))
                throw new ApplicationException("Bad product id");
            if (!DateTime.TryParse(productVM.ProductionDate, out DateTime productionDate))
                throw new ApplicationException("Bad product production date");

            var newProduct = new IncomeProduct
            {
                Product_id = productId,
                Measure_unit = productVM.MeasureUnit,
                Production_date = productionDate,
                P_count = productCnt,
                Vendor_vax = vendorVax,
                Series = productSeries,
                Storage_location = productVM.StorageLocation,
                Vendor_price = vendorProductPrice,
            };
            IncomeProducts.Add(newProduct);
        }

        public void AddInvoice(AddNewInvoiceVM invoiceVM)
        {
            if (!int.TryParse(SelectedVendorId, out int vendorId))
                throw new ApplicationException("Bad vendor Id");
            if (!int.TryParse(invoiceVM.InvoiceNumber, out int invoiceNumber))
                throw new ApplicationException("Bad invoice number");

            var invoice = new Invoice
            {
                vendor_id = vendorId,
                invoice_number = invoiceNumber
            };
            PharmacyManagerModel.AddInvoiceWithProducts(invoice, IncomeProducts);
            LoadInvoices();
        }

        public void ApproveInvoice(string invoiceIdVM)
        {
            if (!int.TryParse(invoiceIdVM, out int invoiceIdBL))
                throw new ApplicationException("Bad invoice Id");

            PharmacyManagerModel.ApproveInvoice(invoiceIdBL);
            LoadInvoices();
        }

        public IncomeProductDetailsVM GetIncomeProductDetailsById(string incomeProductIdVM)
        {
            if (!int.TryParse(incomeProductIdVM, out int incomeProductIdBL))
                throw new ApplicationException("Bad income product id");

            IncomeProduct incomeProductBL = PharmacyManagerModel.GetIncomeProductById(incomeProductIdBL);
            Product productBL = PharmacyManagerModel.GetProductById(incomeProductBL.Product_id);
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
    }
}
