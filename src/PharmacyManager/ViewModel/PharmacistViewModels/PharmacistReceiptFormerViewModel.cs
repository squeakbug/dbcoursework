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
    public class PharmacistReceiptFormerViewModel : PharmacistBaseViewModel
    {
        private string _totalPrice;
        public string SelectedProductId { get; set; }
        public ObservableCollection<MarkedProductTableVM> ReceiptMarkedProducts { get; set; }
        public ObservableCollection<MarkedProductTableVM> MarkedProducts { get; set; }

        public PharmacistReceiptFormerViewModel()
            : base()
        {
            TotalPrice = "0";
            ReceiptMarkedProducts = new ObservableCollection<MarkedProductTableVM>();
            MarkedProducts = new ObservableCollection<MarkedProductTableVM>();
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

        public void RemoveMarkedProduct(string markedProductId)
        {
            ReceiptMarkedProducts.Remove(ReceiptMarkedProducts.Where(x => x.Id == markedProductId).First());
        }

        public void AddMarkedProduct(string idVM, string cntVM)
        {
            if (!int.TryParse(idVM, out int idBL))
                throw new ApplicationException("Bad marked product id");

            MarkedProduct markedProductBL = PharmacistModel.GetMarkedProductById(idBL);
            MarkInfo markInfoBL = PharmacistModel.GetMarkInfoById(markedProductBL.Mark_info_id);

            var markedProductVM = new MarkedProductTableVM
            {
                ApprovedDate = markInfoBL.approved_date.ToString(),
                Count = cntVM,
                Id = markedProductBL.Id.ToString(),
                StorageLocation = markInfoBL.Storage_location
            };

            ReceiptMarkedProducts.Add(markedProductVM);
            TotalPrice = "10";
        }

        public string TotalPrice
        {
            get => _totalPrice;
            set
            {
                _totalPrice = value;
                NotifyPropertyChanged(nameof(TotalPrice));
            }
        }

        public void ConfirmReceiptOperation(string discountVM, string cashSizeVM, string cardSizeVM)
        {
            PharmacistModel.InitOperation();
            foreach (var item in ReceiptMarkedProducts)
            {
                if (!int.TryParse(item.Id, out int markedProductId))
                {
                    PharmacistModel.CancelOperation();
                    throw new ApplicationException("Bad marked product Id");
                }
                if (!uint.TryParse(item.Count, out uint cnt))
                {
                    PharmacistModel.CancelOperation();
                    throw new ApplicationException("Bad marked product count");
                }
                PharmacistModel.AddProductToOperation(markedProductId, cnt);
            }
            if (!decimal.TryParse(discountVM, out decimal discountBL))
            {
                PharmacistModel.CancelOperation();
                throw new ApplicationException("Bad discount");
            }
            if (!decimal.TryParse(cardSizeVM, out decimal cardSizeBL))
            {
                PharmacistModel.CancelOperation();
                throw new ApplicationException("Bad card size");
            }
            if (!decimal.TryParse(cashSizeVM, out decimal cashSizeBL))
            {
                PharmacistModel.CancelOperation();
                throw new ApplicationException("Bad cash size");
            }
            PharmacistModel.SetDiscount(discountBL);

            string paymentType = "";
            if (cashSizeBL != 0)
            {
                paymentType = "cash";
                if (cardSizeBL != 0)
                    paymentType += "/card";
            }
            else
            {
                if (cardSizeBL != 0)
                    paymentType = "card";
                else
                    throw new ApplicationException("Bad payment type");
            }

            try
            {
                PharmacistModel.ConfirmOperation(cardSizeBL, cashSizeBL, paymentType);
                TotalPrice = "0";
            }
            catch (ApplicationException ex)
            {
                PharmacistModel.CancelOperation();
                throw new ApplicationException(ex.Message, ex);
            }
        }
    }
}
