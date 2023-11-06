using System;
using System.ComponentModel;
using System.Collections.ObjectModel;

using BusinessSpecification.Entities;
using DefaultBusinessLogic;
using ViewModel.Entities;
using ViewModel.Entities.PharmacistEntities;
using ViewModel.Entities.PharmacistEntities.TableEntities;
using ViewModel.Converters;

namespace ViewModel.PharmacistViewModels
{
    public class PharmacistBaseViewModel : BaseNotify
    {
        private CommonStatusBarViewModel _commonStatusBarViewModel = new CommonStatusBarViewModel();
        protected IPharmacist PharmacistModel { get; set; }

        public PharmacistBaseViewModel()
        {
            PharmacistModel = Facade.Pharmacist;
        }

        public ManufacturerVM GetManufacturerById(string manufacturerIdVM)
        {
            if (!int.TryParse(manufacturerIdVM, out int manufacturerIdBL))
                throw new ApplicationException("Bad manufacturer id");

            Manufacturer manufacturerBL = PharmacistModel.GetManufacturerById(manufacturerIdBL);
            return ManufacturerConverter.MapToViewModel(manufacturerBL);
        }

        public MarkedProductVM GetMarkedProductById(string idVM)
        {
            if (!int.TryParse(idVM, out int idBL))
                throw new ApplicationException("Bad marked product id");

            MarkedProduct markedProductBL = PharmacistModel.GetMarkedProductById(idBL);
            return MarkedProductConverter.MapToViewModel(markedProductBL);
        }

        public MarkInfoVM GetMarkInfoById(string idVM)
        {
            if (!int.TryParse(idVM, out int idBL))
                throw new ApplicationException("Bad markinfo id");

            MarkInfo markInfoBL = PharmacistModel.GetMarkInfoById(idBL);
            return MarkInfoConverter.MapToViewModel(markInfoBL);
        }

        public IncomeProductVM GetIncomeProductById(string idVM)
        {
            if (!int.TryParse(idVM, out int idBL))
                throw new ApplicationException("Bad income product id");

            IncomeProduct incomeProductBL = PharmacistModel.GetIncomeProductById(idBL);
            return IncomeProductConverter.MapToViewModel(incomeProductBL);
        }

        public MarkedProductDetailsVM GetMarkedProductDetailsById(string idVM)
        {
            if (!int.TryParse(idVM, out int idBL))
                throw new ApplicationException("Bad marked product id");

            MarkedProduct markedProductBL = PharmacistModel.GetMarkedProductById(idBL);
            MarkInfo markInfoBL = PharmacistModel.GetMarkInfoById(markedProductBL.Mark_info_id);
            IncomeProduct incomeProductBL = PharmacistModel.GetIncomeProductById(markInfoBL.Income_product_id);
            Product productBL = PharmacistModel.GetProductById(incomeProductBL.Product_id);

            return new MarkedProductDetailsVM
            {
                ApprovedDate = markInfoBL.approved_date.ToString(),
                Count = markInfoBL.P_count.ToString(),
                Id = markedProductBL.Id.ToString(),
                ProductName = productBL.P_name,
                StorageLocation = markInfoBL.Storage_location
            };
        }

        public ProductDetailsVM GetProductDetailsVMById(string idVM)
        {
            if (!int.TryParse(idVM, out int idBL))
                throw new ApplicationException("Bad product Id");

            Product productBL = PharmacistModel.GetProductById(idBL);
            Manufacturer manufacturerBL = PharmacistModel.GetManufacturerById(productBL.Manufacturer_id);

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
            PersonMetadata metadata = PharmacistModel.GetInnerEmployeeMetadata();
            _commonStatusBarViewModel.EmployeeName =
                $"{metadata.Middle_name} {metadata.First_name} {metadata.Last_name}";
        }

        public void Logout()
        {
            Facade.Logout();
        }
    }
}
