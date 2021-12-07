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
    /// Interaction logic for DronePage.xaml
    /// </summary>
    public partial class DronePage : Page
    {
        static DroneList droneList = new DroneList();
        static Drone dr;
        static Parcel pa;
        static BlApi.IBL blDrone;
        private DroneListPage dlPage;

        /// <summary>
        /// The first ctor (Drone Actions)
        /// </summary>
        /// <param name="bl">Data Base</param>
        /// <param name="drone">The drone to make on it actions</param>
        /// <param name="droneListPage">Pointer to the Drone List Page</param>
        public DronePage(BlApi.IBL bl, Drone drone, DroneListPage droneListPage)
        {
            InitializeComponent();
            blDrone = bl;
            dlPage = droneListPage;
            dr = drone;
            maxWeightDrone.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            if (dr.Status == DroneStatuses.Delivery)
                pa = blDrone.GetParcelById(dr.PTransfer.Id);
            InitializeData();
            InitializeButtons();
        }

        /// <summary>
        /// The second constructor (Drone Add)
        /// </summary>
        /// <param name="bl">Data Base</param>
        /// <param name="droneListPage">Pointer to the Drone List Page</param>
        public DronePage(BlApi.IBL bl, DroneListPage droneListPage)
        {
            InitializeComponent();
            blDrone = bl;
            dlPage = droneListPage;

            droneWithoutParcel.Visibility = Visibility.Visible;
            DroneAddGrid.Visibility = Visibility.Visible;
            idDrone.Background = Brushes.Red;
            modelDrone.Background = Brushes.Red;
            action1.IsEnabled = false;

            maxWeightDrone.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            maxWeightDrone.SelectedIndex = 0;
            idStationToChrging.ItemsSource = bl.GetStationCharge();
            idStationToChrging.SelectedIndex = 0;

            parcelDrone.Text = "not exist";

            action2.Visibility = Visibility.Hidden;
            updateDrone.Visibility = Visibility.Hidden;
            
            action1.Content = "Add Drone";
            action1.Click += new RoutedEventHandler(Add_Click);
        }

        /// <summary>
        /// Initialise all the data and some of the graphics
        /// </summary>
        private void InitializeData()
        {
            dr = blDrone.GetDroneById(dr.Id);

            idDrone.IsEnabled = false;
            maxWeightDrone.IsEnabled = false;

            idDrone.Text = dr.Id.ToString();
            modelDrone.Text = dr.Model;
            batteryDrone.Text = Math.Round(dr.Battery, 0).ToString() + "%";
            maxWeightDrone.SelectedIndex = maxWeightDrone.Items.IndexOf(dr.MaxWeight);
            statusDrone.Text = dr.Status.ToString();
            updateDrone.IsEnabled = false;

            Inline line = locationDrone.Inlines.FirstInline;
            locationDrone.Inlines.Clear();
            locationDrone.Inlines.Add(line);
            locationDrone.Inlines.Add(new Run(dr.CLocation.ToString()));

            //If the choosen drone is in delivery
            if (dr.Status == DroneStatuses.Delivery)
            {
                droneWithoutParcel.Visibility = Visibility.Hidden;
                droneWithParcel.Visibility = Visibility.Visible;
                parcelDrone.Text = dr.PTransfer.ToString();
            }
            else
            {
                droneWithParcel.Visibility = Visibility.Hidden;
                droneWithoutParcel.Visibility = Visibility.Visible;
                parcelDrone.Text = "not exist";
            }
        }

        /// <summary>
        /// Initialise all the buttons of actions
        /// </summary>
        private void InitializeButtons()
        {
            if (dr.Status == DroneStatuses.Delivery)
            {
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
        }

        #region check valid input and the results
        /// <summary>
        /// Check if what captured in the "Id of Drone" filed is valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetId(object sender, RoutedEventArgs e)
        {
            int num;
            bool error = Int32.TryParse(idDrone.Text, out num);
            if (!error)
            {
                idDrone.Background = Brushes.Red;
                action1.IsEnabled = false;
            }
            else
            {
                idDrone.Background = Brushes.White;
                if (modelDrone.Background != Brushes.Red)
                    action1.IsEnabled = true;
            }
        }

        /// <summary>
        /// Changes the button according to if the drone's model changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateModel(object sender, RoutedEventArgs e)
        {
            if (modelDrone.Text == "")
            {
                updateDrone.IsEnabled = false;
                modelDrone.Background = Brushes.Red;
                action1.IsEnabled = false;
            }
            else
            {
                updateDrone.IsEnabled = true;
                modelDrone.Background = Brushes.White;
                if (idDrone.Background != Brushes.Red)
                    action1.IsEnabled = true;
            }
        }
        #endregion

        #region possible functions for buttons (Drone View)
        /// <summary>
        /// If the update button has been pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            dr.Model = modelDrone.Text;
            MessageBox.Show(blDrone.UpdateDrone(dr.Id, dr.Model));
            updateDrone.IsEnabled = false;
        }

        /// <summary>
        /// If the assign button had been pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Assign_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show(blDrone.AssignDroneParcel(dr.Id));
                action2.Visibility = Visibility.Hidden;
                action1.Click -= new RoutedEventHandler(Assign_Click);
                action2.Click -= new RoutedEventHandler(Send_Click);
                ChangePickUP();
                InitializeData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                action1.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// If the pickup button has been pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PickUp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(blDrone.PickUpDroneParcel(dr.Id));
            action1.Click -= new RoutedEventHandler(PickUp_Click);
            ChangeDelivery();
            InitializeData();
        }

        /// <summary>
        /// If the deliver button has been pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Deliver_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(blDrone.DeliverParcelCustomer(dr.Id));
            action1.Click -= new RoutedEventHandler(Deliver_Click);
            ChangeAssignSend();
            InitializeData();
        }

        /// <summary>
        /// If the send button has been pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show(blDrone.SendDrone(dr.Id));
                action2.Click -= new RoutedEventHandler(Send_Click);
                action1.Click -= new RoutedEventHandler(Assign_Click);
                ChangeRelese();
                InitializeData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                action2.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// If the release button has been pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Release_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = Visibility.Visible;
        }
        #endregion

        #region "Window" to get input (time of charge)
        /// <summary>
        /// If the ok button has been pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputTextBox.Background != Brushes.Red)
            {
                // Do something with the Input
                double time = Double.Parse(InputTextBox.Text);
                InputBox.Visibility = Visibility.Hidden;
                // Clear InputBox.
                InputTextBox.Text = String.Empty;
                try
                {
                    MessageBox.Show(blDrone.ReleasDrone(dr.Id, time));
                    action2.Click -= new RoutedEventHandler(Release_Click);
                    ChangeAssignSend();
                    InitializeData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
                MessageBox.Show("Enter valid input (double) or Cancel!", "Valid Input", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// If the cancel button has been pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            //NoButton Clicked! Let's hide our InputBox.
            InputBox.Visibility = Visibility.Hidden;
            //Clear InputBox.
            InputTextBox.Text = String.Empty;
        }

        /// <summary>
        /// Changes the color of the background according to if the time is legal or not - Bonus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetTime(object sender, RoutedEventArgs e)
        {
            double num;
            bool error = double.TryParse(InputTextBox.Text, out num);
            if (!error)
                InputTextBox.Background = Brushes.Red;
            else
                InputTextBox.Background = Brushes.White;
        }
        #endregion

        /// <summary>
        /// If the add button has been pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID;
                DroneList droneAdd = new DroneList();
                droneAdd.MaxWeight = (WeightCategories)(int)maxWeightDrone.SelectedItem;
                Int32.TryParse(idDrone.Text, out ID);
                droneAdd.Id = ID;
                StationList st = (StationList)idStationToChrging.SelectedItem;
                droneAdd.Model = modelDrone.Text;
                MessageBox.Show(blDrone.AddDrone(droneAdd, st.Id));
                dlPage.Selector_SelectionChanged();
                this.NavigationService.Navigate(dlPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// If the users wants to go back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            dlPage.Selector_SelectionChanged();
            this.NavigationService.Navigate(dlPage);
        }

        #region change the button(s) according the drone status
        /// <summary>
        /// Changes action1 to be the clickup button
        /// </summary>
        private void ChangePickUP()
        {
            action1.Content = "PickUp the Parcel \n  from Customer";
            action1.Click += new RoutedEventHandler(PickUp_Click);
        }

        /// <summary>
        /// Changes action2 to be the changedelivery  button
        /// </summary>
        private void ChangeDelivery()
        {
            action1.Content = "Deliver Parcel \n by the Drone";
            action1.Click += new RoutedEventHandler(Deliver_Click);
        }

        /// <summary>
        /// Makes action1 and action2 to visible and clickable buttons and changes them to be drone to parcel assigning
        ///and sending drone for charging buttons
        /// </summary>
        private void ChangeAssignSend()
        {
            action1.Visibility = Visibility.Visible;
            action1.Content = "Assign the Drone \n      to Parcel";
            action1.Click += new RoutedEventHandler(Assign_Click);
            action2.Visibility = Visibility.Visible;
            action2.Content = "Send the Drone \n  to Charging";
            action2.Click += new RoutedEventHandler(Send_Click);
        }

        /// <summary>
        /// Hides action1 and changes action2 to drone releasing button
        /// </summary>
        private void ChangeRelese()
        {
            action1.Visibility = Visibility.Hidden;
            action2.Content = "Release the Drone \n   from Charging";
            action2.Click += new RoutedEventHandler(Release_Click);
        }
        #endregion
    }
}
