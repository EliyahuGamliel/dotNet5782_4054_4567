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
using System.ComponentModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerListPage.xaml
    /// </summary>
    public partial class CustomerListPage : Page
    {
        private BlApi.IBL bl = BlApi.BlFactory.GetBl();
        private readonly BackgroundWorker worker = new BackgroundWorker();

        public CustomerListPage()
        {
            InitializeComponent();
            worker.DoWork += Worker_DoWork;
            CustomerListView.ItemsSource = bl.GetCustomers();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e) {
            CustomerListView.ItemsSource = null;
            CustomerListView.ItemsSource = bl.GetCustomers();
        }


        /// <summary>
        /// Navigates to the "DronePage" - drone add page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCustomer(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CustomerPage(this));
        }

        /// <summary>
        /// If the user wants to go back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        /// <summary>
        /// Navigates to the "DronaPage" - drone actions page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerActions(object sender, MouseButtonEventArgs e)
        {
            if (CustomerListView.SelectedItem != null)
            {
                BO.CustomerList c = (BO.CustomerList)CustomerListView.SelectedItem;
                this.NavigationService.Navigate(new CustomerPage(bl.GetCustomerById(c.Id), this));
            }
        }
    }
}
