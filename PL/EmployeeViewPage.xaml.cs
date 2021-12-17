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
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for EmployeeViewPage.xaml
    /// </summary>
    public partial class EmployeeViewPage : Page
    {
        private IBL bl;
        internal MainPage mPage;
        public EmployeeViewPage(MainPage mainPage)
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
            mPage = mainPage;
        }
      
        /// <summary>
        /// When the show drone button has been pressed it navigates to the "DroneListPage"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DroneList(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DroneListPage());
        }


        private void CustomerList(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CustomerListPage());
        }

        private void StationList(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new StationListPage());
        }

        private void ParcelList(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ParcelListPage());
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            mPage.mWindow.ExitBonus = true;
            this.NavigationService.GoBack();
        }

        /// <summary>
        /// Makes sure that the gif keep running over and over again
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Again_MediaEnded(object sender, RoutedEventArgs e)
        {
            Gif.Position = new TimeSpan(0, 0, 1);
            Gif.Play();
        }
    }
}
