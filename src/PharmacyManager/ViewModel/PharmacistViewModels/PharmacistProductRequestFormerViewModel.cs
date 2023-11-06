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
    public class PharmacistProductRequestFormerViewModel : PharmacistBaseViewModel
    {
        public string SelectedProductToRequestId { get; set; }
        public ObservableCollection<ProductTableVM> Products { get; set; }

        public PharmacistProductRequestFormerViewModel()
        {
            Products = new ObservableCollection<ProductTableVM>();
        }

        public void ConfirmProductRequest(AddProductRequestVM productRequestVM)
        {
            if (!int.TryParse(productRequestVM.Pr_count, out int pr_count))
                throw new ApplicationException("Bad product count");

            var productRequest = new ProductRequest
            {
                Id = productRequestVM.Id,
                Approximate_name = productRequestVM.Approximate_name,
                Pr_count = pr_count,
            };
            var customerMetadata = new PersonMetadata
            {
                First_name = productRequestVM.ConsumerFirstName,
                Middle_name = productRequestVM.ConsumerMiddleName,
                Last_name = productRequestVM.ConsumerLastName,
                Email = productRequestVM.Email,
                Phone = productRequestVM.PhoneNumber
            };
            if (SelectedProductToRequestId != null)
            {
                if (!int.TryParse(SelectedProductToRequestId, out int productRequestId))
                    throw new ApplicationException("Bad product request id");
                productRequest.Product_id = productRequestId;
            }
            PharmacistModel.ConfirmProductRequest(productRequest, customerMetadata);
        }
    }
}
