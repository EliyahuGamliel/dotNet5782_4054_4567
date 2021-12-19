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

namespace PL
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private IBL bl;
        internal MainWindow mWindow;

        /// <summary>
        /// The ctor of the main page 
        /// </summary>
        /// <param name="mainWindow">Pointer to the Main Window</param>
        public MainPage(MainWindow mainWindow)
        {
            //Set the value of "CurrentPageBonus" to be "MainPage" to allow the window to close - Bonus
            mainWindow.ExitBonus = true;
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
            mWindow = mainWindow;
        }


        private void SignUp(object sender, RoutedEventArgs e)
        {
            mWindow.ExitBonus = false;
            this.NavigationService.Navigate(new CustomerPage());
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            if (Password.Password == "admin" && Username.Text == "admin")
            {
                mWindow.ExitBonus = false;
                this.NavigationService.Navigate(new EmployeeViewPage(this));
            }
            else if(bl.CustomerPassword(Password.Password, Username.Text))
            {
                mWindow.ExitBonus = false;
                this.NavigationService.Navigate(new CustomerViewPage(this));
            }
            else
            {
                MessageBox.Show("The password is wrong!", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}