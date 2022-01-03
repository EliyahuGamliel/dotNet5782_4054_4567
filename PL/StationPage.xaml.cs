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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for StationPage.xaml
    /// </summary>
    public partial class StationPage : Page
    {
        private IBL bl = BlFactory.GetBl();
        private Station st;
        public StationPage(Station station = null) {
            InitializeComponent();
            if (station == null) {
                st = new Station();
                st.Location = new Location();
                action1.Content = "Add Station";
                action1.Click += new RoutedEventHandler(Add_Click);
                this.DataContext = st;
            }
            else {
                st = station;
                this.DataContext = st;
                InitializeData();
            }
        }

        private void InitializeData() {

            idStation.IsEnabled = false;
            longStation.IsEnabled = false;
            latiStation.IsEnabled = false;

            action1.Content = "Update Station";
            action1.Click += new RoutedEventHandler(Update_Click);
        }

        private void Update_Click(object sender, RoutedEventArgs e) {
            try {
                MessageBox.Show(bl.UpdateStation(st.Id.Value, nameStation.Text, st.ChargeSlots.Value));
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e) {
            try {
                MessageBox.Show(bl.AddStation(st));
                this.NavigationService.GoBack();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// If the users wants to go back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e) {
            this.NavigationService.GoBack();
        }

        private void OpenDrone(object sender, MouseButtonEventArgs e) {
            if (droneStationListView.SelectedItem != null) {
                DroneCharge droneCharge = (DroneCharge)droneStationListView.SelectedItem;
                DronePage dronePage = new DronePage(bl.GetDroneById(droneCharge.Id));
                dronePage.Unloaded += UpdateStation;
                this.NavigationService.Navigate(dronePage);
            }
        }

        private void UpdateStation(object sender = null, EventArgs e = null) {
            st = bl.GetStationById(st.Id.Value);
            this.DataContext = st;
        }
    }
}
