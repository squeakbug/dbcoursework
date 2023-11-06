using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;

using SDWPFView;
using SDWPFView.ProductManagerViews;
using SDWPFView.StockmanViews;
using SDWPFView.PharmacyManagerViews;
using SDWPFView.PharmacistViews;
using ViewModel;
using ViewModel.Entities;
using ViewModel.AuthManagerViewModels;

namespace SDWPFView.AuthManagerViews
{
    public partial class AuthWindow : Window
    {
        private AuthManagerViewModel _authManagerVM;

        public AuthWindow()
        {
            InitializeComponent();
            _authManagerVM = new AuthManagerViewModel();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            AuthEmployeeVM employeeVM = null;
            try
            {
                employeeVM = _authManagerVM.Auth(textBoxEmail.Text, passwordBox1.Password);
            }
            catch (ApplicationException ex)
            {
                errormessage.Text = ex.Message;
                return;
            }

            switch (employeeVM.Appointment)
            {
                case "root":
                    new RootDefaultWindow(this).Show();
                    break;
                case "pharmacist":
                    new PharmacistDefaultWindow(this).Show();
                    break;
                case "pharmacy_manager":
                    new PharmacyManagerDefaultWindow(this).Show();
                    break;
                case "product_manager":
                    new ProductManagerDefaultWindow(this).Show();
                    break;
                case "stockman":
                    new StockmanDefaultWindow(this).Show();
                    break;
            }
            Hide();

        }
    }
}
