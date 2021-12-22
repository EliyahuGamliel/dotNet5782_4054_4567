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

namespace PLCustomer
{
    
    /// <summary>
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        private BlApi.IBL bl = BlApi.BlFactory.GetBl();
        private BO.Customer cu;
        private MainPage clPage;

        private int numInt;
        private double numDouble;

        /// <summary>
        /// The second constructor (Drone Add)
        /// </summary>
        /// <param name="bl">Data Base</param>
        /// <param name="droneListPage">Pointer to the Drone List Page</param>
        public CustomerPage(MainPage customerListPage) {
            InitializeComponent();
            clPage = customerListPage;

            updateCustomer.Visibility = Visibility.Hidden;

            idCustomer.Background = Brushes.Red;
            nameCustomer.Background = Brushes.Red;
            longCustomer.Background = Brushes.Red;
            latiCustomer.Background = Brushes.Red;
            phoneCustomer.Background = Brushes.Red;

            action1.IsEnabled = false;
            phoneCustomer.Text = "+972-5????????";

            action1.Content = "Add Customer";
            action1.Click += new RoutedEventHandler(Add_Click);
        }

        private void CheckAddCustomer() {
            if (new[] { nameCustomer, idCustomer, phoneCustomer, latiCustomer, longCustomer }.All(x => x.Background != Brushes.Red))
                action1.IsEnabled = true;
            else
                action1.IsEnabled = false;
        }

        /// <summary>
        /// Check if what captured in the "Id of Drone" filed is valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetId(object sender, RoutedEventArgs e) {
            bool error = Int32.TryParse(idCustomer.Text, out numInt);
            if (!error)
                idCustomer.Background = Brushes.Red;
            else
                idCustomer.Background = Brushes.White;
            CheckAddCustomer();
        }

        /// <summary>
        /// Changes the button according to if the drone's model changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetName(object sender, RoutedEventArgs e) {
            if (nameCustomer.Text == "") {
                updateCustomer.IsEnabled = false;
                nameCustomer.Background = Brushes.Red;
            }
            else {
                updateCustomer.IsEnabled = true;
                nameCustomer.Background = Brushes.White;
            }
            CheckAddCustomer();
        }

        private void GetPhone(object sender, TextChangedEventArgs e) {
            string num = phoneCustomer.Text;
            int check;
            bool error = true;
            if (num.Length != 14 || num[0] != '+' || num[1] != '9' || num[2] != '7' || num[3] != '2' || num[4] != '-' || num[5] != '5')
                error = false;
            else {
                string output = num.Substring(num.IndexOf("+") + 6, 4);
                error = Int32.TryParse(output, out check);
                if (error) {
                    output = num.Substring(num.IndexOf("+") + 10, 4);
                    error = Int32.TryParse(output, out check);
                }
            }
            if (phoneCustomer.Text == "")
                phoneCustomer.Text = "+972-5????????";
            else if (!error) {
                phoneCustomer.Background = Brushes.Red;
            }
            else
                phoneCustomer.Background = Brushes.White;
            CheckAddCustomer();
        }

        private void GetLong(object sender, TextChangedEventArgs e) {
            bool error = Double.TryParse(longCustomer.Text, out numDouble);
            if (!error)
                longCustomer.Background = Brushes.Red;
            else
                longCustomer.Background = Brushes.White;
            CheckAddCustomer();
        }

        private void GetLati(object sender, TextChangedEventArgs e) {
            bool error = Double.TryParse(latiCustomer.Text, out numDouble);
            if (!error)
                latiCustomer.Background = Brushes.Red;
            else
                latiCustomer.Background = Brushes.White;
            CheckAddCustomer();
        }

        /// <summary>
        /// If the update button has been pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show(bl.UpdateCustomer(cu.Id, nameCustomer.Text, phoneCustomer.Text));
            updateCustomer.IsEnabled = false;
        }

        /// <summary>
        /// If the add button has been pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e) {
            try {
                BO.Customer customerAdd = new BO.Customer();
                customerAdd.Location = new BO.Location();
                Int32.TryParse(idCustomer.Text, out numInt);
                customerAdd.Id = numInt;
                customerAdd.Name = nameCustomer.Text;
                customerAdd.Phone = phoneCustomer.Text;
                Double.TryParse(longCustomer.Text, out numDouble);
                customerAdd.Location.Longitude = numDouble;
                Double.TryParse(latiCustomer.Text, out numDouble);
                customerAdd.Location.Lattitude = numDouble;
                MessageBox.Show(bl.AddCustomer(customerAdd));
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
        private void Cancel_Click(object sender, RoutedEventArgs e) {
            this.NavigationService.GoBack();
        }


        private void ParcelInDrone(object sender, RoutedEventArgs e) {

        }

        private void OpenParcel(object sender, MouseButtonEventArgs e) {

        }
    }
}
