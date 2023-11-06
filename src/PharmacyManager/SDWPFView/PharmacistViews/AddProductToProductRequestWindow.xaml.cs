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

using BusinessSpecification.Entities;
using ViewModel;
using ViewModel.Entities;
using ViewModel.Entities.PharmacistEntities;
using ViewModel.Entities.PharmacistEntities.TableEntities;
using ViewModel.PharmacistViewModels;

namespace SDWPFView.PharmacistViews
{
    public partial class AddProductToProductRequestWindow : Window
    {
        private PharmacistProductRequestFormerViewModel _pharmacistVM;
        public AddProductToProductRequestWindow(PharmacistProductRequestFormerViewModel pharmacistVM)
        {
            InitializeComponent();
            _pharmacistVM = pharmacistVM;
            ProductTable.ItemsSource = _pharmacistVM.Products;
        }

        private void ChooseBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = (ProductTableVM)ProductTable.SelectedItem;
            if (selectedProduct == null)
            {
                MessageBox.Show("Не выбрано лекарственное средство");
                return;
            }

            _pharmacistVM.SelectedProductToRequestId = selectedProduct.Id;
            Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
