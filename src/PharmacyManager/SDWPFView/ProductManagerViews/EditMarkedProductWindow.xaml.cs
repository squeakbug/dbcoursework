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
using ViewModel.ProductManagerViewModels;

namespace SDWPFView.ProductManagerViews
{
    public partial class EditMarkedProductWindow : Window
    {
        private ProductManagerMarkedProductsViewModel _productManagerVM;
        private readonly string _markedProductId;

        public EditMarkedProductWindow(ProductManagerMarkedProductsViewModel productManagerVM, string markedProductId)
        {
            InitializeComponent();
            _productManagerVM = productManagerVM;
            _markedProductId = markedProductId;

            MarkedProductVM markedProductVM = _productManagerVM.GetMarkedProductById(_markedProductId);
            MarkInfoVM markInfoVM = _productManagerVM.GetMarkInfoById(markedProductVM.Mark_info_id.ToString());

            MarkupTextBox.Text = markInfoVM.markup;
            StorageLocationTextBox.Text = markInfoVM.Storage_location;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UpdateProductBtn_Click(object sender, RoutedEventArgs e)
        {
            MarkedProductVM markedProductVM = _productManagerVM.GetMarkedProductById(_markedProductId);
            MarkInfoVM markInfoVM = _productManagerVM.GetMarkInfoById(markedProductVM.Mark_info_id);

            markInfoVM.markup = MarkupTextBox.Text;
            markInfoVM.Storage_location = StorageLocationTextBox.Text;

            try
            {
                _productManagerVM.UpdateMarkInfo(markInfoVM);
                _productManagerVM.LoadMarkedProducts();
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                _productManagerVM.UpdateStatus("Ошибка при обновлении информации");
                return;
            }
            _productManagerVM.UpdateStatus("Информация обновлена");
            Close();
        }
    }
}
