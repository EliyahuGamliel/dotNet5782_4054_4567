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
            if (parcel == null) {
                pa = new Parcel();

                action1.Content = "Add Parcel";
                action1.Click += new RoutedEventHandler(Add_Click);
            }
            else {
                pa = parcel;

                action1.Content = "Update Parcel";
                action1.Click += new RoutedEventHandler(Update_Click);
            }
            targetIdParcel.ItemsSource = bl.GetCustomers();
            senderIdParcel.ItemsSource = bl.GetCustomers();
            weightParcel.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            priorityParcel.ItemsSource = Enum.GetValues(typeof(BO.Priorities));
            pa = parcel;
            //InitializeData();
            this.DataContext = pa;
        }

        /// <summary>
        /// Initialise all the data and some of the graphics
        /// </summary>
        private void InitializeData() {
            pa = bl.GetParcelById(pa.Id);
            

        }

        private void Update_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show(bl.UpdateParcel(pa.Id, pa.Priority));
        }

        private void Add_Click(object sender, RoutedEventArgs e) {
            try {
                MessageBox.Show(bl.AddParcel(pa, pa.Sender.Id, pa.Target.Id));
                this.NavigationService.GoBack();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ParcelInDrone(object sender, RoutedEventArgs e) {

        }

        private void DroneInParcel(object sender, RoutedEventArgs e) {

        }

        /// <summary>
        /// If the users wants to go back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e) {
            this.NavigationService.GoBack();
        }

        private void DroneInParcel(object sender, MouseEventArgs e) {

        }
    }
}
