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
using ViewModel.Entities.PharmacyManagerEntities;
using ViewModel.Entities.PharmacyManagerEntities.TableEntities;
using ViewModel.PharmacyManagerViewModels;

namespace SDWPFView.PharmacyManagerViews
{
    public partial class PharmacyManagerMarkedProductsControl : UserControl
    {
        private PharmacyManagerMarkedProductsViewModel _pharmacyManagerVM;

        public PharmacyManagerMarkedProductsControl()
        {
            InitializeComponent();
            _pharmacyManagerVM = new PharmacyManagerMarkedProductsViewModel();
            MarkedProductsTable.ItemsSource = _pharmacyManagerVM.MarkedProducts;
        }

        private void WriteOffProductBtn_Click(object sender, RoutedEventArgs e)
        {
            var markedProduct = (MarkedProductTableVM)MarkedProductsTable.SelectedItem;
            if (markedProduct == null)
            {
                MessageBox.Show("Лекарственное средство не выбрано");
                return;
            }

            new WriteOffProductWindow(_pharmacyManagerVM, markedProduct.Id).Show();
        }
    }
}
