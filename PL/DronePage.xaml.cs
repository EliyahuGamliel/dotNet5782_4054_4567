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
    /// Interaction logic for DronePage.xaml
    /// </summary>
    public partial class DronePage : Page
    {
        static DroneList droneList = new DroneList();
        static Drone dr;
        static Parcel pa;
        private int idStation;
        static IBL.IBL blDrone;
        private DroneListPage dlPage;

        /// <summary>
        /// The first ctor (Drone Actions)
        /// </summary>
        /// <param name="bl">Data Base</param>
        /// <param name="drone">The drone to make on it actions</param>
        /// <param name="droneListPage">Pointer to the Drone List Page</param>
        public DronePage(IBL.IBL bl, Drone drone, DroneListPage droneListPage)
        {
            InitializeComponent();
            DroneAddGrid.Visibility = Visibility.Hidden;
            blDrone = bl;
            dlPage = droneListPage;
            dr = drone;
            if (dr.Status == DroneStatuses.Delivery)
                pa = blDrone.GetParcelById(dr.PTransfer.Id);
            InitializeButtons();
            InitializeData();
        }

        /// <summary>
        /// The second constructor (Drone Add)
        /// </summary>
        /// <param name="bl">Data Base</param>
        /// <param name="droneListPage">Pointer to the Drone List Page</param>
        public DronePage(IBL.IBL bl, DroneListPage droneListPage)
        {
            InitializeComponent();
            DroneListGrid.Visibility = Visibility.Hidden;
            blDrone = bl;
            dlPage = droneListPage;
            maxWeight.ItemsSource = Enum.GetValues(typeof(WeightCategories));
        }

        /// <summary>
        /// Initialise all the data and some of the graphics
        /// </summary>
        private void InitializeData()
        {
            dr = blDrone.GetDroneById(dr.Id);

            idDrone.Text = dr.Id.ToString();
            modelDrone.Text = dr.Model;
            batteryDrone.Text = Math.Round(dr.Battery, 3).ToString() + "%";
            maxWightDrone.Text = dr.MaxWeight.ToString();
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
                if (pa.PickedUp == null)
                    pickUpDrone.IsEnabled = true;
                else
                    deliverDrone.IsEnabled = true;
            else if (dr.Status == DroneStatuses.Maintenance)
                releaseDrone.IsEnabled = true;
            else
            {
                assignDrone.IsEnabled = true;
                sendDrone.IsEnabled = true;
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

        /// <summary>
        /// Changes the backgroung according to if its legal or not - Bonus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetModel(object sender, RoutedEventArgs e)
        {
            if (moDrone.Text == "")
                moDrone.Background = Brushes.Red;
            else
                moDrone.Background = Brushes.White;
        }

        /// <summary>
        /// Changes the button according to if the drone's model changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateModel(object sender, RoutedEventArgs e)
        {
            if (modelDrone.Text == "")
                updateDrone.IsEnabled = false;
            else
                updateDrone.IsEnabled = true;
        }

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
                pickUpDrone.IsEnabled = true;
                sendDrone.IsEnabled = false;
                InitializeData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                assignDrone.IsEnabled = false;
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
            pickUpDrone.IsEnabled = false;
            deliverDrone.IsEnabled = true;
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
            deliverDrone.IsEnabled = false;
            sendDrone.IsEnabled = true;
            assignDrone.IsEnabled = true;
            InitializeData();
        }

        /// <summary>
        /// Makes sure the gif is running over and over again
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Again_MediaEnded(object sender, RoutedEventArgs e)
        {
            Gif.Position = new TimeSpan(0, 0, 1);
            Gif.Play();
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
                releaseDrone.IsEnabled = true;
                assignDrone.IsEnabled = false;
                InitializeData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                sendDrone.IsEnabled = false;
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
            DroneListGrid.IsEnabled = false;
        }

        /// <summary>
        /// If the show stations button has been pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowStationsButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new StationListPage(this));
        }

        /// <summary>
        /// If the add button has been pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //If all the filed in "DroneAdd" filled
            if (moDrone.Text != "" && idone.Text != "" && idStationToChrging.Text != "" && maxWeight.SelectedItem != null)
            {
                try
                {
                    DroneList droneAdd = new DroneList();
                    droneAdd.MaxWeight = (WeightCategories)(int)maxWeight.SelectedItem;
                    Int32.TryParse(idone.Text, out idStation);
                    droneAdd.Id = idStation;
                    Int32.TryParse(idStationToChrging.Text, out idStation);
                    droneAdd.Model = moDrone.Text;
                    MessageBox.Show(blDrone.AddDrone(droneAdd, idStation));
                    dlPage.Selector_SelectionChanged();
                    this.NavigationService.Navigate(dlPage);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                MessageBox.Show("Enter data in all fields!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Check if what captured in the "Id Station To Chrging" filed is valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetIdStationToChrging(object sender, RoutedEventArgs e)
        {
            GetInt(idStationToChrging);
        }

        /// <summary>
        /// Changes the background to transparent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetMaxWeight(object sender, RoutedEventArgs e)
        {
            maxWeight.Background = Brushes.Transparent;
        }

        /// <summary>
        /// Check if what captured in the "Id of Drone" filed is valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetId(object sender, RoutedEventArgs e)
        {
            GetInt(idone);
        }

        /// <summary>
        /// Checks if its legal int, if not it changes the color of the background to red - Bonus
        /// </summary>
        /// <param name="tBox"></param>
        private void GetInt(TextBox tBox)
        {
            int num;
            bool error = Int32.TryParse(tBox.Text, out num);
            if (!error)
                tBox.Background = Brushes.Red;
            else
                tBox.Background = Brushes.White;
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
                DroneListGrid.IsEnabled = true;
                // Clear InputBox.
                InputTextBox.Text = String.Empty;
                try
                {
                    MessageBox.Show(blDrone.ReleasDrone(dr.Id, time));
                    releaseDrone.IsEnabled = false;
                    assignDrone.IsEnabled = true;
                    sendDrone.IsEnabled = true;
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
            DroneListGrid.IsEnabled = true;
            //Clear InputBox.
            InputTextBox.Text = String.Empty;
        }
    }
}
