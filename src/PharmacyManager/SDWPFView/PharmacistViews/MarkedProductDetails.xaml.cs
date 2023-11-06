using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using ViewModel;
using ViewModel.Entities;
using ViewModel.Entities.PharmacistEntities;
using ViewModel.Entities.PharmacistEntities.TableEntities;
using ViewModel.PharmacistViewModels;

namespace SDWPFView.PharmacistViews
{
    public partial class MarkedProductDetails : Window
    {
        private PharmacistBaseViewModel _pharmacistVM;
        private string _markedProductId;

        public MarkedProductDetails(PharmacistBaseViewModel pharmacistVM, string markedProductId)
        {
            InitializeComponent();
            _pharmacistVM = pharmacistVM;
            _markedProductId = markedProductId;

            MarkedProductDetailsVM markedProductDetails = _pharmacistVM.GetMarkedProductDetailsById(markedProductId);
            ApprovedDateTextBox.Text = markedProductDetails.ApprovedDate;
            CountTextBox.Text = markedProductDetails.Count;
            ProductNameTextBox.Text = markedProductDetails.ProductName;
            StorageLocationTextBox.Text = markedProductDetails.StorageLocation;
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ShowProductDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            MarkedProductVM markedProductVM = _pharmacistVM.GetMarkedProductById(_markedProductId);
            MarkInfoVM markInfoVM = _pharmacistVM.GetMarkInfoById(markedProductVM.Mark_info_id);
            IncomeProductVM incomeProductVM = _pharmacistVM.GetIncomeProductById(markInfoVM.Income_product_id);
            new ProductDetailsWindow(_pharmacistVM, incomeProductVM.Product_id).Show();
        }
    }
}
