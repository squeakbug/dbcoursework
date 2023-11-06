using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

using DefaultBusinessLogic;
using BusinessSpecification.Entities;
using ViewModel.Entities;
using ViewModel.Entities.StockmanEntities;
using ViewModel.Converters;

namespace ViewModel.StockmanViewModels
{
    public class StockmanBaseViewModel : BaseNotify
    {
        private CommonStatusBarViewModel _commonStatusBarViewModel = new CommonStatusBarViewModel();
        protected IStockman StockmanModel { get; }

        public StockmanBaseViewModel()
        {
            StockmanModel = Facade.Stockman;
        }

        public ProductVM GetProductById(int id)
        {
            var productBL = StockmanModel.GetProductById(id);
            return ProductConverter.MapToViewModel(productBL);
        }

        public IncomeProductVM GetIncomeProductById(string incomeProductIdVM)
        {
            if (!int.TryParse(incomeProductIdVM, out int incomeProductIdBL))
                throw new ApplicationException("Bad income product id");

            var incomeProductBL = StockmanModel.GetIncomeProductById(incomeProductIdBL);
            return IncomeProductConverter.MapToViewModel(incomeProductBL);
        }

        public VendorVM GetVendorById(string idVM)
        {
            if (!int.TryParse(idVM, out int idBL))
                throw new ApplicationException("Bad vendor id");

            var vendorBL = StockmanModel.GetVendorById(idBL);
            return VendorConverter.MapToViewModel(vendorBL);
        }

        public MarkInfoVM GetMarkInfoById(string idVM)
        {
            if (!int.TryParse(idVM, out int idBL))
                throw new ApplicationException("Bad markinfo id");

            var markInfoBL = StockmanModel.GetMarkInfoById(idBL);
            return MarkInfoConverter.MapToViewModel(markInfoBL);
        }

        public WriteOffProductVM GetWriteOffProductById(string idVM)
        {
            if (!int.TryParse(idVM, out int idBL))
                throw new ApplicationException("Bad write-off product id");

            WriteOffProduct writeOffProductBL = StockmanModel.GetWriteOffProductById(idBL);
            return WriteOffProductConverter.MapToViewModel(writeOffProductBL);
        }

        public MarkedProductVM GetMarkedProductById(string idVM)
        {
            if (!int.TryParse(idVM, out int idBL))
                throw new ApplicationException("Bad marked product id");

            MarkedProduct markedProductBL = StockmanModel.GetMarkedProductById(idBL);
            return MarkedProductConverter.MapToViewModel(markedProductBL);
        }

        public ProductVM GetProductByMarkInfotId(int id)
        {
            var markInfoBL = StockmanModel.GetMarkInfoById(id);
            var incomeProductBL = StockmanModel.GetIncomeProductById(markInfoBL.Id);
            var productBL = StockmanModel.GetProductById(incomeProductBL.Id);
            return ProductConverter.MapToViewModel(productBL);
        }

        public ProductDetailsVM GetProductDetailsVMById(string idVM)
        {
            if (!int.TryParse(idVM, out int idBL))
                throw new ApplicationException("Bad product Id");

            Product productBL = StockmanModel.GetProductById(idBL);
            Manufacturer manufacturerBL = StockmanModel.GetManufacturerById(productBL.Manufacturer_id);

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
            PersonMetadata metadata = StockmanModel.GetInnerEmployeeMetadata();
            _commonStatusBarViewModel.EmployeeName =
                $"{metadata.Middle_name} {metadata.First_name} {metadata.Last_name}";
        }

        public void Logout()
        {
            Facade.Logout();
        }
    }
}
