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

namespace PL
{
    /// <summary>
    /// Interaction logic for StationListPage.xaml
    /// </summary>
    public partial class StationListPage : Page
    {
        static IBL.IBL blStationList;
        private DronePage dPage;

        /// <summary>
        /// The ctor
        /// </summary>
        /// <param name="dronePage">Pointer to Drone Page</param>
        public StationListPage(IBL.IBL bl, DronePage dronePage)
        {
            InitializeComponent();
            blStationList = bl;
            StationListView.ItemsSource = blStationList.GetStations();
            dPage = dronePage;
        }

        /// <summary>
        /// If the user wants to go back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(dPage);
        }
    }
}
