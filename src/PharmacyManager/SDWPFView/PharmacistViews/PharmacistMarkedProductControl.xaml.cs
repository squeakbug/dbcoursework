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
using System.Windows.Navigation;
using System.Windows.Shapes;

using ViewModel;
using ViewModel.Entities;
using ViewModel.Entities.PharmacistEntities;
using ViewModel.Entities.PharmacistEntities.TableEntities;
using ViewModel.PharmacistViewModels;

namespace SDWPFView.PharmacistViews
{
    public partial class PharmacistMarkedProductControl : UserControl
    {
        private PharmacistMarkedProductViewModel _pharmacistVM;
        public PharmacistMarkedProductControl()
        {
            InitializeComponent();
            _pharmacistVM = new PharmacistMarkedProductViewModel();
            MarkedProductsTable.ItemsSource = _pharmacistVM.MarkedProducts;
        }

        private void ShowMarkedProductDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            var markedProduct = (MarkedProductTableVM)MarkedProductsTable.SelectedItem;
            if (markedProduct == null)
            {
                MessageBox.Show("Лекарственное средство не выбрано");
                return;
            }

            new MarkedProductDetailsWindow(_pharmacistVM, markedProduct.Id).Show();
        }
    }
}
