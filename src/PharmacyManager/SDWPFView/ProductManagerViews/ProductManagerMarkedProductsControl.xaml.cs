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

using BusinessSpecification.Entities;
using ViewModel;
using ViewModel.Entities;
using ViewModel.Entities.ProductManagerEntities;
using ViewModel.Entities.ProductManagerEntities.TableEntities;
using ViewModel.ProductManagerViewModels;

namespace SDWPFView.ProductManagerViews
{
    public partial class ProductManagerMarkedProductsControl : UserControl
    {
        private ProductManagerMarkedProductsViewModel _productManagerVM;

        public ProductManagerMarkedProductsControl()
        {
            InitializeComponent();
            _productManagerVM = new ProductManagerMarkedProductsViewModel();
            MarkedProductsTable.ItemsSource = _productManagerVM.MarkedProducts;
        }

        private void EditMarkedProductBtn_Click(object sender, RoutedEventArgs e)
        {
            var markedProduct = (MarkedProductTableVM)MarkedProductsTable.SelectedItem;
            if (markedProduct == null)
            {
                _productManagerVM.UpdateStatus("Товар не выбран");
                return;
            }

            new EditMarkedProductWindow(_productManagerVM, markedProduct.Id.ToString()).Show();
        }
    }
}
