using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;

using BusinessSpecification.Entities;
using DefaultBusinessLogic;
using ViewModel.Entities;
using ViewModel.Entities.PharmacistEntities;
using ViewModel.Entities.PharmacistEntities.TableEntities;
using ViewModel.Converters;

namespace ViewModel.PharmacistViewModels
{
    public class PharmacistMarkedProductViewModel : PharmacistBaseViewModel
    {
        public ObservableCollection<MarkedProductTableVM> MarkedProducts { get; set; }

        public PharmacistMarkedProductViewModel()
        {
            MarkedProducts = new ObservableCollection<MarkedProductTableVM>();

            LoadMarkedProducts();
        }

        public void LoadMarkedProducts()
        {
            MarkedProducts.Clear();
            var markedProducts = PharmacistModel.GetStoredProducts();
            var markInfos = PharmacistModel.GetMarkInfos();

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
    }
}
