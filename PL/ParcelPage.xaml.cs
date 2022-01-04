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
    /// Interaction logic for ParcelPage.xaml
    /// </summary>
    public partial class ParcelPage : Page
    {
        private Parcel pa;
        private IBL bl = BlFactory.GetBl();

        public ParcelPage(Parcel parcel = null) {
            InitializeComponent();

            targetIdParcel.ItemsSource = bl.GetCustomers();
            senderIdParcel.ItemsSource = bl.GetCustomers();
            weightParcel.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            priorityParcel.ItemsSource = Enum.GetValues(typeof(Priorities));

            if (parcel == null) {
                pa = new Parcel();
                pa.Requested = DateTime.Now;
                action1.Content = "Add Parcel";
                action1.Click += new RoutedEventHandler(Add_Click);
            }
            else {
                pa = parcel;
                weightParcel.IsEnabled = false;
                action1.Content = "Update Parcel";
                action1.Click += new RoutedEventHandler(Update_Click);
            }
            this.DataContext = pa;
        }

        /// <summary>
        /// Initialise all the data and some of the graphics
        /// </summary>
        private void UpdateParcel(object sender, RoutedEventArgs e) {
            pa = bl.GetParcelById(pa.Id.Value);
            this.DataContext = pa;
        }

        private void Update_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show(bl.UpdateParcel(pa.Id.Value, pa.Priority));
        }

        private void Add_Click(object sender, RoutedEventArgs e) {
            try {
                CustomerList targetC = (CustomerList)targetIdParcel.SelectedItem;
                CustomerList senderC = (CustomerList)senderIdParcel.SelectedItem;
                MessageBox.Show(bl.AddParcel(pa, senderC.Id, targetC.Id));
                this.NavigationService.GoBack();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TargetInParcel(object sender, RoutedEventArgs e) {
            CustomerPage customerPage = new CustomerPage(bl.GetCustomerById(pa.Target.Id));
            customerPage.Unloaded += UpdateParcel;
            this.NavigationService.Navigate(customerPage);
        }

        private void SenderInParcel(object sender, RoutedEventArgs e) {
            CustomerPage customerPage = new CustomerPage(bl.GetCustomerById(pa.Sender.Id));
            customerPage.Unloaded += UpdateParcel;
            this.NavigationService.Navigate(customerPage);
        }

        private void DroneInParcel(object sender, RoutedEventArgs e) {
            DroneWindow dronePage = new DroneWindow(bl.GetDroneById(pa.Drone.Id));
            dronePage.Unloaded += UpdateParcel;
            dronePage.ShowDialog();
        }

        /// <summary>
        /// If the users wants to go back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e) {
            if (this.NavigationService.CanGoBack)
                this.NavigationService.GoBack();
            else
                this.Content = null;
        }
    }
}
