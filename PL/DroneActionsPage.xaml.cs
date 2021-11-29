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
        static Parcel pa;
        static IBL.IBL blDroneActions;
        private DroneListPage dlPage;
        public DroneActionsPage(IBL.IBL bl, Drone drone, DroneListPage droneListPage)
        {
            InitializeComponent();
            blDroneActions = bl;
            dlPage = droneListPage;
            dr = drone;
            if (dr.Status == DroneStatuses.Delivery)
                pa = blDroneActions.GetParcelById(dr.PTransfer.Id);
            InitializeButtons();
            InitializeData();
        }

        private void InitializeData()
        {
            idDrone.Text = dr.Id.ToString();
            modelDrone.Text = dr.Model;
            updateDrone.IsEnabled = false;
            batteryDrone.Text = dr.Battery.ToString();
            maxWightDrone.Text = dr.MaxWeight.ToString();
            statusDrone.Text = dr.Status.ToString();
            locationDrone.Text = "\tLattitude: " + dr.CLocation.Lattitude.ToString() + "\n\tLongitude: " + dr.CLocation.Longitude.ToString();
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
            else
            {
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
            MessageBox.Show(blDroneActions.UpdateDrone(dr.Id, dr.Model));
            updateDrone.IsEnabled = false;
        }
        
        private void Assign_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show(blDroneActions.AssignDroneParcel(dr.Id));
                assignDrone.IsEnabled = false;
                pickUpDrone.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        private void PickUp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(blDroneActions.PickUpDroneParcel(dr.Id));
            pickUpDrone.IsEnabled = false;
            deliverDrone.IsEnabled = true;
        }
        
        private void Deliver_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(blDroneActions.DeliverParcelCustomer(dr.Id));
            deliverDrone.IsEnabled = false;
            sendDrone.IsEnabled = true;
            assignDrone.IsEnabled = true;
        }
        
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show(blDroneActions.SendDrone(dr.Id));
                releaseDrone.IsEnabled = true;
                assignDrone.IsEnabled = false;
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
            MessageBox.Show(blDroneActions.ReleasDrone(dr.Id, 1));
            releaseDrone.IsEnabled = false;
            assignDrone.IsEnabled = true;
            sendDrone.IsEnabled = true;
        }
    }
}
