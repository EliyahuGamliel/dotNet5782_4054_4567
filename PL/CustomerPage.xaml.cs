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
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        private BlApi.IBL bl = BlApi.BlFactory.GetBl();
        private CustomerListPage clPage;

        public CustomerPage(BO.Customer customer, CustomerListPage customerListPage) {
            InitializeComponent();
            clPage = customerListPage;
            //maxWeightDrone.ItemsSource = ;
            InitializeData();
            InitializeButtons();
            this.DataContext = this;
        }

        /// <summary>
        /// The second constructor (Drone Add)
        /// </summary>
        /// <param name="bl">Data Base</param>
        /// <param name="droneListPage">Pointer to the Drone List Page</param>
        public CustomerPage(CustomerListPage customerListPage) {
            InitializeComponent();
            clPage = customerListPage;

            idCustomer.Background = Brushes.Red;
            nameCustomer.Background = Brushes.Red;
            action1.IsEnabled = false;
            /*
            maxWeightDrone.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            maxWeightDrone.SelectedIndex = 0;
            idStationToChrging.ItemsSource = bl.GetStationCharge();
            idStationToChrging.SelectedIndex = 0;
            parcelDrone.Text = "\n      not exist";

            action2.Visibility = Visibility.Hidden;
            updateDrone.Visibility = Visibility.Hidden;
            */
            action1.Content = "Add Customer";
            action1.Click += new RoutedEventHandler(Add_Click);
        }

        /// <summary>
        /// Initialise all the data and some of the graphics
        /// </summary>
        private void InitializeData() {
            /*
            dr = bl.GetDroneById(dr.Id);

            idDrone.IsEnabled = false;
            maxWeightDrone.IsEnabled = false;

            idDrone.Text = dr.Id.ToString();
            //modelDrone.Text = dr.Model;
            batteryDrone.Text = Math.Round(dr.Battery, 0).ToString() + "%";
            maxWeightDrone.SelectedIndex = maxWeightDrone.Items.IndexOf(dr.MaxWeight);
            statusDrone.Text = dr.Status.ToString();
            locationDrone.Text = dr.CLocation.ToString();
            updateDrone.IsEnabled = false;
            //If the choosen drone is in delivery
            if (dr.Status == DroneStatuses.Delivery) {
                parcelInDrone.IsEnabled = true;
                parcelDrone.Text = dr.PTransfer.ToString();
            }
            else {
                parcelDrone.Text = "\n      not exist";
                parcelInDrone.IsEnabled = false;
            }
            */
        }

        /// <summary>
        /// Initialise all the buttons of actions
        /// </summary>
        private void InitializeButtons() {
            /*
            //If the choosen drone is in delivery
            if (dr.Status == DroneStatuses.Delivery) {
                action2.Visibility = Visibility.Hidden;
                if (pa.PickedUp == null)
                    ChangePickUP();
                else
                    ChangeDelivery();
            }
            else if (dr.Status == DroneStatuses.Maintenance)
                ChangeRelese();
            else
                ChangeAssignSend();
            */
        }

        #region check valid input and the results
        /// <summary>
        /// Check if what captured in the "Id of Drone" filed is valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetId(object sender, RoutedEventArgs e) {
            int num;
            bool error = Int32.TryParse(idCustomer.Text, out num);
            if (!error) {
                idCustomer.Background = Brushes.Red;
                action1.IsEnabled = false;
            }
            else {
                idCustomer.Background = Brushes.White;
                if (nameCustomer.Background != Brushes.Red)
                    action1.IsEnabled = true;
            }
        }

        /// <summary>
        /// Changes the button according to if the drone's model changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateName(object sender, RoutedEventArgs e) {
            if (nameCustomer.Text == "") {
                updateCustomer.IsEnabled = false;
                nameCustomer.Background = Brushes.Red;
                action1.IsEnabled = false;
            }
            else {
                updateCustomer.IsEnabled = true;
                nameCustomer.Background = Brushes.White;
                if (idCustomer.Background != Brushes.Red)
                    action1.IsEnabled = true;
            }
        }
        #endregion

        /// <summary>
        /// If the update button has been pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e) {
            /*
            dr.Model = modelDrone.Text;
            MessageBox.Show(bl.UpdateDrone(dr.Id, dr.Model));
            updateDrone.IsEnabled = false;
            */
        }

        /// <summary>
        /// If the add button has been pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e) {
            /*
            try {
                int ID;
                DroneList droneAdd = new DroneList();
                droneAdd.MaxWeight = (WeightCategories)(int)maxWeightDrone.SelectedItem;
                Int32.TryParse(idDrone.Text, out ID);
                droneAdd.Id = ID;
                StationList st = (StationList)idStationToChrging.SelectedItem;
                droneAdd.Model = modelDrone.Text;
                MessageBox.Show(bl.AddDrone(droneAdd, st.Id));
                dlPage.Selector_SelectionChanged();
                this.NavigationService.GoBack();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            */
        }

        /// <summary>
        /// If the users wants to go back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e) {
            this.NavigationService.GoBack();
        }

        
        private void ParcelInDrone(object sender, RoutedEventArgs e) {

        }

    }
}