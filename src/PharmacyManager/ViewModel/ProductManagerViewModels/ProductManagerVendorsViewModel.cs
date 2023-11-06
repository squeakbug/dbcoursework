using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using BusinessSpecification.Entities;
using ViewModel.Converters;
using ViewModel.Entities;

namespace ViewModel.ProductManagerViewModels
{
    public class ProductManagerVendorsViewModel : ProductManagerBaseViewModel
    {
        private Vendor _selectedVendor;
        public ObservableCollection<Vendor> Vendors { get; set; }

        public ProductManagerVendorsViewModel()
        {
            Vendors = new ObservableCollection<Vendor>(ProductManagerModel.GetVendors());
        }

        public void LoadVendors()
        {
            Vendors.Clear();
            IEnumerable<Vendor> vendors = ProductManagerModel.GetVendors();
            foreach (var item in vendors)
                Vendors.Add(item);
        }

        public void AddVendor(VendorVM vendorVM)
        {
            Vendor vendorBL = VendorConverter.MapToModel(vendorVM);
            ProductManagerModel.AddVendor(vendorBL);
            LoadVendors();
        }

        public void UpdateVendor(VendorVM vendorVM)
        {
            if (!int.TryParse(vendorVM.Id, out int vendorIdBL))
                throw new ApplicationException("Bad vendor id");

            Vendor vendorBL = ProductManagerModel.GetVendorById(vendorIdBL);
            Vendor mappedVendorBL = VendorConverter.MapToModel(vendorVM);

            vendorBL.Full_name = mappedVendorBL.Full_name;
            vendorBL.Inn = mappedVendorBL.Inn;
            vendorBL.Kpp = mappedVendorBL.Kpp;
            vendorBL.Phone = mappedVendorBL.Phone;
            vendorBL.Short_name = mappedVendorBL.Short_name;

            ProductManagerModel.UpdateVendor(vendorBL);
            LoadVendors();
        }

        public Vendor SelectedVendor
        {
            get => _selectedVendor;
            set
            {
                _selectedVendor = value;
                NotifyPropertyChanged(nameof(SelectedVendor));
            }
        }
    }
}
