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
    public class PharmacyManagerMarkedProductsViewModel : PharmacyManagerBaseViewModel
    {
        public ObservableCollection<MarkedProductTableVM> MarkedProducts { get; set; }

        public PharmacyManagerMarkedProductsViewModel()
        {
            MarkedProducts = new ObservableCollection<MarkedProductTableVM>();

            LoadMarkedProducts();
        }

        public void LoadMarkedProducts()
        {
            MarkedProducts.Clear();
            var markedProducts = PharmacyManagerModel.GetStoredProducts();
            var markInfos = PharmacyManagerModel.GetMarkInfos();

            foreach (var item in markedProducts)
            {
                var markInfo = markInfos.Where(x => x.Id == item.Mark_info_id).FirstOrDefault();

                var markedProductTableVM = new MarkedProductTableVM
                {
                    ApprovedDate = markInfo.approved_date.ToString(),
                    Count = markInfo.P_count.ToString(),
                    Id = item.Id.ToString(),
                    StorageLocation = markInfo.Storage_location
                };

                MarkedProducts.Add(markedProductTableVM);
            }
        }

        public void DoWriteOffProduct(string markedProductId, string countVM, string reasonVM)
        {
            if (!int.TryParse(markedProductId, out int markedProductIdBL))
                throw new ApplicationException("Bad marked product id");
            if (!int.TryParse(countVM, out int countBL))
                throw new ApplicationException("Bad marked product count");

            PharmacyManagerModel.WriteOffProduct(markedProductIdBL, countBL, reasonVM);
            LoadMarkedProducts();
        }
    }
}
