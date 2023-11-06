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
    public partial class AddProductToReceiptWindow : Window
    {
        private PharmacistReceiptFormerViewModel _pharmacistVM;

        public AddProductToReceiptWindow(PharmacistReceiptFormerViewModel pharmacistVM)
        {
            InitializeComponent();
            _pharmacistVM = pharmacistVM;
            _pharmacistVM.LoadMarkedProducts();
            MarkedProductTable.ItemsSource = _pharmacistVM.MarkedProducts;
        }

        private void ShowMarkedProductDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            var markedProduct = (MarkedProductTableVM)MarkedProductTable.SelectedItem;
            if (markedProduct == null)
            {
                MessageBox.Show("Лекарственное средство не выбрано");
                return;
            }

            new MarkedProductDetailsWindow(_pharmacistVM, markedProduct.Id).Show();
        }

        private void AddProductBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = (MarkedProductTableVM)MarkedProductTable.SelectedItem;
            if (selectedProduct == null)
            {
                MessageBox.Show("Не выбрано лекарственное средство");
                return;
            }
            string cnt = CountTextBox.Text;

            try
            {
                _pharmacistVM.AddMarkedProduct(selectedProduct.Id, cnt);
            }
            catch (ApplicationException ex)
            {
                _pharmacistVM.UpdateStatus("Ошибка при добавлении ЛС в чек");
                MessageBox.Show(ex.Message);
                return;
            }
            Close();
        }

        private void CancelProductBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
