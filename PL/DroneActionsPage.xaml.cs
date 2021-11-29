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
using IBL.BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneActionsPage.xaml
    /// </summary>
    public partial class DroneActionsPage : Page
    {
        static Drone dr;
        private DroneListPage dlPage;
        public DroneActionsPage(Drone drone, DroneListPage droneListPage)
        {
            InitializeComponent();
            dlPage = droneListPage;
            dr = drone;
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(dlPage);
        }

        private void Updata_Click(object sender, RoutedEventArgs e)
        {
            DroneListView.ItemsSource = blDroneList.GetDroneByFilter(d => true);
            StatusSelector.SelectedItem = null;
            MaxWeightSelector.SelectedItem = null;
        }
    }
}
