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
        public DronePage(IBL.IBL bl, DroneListPage droneListPage)
        {
            InitializeComponent();
            DroneListGrid.Visibility = Visibility.Hidden;
            blDrone = bl;
            dlPage = droneListPage;
            maxWeight.ItemsSource = Enum.GetValues(typeof(WeightCategories));
        }
        private void InitializeData()
        {
            dr = blDrone.GetDroneById(dr.Id);
            idDrone.Text = dr.Id.ToString();
            modelDrone.Text = dr.Model;
            updateDrone.IsEnabled = false;
            batteryDrone.Text = Math.Round(dr.Battery, 3).ToString() + "%";
            maxWightDrone.Text = dr.MaxWeight.ToString();
            statusDrone.Text = dr.Status.ToString();
            Inline line = locationDrone.Inlines.FirstInline;
            locationDrone.Inlines.Clear();
            locationDrone.Inlines.Add(line);
            locationDrone.Inlines.Add(new Run(dr.CLocation.ToString()));
            if (dr.Status == DroneStatuses.Delivery)
                parcelDrone.Text = dr.PTransfer.ToString();
            else
                parcelDrone.Text = "not exist";
        }
        private void InitializeButtons()
        {
            if (dr.Status == DroneStatuses.Delivery)
                if (pa.PickedUp == null)
                    pickUpDrone.IsEnabled = true;
                else
                    deliverDrone.IsEnabled = true;
            else if (dr.Status == DroneStatuses.Maintenance)
                releaseDrone.IsEnabled = true;
            else {
                assignDrone.IsEnabled = true;
                sendDrone.IsEnabled = true;
            }
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            dlPage.Selector_SelectionChanged();
            this.NavigationService.Navigate(dlPage);
        }
        private void GetModel(object sender, RoutedEventArgs e)
        {
            updateDrone.IsEnabled = true;
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            dr.Model = modelDrone.Text;
            MessageBox.Show(blDrone.UpdateDrone(dr.Id, dr.Model));
            updateDrone.IsEnabled = false;
        }
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
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                assignDrone.IsEnabled = false;
            }
        }
        private void PickUp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(blDrone.PickUpDroneParcel(dr.Id));
            pickUpDrone.IsEnabled = false;
            deliverDrone.IsEnabled = true;
            InitializeData();
        }

        private void Deliver_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(blDrone.DeliverParcelCustomer(dr.Id));
            deliverDrone.IsEnabled = false;
            sendDrone.IsEnabled = true;
            assignDrone.IsEnabled = true;
            InitializeData();
        }

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
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sendDrone.IsEnabled = false;
            }

        }

        private void Release_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show(blDrone.ReleasDrone(dr.Id, 1));
            releaseDrone.IsEnabled = false;
            assignDrone.IsEnabled = true;
            sendDrone.IsEnabled = true;
            InitializeData();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                droneList.Model = modelDrone.Text;
                MessageBox.Show(blDrone.AddDrone(droneList, idStation));
                dlPage.Selector_SelectionChanged();
                this.NavigationService.Navigate(dlPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void GetIdStationToChrging(object sender, RoutedEventArgs e)
        {
            GetInt(idStationToChrging);
            if (idStationToChrging.Background == Brushes.White)
                Int32.TryParse(idStationToChrging.Text, out idStation);
        }

        private void GetMaxWeight(object sender, RoutedEventArgs e)
        {
            droneList.MaxWeight = (WeightCategories)(int)maxWeight.SelectedItem;
        }

        private void GetId(object sender, RoutedEventArgs e)
        {
            GetInt(idone);
            if (idDrone.Background == Brushes.White)
            {
                int id;
                Int32.TryParse(idDrone.Text, out id);
                droneList.Id = id;
            }
        }

        private void GetInt(TextBox tBox)
        {
            int num;
            bool error = Int32.TryParse(tBox.Text, out num);
            if (!error)
                tBox.Background = Brushes.Red;
            else
                tBox.Background = Brushes.White;
        }
    }
}
