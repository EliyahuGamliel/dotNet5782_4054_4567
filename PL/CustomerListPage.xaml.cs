﻿using System;
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
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerListPage.xaml
    /// </summary>
    public partial class CustomerListPage : Page
    {
        private BlApi.IBL bl = BlApi.BlFactory.GetBl();
        private ObservableCollection<BO.CustomerList> customerList;

        public CustomerListPage() {
            InitializeComponent();
            customerList = new ObservableCollection<BO.CustomerList>(bl.GetCustomers());
            this.DataContext = customerList;
        }

        private void AddCustomer(object sender, RoutedEventArgs e) {
            CustomerPage customerPage = new CustomerPage();
            customerPage.Unloaded += UpdateList;
            this.NavigationService.Navigate(customerPage);
        }

        private void UpdateList(object sender = null, EventArgs e = null) {
            customerList.Clear();
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
                BO.CustomerList c = (BO.CustomerList)CustomerListView.SelectedItem;
                this.NavigationService.Navigate(new CustomerPage(bl.GetCustomerById(c.Id)));
            }
        }

        private void DeleteCustomer(object sender, RoutedEventArgs e) {
            try {
                //MessageBox.Show(bl.DeleteCustomer((BO.CustomerList)CustomerListView.SelectedItem));
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
