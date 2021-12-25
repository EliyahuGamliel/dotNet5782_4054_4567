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
    /// Interaction logic for ParcelPage.xaml
    /// </summary>
    public partial class ParcelPage : Page
    {
        private BO.Parcel pa;
        private BlApi.IBL bl = BlApi.BlFactory.GetBl();

        public ParcelPage(int? id) {
            InitializeComponent();
        }

        public ParcelPage(BO.Parcel parcel) {
            InitializeComponent();
            targetIdParcel.ItemsSource = bl.GetCustomers();
            senderIdParcel.ItemsSource = bl.GetCustomers();
            weightParcel.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            priorityParcel.ItemsSource = Enum.GetValues(typeof(BO.Priorities));
            pa = parcel;
            InitializeData();
        }

        /// <summary>
        /// Initialise all the data and some of the graphics
        /// </summary>
        private void InitializeData() {
            pa = bl.GetParcelById(pa.Id);
            this.DataContext = pa;

        }

        /// <summary>
        /// If the update button has been pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e) {

        }

        /// <summary>
        /// If the users wants to go back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e) {
            this.NavigationService.GoBack();
        }
    }
}
