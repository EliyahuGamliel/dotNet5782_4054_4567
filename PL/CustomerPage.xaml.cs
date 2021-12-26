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
        private BO.Customer cu;

        private int numInt;
        private double numDouble;

        public CustomerPage(BO.Customer customer, CustomerListPage customerListPage) {
            InitializeComponent();
            cu = customer;
            this.DataContext = cu;
            InitializeData();
        }

        /// <summary>
        /// The second constructor (Drone Add)
        /// </summary>
        /// <param name="bl">Data Base</param>
        /// <param name="droneListPage">Pointer to the Drone List Page</param>
        public CustomerPage(CustomerListPage customerListPage) {
            InitializeComponent();

            action1.Content = "Add Customer";
            action1.Click += new RoutedEventHandler(Add_Click);
        }

        /// <summary>
        /// Initialise all the data and some of the graphics
        /// </summary>
        private void InitializeData() {
            CustomerForListView.ItemsSource = cu.ForCustomer;
            CustomerFromListView.ItemsSource = cu.FromCustomer;

            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Status");
            CollectionView view1 = (CollectionView)CollectionViewSource.GetDefaultView(CustomerForListView.ItemsSource);
            CollectionView view2 = (CollectionView)CollectionViewSource.GetDefaultView(CustomerFromListView.ItemsSource);
            view1.GroupDescriptions.Add(groupDescription);
            view2.GroupDescriptions.Add(groupDescription);

            idCustomer.IsEnabled = false;
            longCustomer.IsEnabled = false;
            latiCustomer.IsEnabled = false;

            action1.Content = "Update Customer";
            action1.Click += new RoutedEventHandler(Update_Click);
        }

        /// <summary>
        /// If the update button has been pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show(bl.UpdateCustomer(cu.Id, nameCustomer.Text, phoneCustomer.Text));
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
        private void Exit_Click(object sender, RoutedEventArgs e) {
            this.NavigationService.GoBack();
        }

        private void OpenParcel(object sender, MouseButtonEventArgs e) {
            if ((sender as ListView).SelectedItem != null) {
                BO.ParcelInCustomer p = (BO.ParcelInCustomer)(sender as ListView).SelectedItem;
                this.NavigationService.Navigate(new ParcelPage(bl.GetParcelById(p.Id)));
            }
        }
    }
}