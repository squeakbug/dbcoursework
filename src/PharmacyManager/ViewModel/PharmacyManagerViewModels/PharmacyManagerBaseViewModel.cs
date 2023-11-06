using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using BusinessSpecification.Entities;
using DefaultBusinessLogic;
using ViewModel.Entities;
using ViewModel.Entities.PharmacyManagerEntities;
using ViewModel.Entities.PharmacyManagerEntities.TableEntities;
using ViewModel.Converters;

namespace ViewModel.PharmacyManagerViewModels
{
    public class PharmacyManagerBaseViewModel : BaseNotify
    {
        private CommonStatusBarViewModel _commonStatusBarViewModel = new CommonStatusBarViewModel();
        protected IPharmacyManager PharmacyManagerModel { get; }

        public PharmacyManagerBaseViewModel()
        {
            PharmacyManagerModel = Facade.PharmacyManager;
        }

        public PersonMetadataVM GetPersonMetadataById(int id)
        {
            var metadataBL = PharmacyManagerModel.GetPersonMetadataById(id);
            return PersonMetadataConverter.MapToViewModel(metadataBL);
        }

        public IEnumerable<string> GetProductCategoiesId(string productIdVM)
        {
            if (!int.TryParse(productIdVM, out int productIdBL))
                throw new ApplicationException("Bad product id");

            var productBL = PharmacyManagerModel.GetProductById(productIdBL);
            var productVM = ProductConverter.MapToViewModel(productBL);
            return productVM.CategoriesId;
        }

        public ManufacturerVM GetManufacturerById(string manufacturerIdVM)
        {
            if (!int.TryParse(manufacturerIdVM, out int manufacturerIdBL))
                throw new ApplicationException("Bad manufacturer id");

            Manufacturer manufacturerBL = PharmacyManagerModel.GetManufacturerById(manufacturerIdBL);
            return ManufacturerConverter.MapToViewModel(manufacturerBL);
        }

        public ProductVM GetProductById(string productIdVM)
        {
            if (!int.TryParse(productIdVM, out int productIdBL))
                throw new ApplicationException("Bad product id");

            var productBL = PharmacyManagerModel.GetProductById(productIdBL);
            return ProductConverter.MapToViewModel(productBL);
        }

        public VendorVM GetVendorById(string vendorIdVM)
        {
            if (!int.TryParse(vendorIdVM, out int vendorIdBL))
                throw new ApplicationException("Bad vendor id");

            var vendorBL = PharmacyManagerModel.GetVendorById(vendorIdBL);
            return VendorConverter.MapToViewModel(vendorBL);
        }

        public EmployeeVM GetEmployeeById(string employeeIdVM)
        {
            if (!int.TryParse(employeeIdVM, out int employeeIdBL))
                throw new ApplicationException("Bad employee id");

            Employee employeeBL = PharmacyManagerModel.GetEmployeeById(employeeIdBL);
            PersonMetadata metadataBL = PharmacyManagerModel.GetPersonMetadataById(employeeBL.person_metadata_id);
            return EmployeeConverter.MapToViewModel(employeeBL, metadataBL);
        }

        public WriteOffProductVM GetWriteOffProductById(string idVM)
        {
            if (!int.TryParse(idVM, out int idBL))
                throw new ApplicationException("Bad write-off product id");

            WriteOffProduct writeOffProductBL = PharmacyManagerModel.GetWriteOffProductById(idBL);
            return WriteOffProductConverter.MapToViewModel(writeOffProductBL);
        }

        public MarkedProductVM GetMarkedProductById(string idVM)
        {
            if (!int.TryParse(idVM, out int idBL))
                throw new ApplicationException("Bad marked product id");

            MarkedProduct markedProductBL = PharmacyManagerModel.GetMarkedProductById(idBL);
            return MarkedProductConverter.MapToViewModel(markedProductBL);
        }

        public MarkInfoVM GetMarkInfoById(string idVM)
        {
            if (!int.TryParse(idVM, out int idBL))
                throw new ApplicationException("Bad markinfo id");

            MarkInfo markInfoBL = PharmacyManagerModel.GetMarkInfoById(idBL);
            return MarkInfoConverter.MapToViewModel(markInfoBL);
        }

        public IncomeProductVM GetIncomeProductById(string idVM)
        {
            if (!int.TryParse(idVM, out int idBL))
                throw new ApplicationException("Bad income product id");

            IncomeProduct incomeProductBL = PharmacyManagerModel.GetIncomeProductById(idBL);
            return IncomeProductConverter.MapToViewModel(incomeProductBL);
        }

        public ProductDetailsVM GetProductDetailsVMById(string idVM)
        {
            if (!int.TryParse(idVM, out int idBL))
                throw new ApplicationException("Bad product Id");

            Product productBL = PharmacyManagerModel.GetProductById(idBL);
            Manufacturer manufacturerBL = PharmacyManagerModel.GetManufacturerById(productBL.Manufacturer_id);

            return new ProductDetailsVM
            {
                Articul = productBL.Articul,
                Count_in_package = productBL.Count_in_package.ToString(),
                Default_markup = productBL.Default_markup.ToString(),
                Dosage = productBL.Dosage.ToString(),
                Dosage_form = productBL.Dosage_form,
                Gtin = productBL.Gtin,
                Instruction = productBL.Instruction,
                International_name = productBL.International_name,
                Leave_condition = productBL.Leave_condition,
                Manufacturer = manufacturerBL.M_name,
                Maximum_markup = productBL.Maximum_markup.ToString(),
                Maximum_shelf_life = productBL.Maximum_markup.ToString(),
                Primacy_packaging = productBL.Primacy_packaging,
                P_description = productBL.P_description,
                P_name = productBL.P_name,
                Storage_temperature = productBL.Storage_temperature.ToString(),
                Threashold_count = productBL.Threashold_count.ToString(),
                Trademark = productBL.Trademark
            };
        }

        public InvoiceVM GetInvoiceById(string idVM)
        {
            if (!int.TryParse(idVM, out int idBL))
                throw new ApplicationException("Bad invoice id");

            Invoice invoiceBL = PharmacyManagerModel.GetInvoiceById(idBL);
            return InvoiceConverter.MapToViewModel(invoiceBL);
        }

        public void UpdateCurrentControlName(string controlName)
        {
            _commonStatusBarViewModel.CurrentControlName = controlName;
        }

        public void UpdateStatus(string newStatus)
        {
            _commonStatusBarViewModel.StatusString = newStatus;
        }

        public void UpdateEmployeeName()
        {
            PersonMetadata metadata = PharmacyManagerModel.GetInnerEmployeeMetadata();
            _commonStatusBarViewModel.EmployeeName =
                $"{metadata.Middle_name} {metadata.First_name} {metadata.Last_name}";
        }

        public void Logout()
        {
            Facade.Logout();
        }
    }
}
