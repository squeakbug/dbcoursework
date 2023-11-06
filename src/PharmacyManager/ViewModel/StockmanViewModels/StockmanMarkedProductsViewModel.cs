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
    public class StockmanMarkedProductsViewModel : StockmanBaseViewModel
    {
        public ObservableCollection<MarkedProductTableVM> MarkedProducts { get; set; }

        public StockmanMarkedProductsViewModel()
            : base()
        {
            MarkedProducts = new ObservableCollection<MarkedProductTableVM>();

            LoadMarkedProducts();
        }

        public void LoadMarkedProducts()
        {
            MarkedProducts.Clear();
            var storedProducts = StockmanModel.GetStoredProducts();
            var markInfos = StockmanModel.GetMarkInfos();

            foreach (var item in storedProducts)
            {
                MarkInfo markInfoBL = markInfos.Where(x => x.Id == item.Mark_info_id).FirstOrDefault();

                var markedProductTableVM = new MarkedProductTableVM
                {
                    ApprovedDate = markInfoBL.approved_date.ToString(),
                    Count = markInfoBL.P_count.ToString(),
                    Id = item.Id.ToString(),
                    StorageLocation = markInfoBL.Storage_location
                };
                MarkedProducts.Add(markedProductTableVM);
            }
        }

        public MarkedProductDetailsVM GetMarkedProductDetailsById(string idVM)
        {
            if (!int.TryParse(idVM, out int idBL))
                throw new ApplicationException("Bad marked product id");

            MarkedProduct markedProductBL = StockmanModel.GetMarkedProductById(idBL);
            MarkInfo markInfoBL = StockmanModel.GetMarkInfoById(markedProductBL.Mark_info_id);
            IncomeProduct incomeProductBL = StockmanModel.GetIncomeProductById(markInfoBL.Income_product_id);
            Product productBL = StockmanModel.GetProductById(incomeProductBL.Product_id);

            return new MarkedProductDetailsVM
            {
                ApprovedDate = markInfoBL.approved_date.ToString(),
                Count = markInfoBL.P_count.ToString(),
                Id = markedProductBL.Id.ToString(),
                ProductName = productBL.P_name,
                StorageLocation = markInfoBL.Storage_location
            };
        }

        public void UpdateMarkedProduct(MarkedProductVM markedProduct, MarkInfoVM markInfo)
        {
            if (!int.TryParse(markedProduct.Id, out int markedProductIdBL))
                throw new ApplicationException("Bad write off product id");

            MarkedProduct markedProductBL = StockmanModel.GetMarkedProductById(markedProductIdBL);
            MarkInfo markInfoBL = StockmanModel.GetMarkInfoById(markedProductBL.Mark_info_id);

            markInfoBL.Storage_location = markInfo.Storage_location;

            StockmanModel.UpdateMarkInfo(markInfoBL);
            LoadMarkedProducts();
        }
    }
}
