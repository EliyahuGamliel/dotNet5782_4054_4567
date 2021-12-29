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
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        private IBL bl = BlFactory.GetBl();
        private Customer cu;

        public CustomerPage(Customer customer = null) {
            InitializeComponent();
            if (customer == null) {
                cu = new Customer();
                cu.Location = new Location();
                action1.Content = "Add Customer";
                action1.Click += new RoutedEventHandler(Add_Click);
                this.DataContext = cu;
            }
            else {
                cu = customer;
                this.DataContext = cu;
                InitializeData();
            }
        }

        /// <summary>
        /// Initialise all the data and some of the graphics
        /// </summary>
        private void InitializeData() {
            //CustomerForListView.ItemsSource = cu.ForCustomer;
            //CustomerFromListView.ItemsSource = cu.FromCustomer;

            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Status");
            CollectionView view1 = (CollectionView)CollectionViewSource.GetDefaultView(cu.ForCustomer);
            CollectionView view2 = (CollectionView)CollectionViewSource.GetDefaultView(cu.FromCustomer);
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
            MessageBox.Show(bl.UpdateCustomer(cu.Id.Value, nameCustomer.Text, phoneCustomer.Text));
        }

        /// <summary>
        /// If the add button has been pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e) {
            try {
                MessageBox.Show(bl.AddCustomer(cu));
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