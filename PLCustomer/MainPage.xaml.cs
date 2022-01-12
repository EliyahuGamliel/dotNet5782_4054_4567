using System;
using System.Windows;
using System.Windows.Controls;
using BlApi;

namespace PLCustomer
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private IBL bl = BlFactory.GetBl();

        public MainPage() {
            InitializeComponent();
        }

        private void SignUp(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new CustomerPage());
        }

        private void SignIn(object sender, RoutedEventArgs e) {
            int iD;
            Int32.TryParse(Username.Text, out iD);
            try {
                this.NavigationService.Navigate(new CustomerPage(bl.GetCustomerById(iD)));
                Username.Text = "";
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
