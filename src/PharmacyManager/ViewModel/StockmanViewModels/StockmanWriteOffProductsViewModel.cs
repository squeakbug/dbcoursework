using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;

using DefaultBusinessLogic;
using BusinessSpecification.Entities;
using ViewModel.Entities;
using ViewModel.Entities.StockmanEntities;
using ViewModel.Entities.StockmanEntities.TableEntities;
using ViewModel.Converters;

namespace ViewModel.StockmanViewModels
{
    public class StockmanWriteOffProductsViewModel : StockmanBaseViewModel
    {
        public ObservableCollection<WriteOffProductTableVM> WriteOffProducts { get; set; }

        public StockmanWriteOffProductsViewModel()
            : base()
        {
            WriteOffProducts = new ObservableCollection<WriteOffProductTableVM>();

            LoadWriteOffProducts();
        }

        public void LoadWriteOffProducts()
        {
            WriteOffProducts.Clear();
            var writeOffProducts = StockmanModel.GetWriteOffProducts();
            var markInfos = StockmanModel.GetMarkInfos();
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

            WriteOffProduct writeOffProductBL = StockmanModel.GetWriteOffProductById(idBL);
            MarkInfo markInfoBL = StockmanModel.GetMarkInfoById(writeOffProductBL.Mark_info_id);
            IncomeProduct incomeProductBL = StockmanModel.GetIncomeProductById(markInfoBL.Income_product_id);
            Product productBL = StockmanModel.GetProductById(incomeProductBL.Product_id);
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

        public void UpdateWriteOffProduct(WriteOffProductVM writeOffProduct, MarkInfoVM markInfo)
        {
            if (!int.TryParse(writeOffProduct.Id, out int writeOffProductIdBL))
                throw new ApplicationException("Bad write off product id");

            WriteOffProduct writeOffProductBL = StockmanModel.GetWriteOffProductById(writeOffProductIdBL);
            MarkInfo markInfoBL = StockmanModel.GetMarkInfoById(writeOffProductBL.Mark_info_id);

            markInfoBL.Storage_location = markInfo.Storage_location;

            StockmanModel.UpdateMarkInfo(markInfoBL);
            LoadWriteOffProducts();
        }
    }
}
