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
    public class PharmacyManagerProductRequestViewModel : PharmacyManagerBaseViewModel
    {
        public ObservableCollection<ProductRequestTableVM> ProductRequests { get; set; }

        public PharmacyManagerProductRequestViewModel()
            : base()
        {
            ProductRequests = new ObservableCollection<ProductRequestTableVM>();

            LoadProductRequests();
        }

        public void LoadProductRequests()
        {
            ProductRequests.Clear();
            IEnumerable<ProductRequest> productRequests = PharmacyManagerModel.GetProductRequests();
            foreach (var item in productRequests)
            {
                var productRequest = new ProductRequestTableVM
                {
                    approved = item.approved == 0 ? "-" : "+",
                    Approved_date = item.Approved_date.ToString(),
                    Approximate_name = item.Approximate_name,
                    Id = item.Id.ToString(),
                    Request_Date = item.Request_Date.ToString()
                };
                ProductRequests.Add(productRequest);
            }
        }

        public ProductRequestDetailsVM GetProductRequestDetailsById(string idVM)
        {
            if (!int.TryParse(idVM, out int idBL))
                throw new ApplicationException("Bad product request id");

            ProductRequest productRequestBL = PharmacyManagerModel.GetProductRequestById(idBL);
            PersonMetadata metadataBL = 
                PharmacyManagerModel.GetPersonMetadataById(productRequestBL.Customer_metadata_id);

            var productRequestVM = new ProductRequestDetailsVM();
            productRequestVM.Approved = productRequestBL.approved == 0 ? "-" : "+";
            if (productRequestBL.Approved_date != null)
                productRequestVM.Approved_date = productRequestBL.Approved_date.Value.ToString();
            else
                productRequestVM.Approved_date = "";
            productRequestVM.Approximate_name = productRequestBL.Approximate_name;
            productRequestVM.ConsumerFirstName = metadataBL.First_name;
            productRequestVM.ConsumerLastName = metadataBL.Last_name;
            productRequestVM.ConsumerMiddleName = metadataBL.Middle_name;
            productRequestVM.Count = productRequestBL.Pr_count.ToString();
            productRequestVM.Id = productRequestBL.Id.ToString();
            if (productRequestBL.Product_id != null)
            {
                Product productBL = PharmacyManagerModel.GetProductById(productRequestBL.Product_id.Value);
                productRequestVM.ProductName = productBL.P_name;
            }
            else
            {
                productRequestVM.ProductName = "";
            }
            productRequestVM.RequestDate = productRequestBL.Request_Date.ToString();
            return productRequestVM;
        }

        public void ApproveProductRequest(string idVM)
        {
            if (!int.TryParse(idVM, out int idBL))
                throw new ApplicationException("Bad product request id");

            PharmacyManagerModel.ApproveProductRequest(idBL);
            LoadProductRequests();
        }
    }
}
