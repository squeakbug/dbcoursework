using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

using DefaultBusinessLogic;
using BusinessSpecification.Entities;
using ViewModel.Entities;
using ViewModel.Entities.PharmacyManagerEntities;
using ViewModel.Entities.PharmacyManagerEntities.TableEntities;
using ViewModel.Converters;

namespace ViewModel.PharmacyManagerViewModels
{
    public class PharmacyManagerWriteOffProductViewModel : PharmacyManagerBaseViewModel
    {
        public ObservableCollection<WriteOffProductTableVM> WriteOffProducts { get; set; }

        public PharmacyManagerWriteOffProductViewModel()
        {
            WriteOffProducts = new ObservableCollection<WriteOffProductTableVM>();

            LoadWriteOffProducts();
        }

        public void LoadWriteOffProducts()
        {
            WriteOffProducts.Clear();
            var writeOffProducts = PharmacyManagerModel.GetWriteOffProducts();
            var markInfos = PharmacyManagerModel.GetMarkInfos();
            foreach (var item in writeOffProducts)
            {
                MarkInfo markInfoBL = markInfos.Where(x => x.Id == item.Mark_info_id).FirstOrDefault();

                var writeOffProductVM = new WriteOffProductTableVM
                {
                    Count = markInfoBL.P_count.ToString(),
                    Id = item.Id.ToString(),
                    StorageLocation = markInfoBL.Storage_location,
                    WriteOffDate = item.Write_off_date.ToString()
                };
                WriteOffProducts.Add(writeOffProductVM);
            }
        }

        public WriteOffProductDetailsVM GetWriteOffProductDetailsById(string idVM)
        {
            if (!int.TryParse(idVM, out int idBL))
                throw new ApplicationException("Bad write-off product id");

            WriteOffProduct writeOffProductBL = PharmacyManagerModel.GetWriteOffProductById(idBL);
            MarkInfo markInfoBL = PharmacyManagerModel.GetMarkInfoById(writeOffProductBL.Mark_info_id);
            IncomeProduct incomeProductBL = PharmacyManagerModel.GetIncomeProductById(markInfoBL.Income_product_id);
            Product productBL = PharmacyManagerModel.GetProductById(incomeProductBL.Product_id);
            return new WriteOffProductDetailsVM
            {
                Count = markInfoBL.P_count.ToString(),
                Id = writeOffProductBL.Id.ToString(),
                ProductName = productBL.P_name,
                StorageLocation = markInfoBL.Storage_location,
                WriteOffDate = writeOffProductBL.Write_off_date.ToString(),
                Reason = writeOffProductBL.Reason
            };
        }
    }
}
