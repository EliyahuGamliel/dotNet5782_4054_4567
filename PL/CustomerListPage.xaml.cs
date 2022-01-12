using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerListPage.xaml
    /// </summary>
    public partial class CustomerListPage : Page
    {
        private IBL bl = BlFactory.GetBl();
        private ObservableCollection<CustomerList> customerList;

        public CustomerListPage() {
            InitializeComponent();
            customerList = new ObservableCollection<CustomerList>(bl.GetCustomers());
            this.DataContext = customerList;
        }

        private void AddCustomer(object sender, RoutedEventArgs e) {
            CustomerPage customerPage = new CustomerPage();
            customerPage.Unloaded += UpdateList;
            this.NavigationService.Navigate(customerPage);
        }

        private void UpdateList(object sender = null, EventArgs e = null) {
            while (customerList.Count != 0) {
                CustomerList cu = customerList.First();
                customerList.Remove(cu);
            }
            foreach (var item in bl.GetCustomers()) {
                customerList.Add(item);
            }
        }

        /// <summary>
        /// If the user wants to go back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnBack(object sender, RoutedEventArgs e) {
            this.NavigationService.GoBack();
        }

        /// <summary>
        /// Navigates to the "DronaPage" - drone actions page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerActions(object sender, MouseButtonEventArgs e) {
            if (CustomerListView.SelectedItem != null) {
                CustomerList c = (CustomerList)CustomerListView.SelectedItem;
                CustomerPage customerPage = new CustomerPage(bl.GetCustomerById(c.Id));
                customerPage.Unloaded += UpdateList;
                this.NavigationService.Navigate(customerPage);
            }
        }

        private void DeleteCustomer(object sender, RoutedEventArgs e) {
            try {
                CustomerList customer = (CustomerList)CustomerListView.SelectedItem;
                MessageBox.Show(bl.DeleteCustomer(customer.Id));
                UpdateList();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
