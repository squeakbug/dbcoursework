using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using BusinessSpecification.Entities;
using DefaultBusinessLogic;
using ViewModel.Entities;
using ViewModel.Converters;

namespace ViewModel.ProductManagerViewModels
{
    public class ProductManagerBaseViewModel : BaseNotify
    {
        private CommonStatusBarViewModel _commonStatusBarViewModel = new CommonStatusBarViewModel();
        public IProductManager ProductManagerModel { get; }

        public ProductManagerBaseViewModel()
        {
            ProductManagerModel = Facade.ProductManager;
        }

        public IEnumerable<string> GetProductCategoiesId(string productIdVM)
        {
            if (!int.TryParse(productIdVM, out int productIdBL))
                throw new ApplicationException("Bad product id");

            var productBL = ProductManagerModel.GetProductById(productIdBL);
            var productVM = ProductConverter.MapToViewModel(productBL);
            return productVM.CategoriesId;
        }

        public ManufacturerVM GetManufacturerById(string manufacturerIdVM)
        {
            if (!int.TryParse(manufacturerIdVM, out int manufacturerIdBL))
                throw new ApplicationException("Bad manufacturer id");

            Manufacturer manufacturerBL = ProductManagerModel.GetManufacturerById(manufacturerIdBL);
            return ManufacturerConverter.MapToViewModel(manufacturerBL);
        }

        public VendorVM GetVendorById(string idVM)
        {
            if (!int.TryParse(idVM, out int idBL))
                throw new ApplicationException("Bad vendor id");

            var vendorBL = ProductManagerModel.GetVendorById(idBL);
            return VendorConverter.MapToViewModel(vendorBL);
        }

        public ProductVM GetProductById(string productIdVM)
        {
            if (!int.TryParse(productIdVM, out int productIdBL))
                throw new ApplicationException("Bad product id");

            var productBL = ProductManagerModel.GetProductById(productIdBL);
            return ProductConverter.MapToViewModel(productBL);
        }

        public MarkedProductVM GetMarkedProductById(string markedProductIdVM)
        {
            if (!int.TryParse(markedProductIdVM, out int markedProductIdBL))
                throw new ApplicationException("Bad marked product id");

            MarkedProduct markedProductBL = ProductManagerModel.GetMarkedProductById(markedProductIdBL);
            return MarkedProductConverter.MapToViewModel(markedProductBL);
        }

        public MarkInfoVM GetMarkInfoById(string markInfoIdVM)
        {
            if (!int.TryParse(markInfoIdVM, out int markInfoIdBL))
                throw new ApplicationException("Bad mark info id");

            MarkInfo markInfoBL = ProductManagerModel.GetMarkInfoById(markInfoIdBL);
            return MarkInfoConverter.MapToViewModel(markInfoBL);
        }

        public void UpdateMarkInfo(MarkInfoVM markInfoVM)
        {
            MarkInfo markInfoBL = MarkInfoConverter.MapToModel(markInfoVM);
            ProductManagerModel.UpdateMarkInfo(markInfoBL);
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
            PersonMetadata metadata = ProductManagerModel.GetInnerEmployeeMetadata();
            _commonStatusBarViewModel.EmployeeName =
                $"{metadata.Middle_name} {metadata.First_name} {metadata.Last_name}";
        }

        public void Logout()
        {
            Facade.Logout();
        }
    }
}
