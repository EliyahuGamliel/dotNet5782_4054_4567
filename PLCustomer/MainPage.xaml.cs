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
using BO;
using BlApi;

namespace PLCustomer
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private IBL bl;

        public MainPage() {
            //Set the value of "CurrentPageBonus" to be "MainPage" to allow the window to close - Bonus
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
        }

        private void SignUp(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new CustomerPage(this));
        }

        private void SignIn(object sender, RoutedEventArgs e) {
            int iD;
            Int32.TryParse(Username.Text, out iD);
            try {
                this.NavigationService.Navigate(new ClientMenu(bl.GetCustomerById(iD), this));
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
