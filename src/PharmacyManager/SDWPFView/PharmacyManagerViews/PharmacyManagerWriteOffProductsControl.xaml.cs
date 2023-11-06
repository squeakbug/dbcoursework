﻿using System;
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
    public partial class PharmacyManagerWriteOffProductsControl : UserControl
    {
        private PharmacyManagerWriteOffProductViewModel _pharmacyManagerVM;

        public PharmacyManagerWriteOffProductsControl()
        {
            InitializeComponent();
            _pharmacyManagerVM = new PharmacyManagerWriteOffProductViewModel();
            WriteOffProductsTable.ItemsSource = _pharmacyManagerVM.WriteOffProducts;
        }

        private void WriteOffProductDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            var writeOffProduct = (WriteOffProductTableVM)WriteOffProductsTable.SelectedItem;
            if (writeOffProduct == null)
            {
                MessageBox.Show("Лекарственное средство не выбрано");
                return;
            }

            new WriteOffProductDetailsWindow(_pharmacyManagerVM, writeOffProduct.Id).Show();
        }
    }
}
