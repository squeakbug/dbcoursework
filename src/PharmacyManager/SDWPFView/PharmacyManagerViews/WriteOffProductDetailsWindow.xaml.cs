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
using ViewModel.Entities.PharmacyManagerEntities;
using ViewModel.PharmacyManagerViewModels;

namespace SDWPFView.PharmacyManagerViews
{
    public partial class WriteOffProductDetailsWindow : Window
    {
        private PharmacyManagerWriteOffProductViewModel _pharmacyManagerVM;
        private string _writeOffProductId;

        public WriteOffProductDetailsWindow(PharmacyManagerWriteOffProductViewModel pharmacyManagerVM,
            string writeOffProductId)
        {
            InitializeComponent();
            _pharmacyManagerVM = pharmacyManagerVM;
            _writeOffProductId = writeOffProductId;

            WriteOffProductDetailsVM writeOffProductDetails =
                _pharmacyManagerVM.GetWriteOffProductDetailsById(writeOffProductId);

            CountTextBox.Text = writeOffProductDetails.Count;
            ProductNameTextBox.Text = writeOffProductDetails.ProductName;
            StorageLocationTextBox.Text = writeOffProductDetails.StorageLocation;
            WriteOffDateTextBox.Text = writeOffProductDetails.WriteOffDate;
            ReasonRichTextBox.Document.Blocks.Clear();
            ReasonRichTextBox.Document.Blocks.Add(new Paragraph(new Run(writeOffProductDetails.Reason)));
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ShowProductDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            WriteOffProductVM writeOffProductVM = _pharmacyManagerVM.GetWriteOffProductById(_writeOffProductId);
            MarkInfoVM markInfoVM = _pharmacyManagerVM.GetMarkInfoById(writeOffProductVM.Mark_info_id);
            IncomeProductVM incomeProductVM = _pharmacyManagerVM.GetIncomeProductById(markInfoVM.Income_product_id);
            new ProductDetailsWindow(_pharmacyManagerVM, incomeProductVM.Product_id).Show();
        }
    }
}
