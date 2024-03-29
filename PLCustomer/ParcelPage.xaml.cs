﻿using System;
using System.Windows;
using System.Windows.Controls;
using BlApi;
using BO;

namespace PLCustomer
{
    /// <summary>
    /// Interaction logic for ParcelPage.xaml
    /// </summary>
    public partial class ParcelPage : Page
    {
        private Parcel pa;
        private Customer cu;
        private IBL bl = BlFactory.GetBl();

        public ParcelPage(Parcel parcel = null, Customer customer = null) {
            InitializeComponent();

            targetIdParcel.ItemsSource = bl.GetCustomers();
            weightParcel.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            priorityParcel.ItemsSource = Enum.GetValues(typeof(Priorities));

            if (parcel == null) {
                pa = new Parcel();
                cu = customer;
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
                MessageBox.Show(bl.AddParcel(pa, cu.Id.Value, targetC.Id));
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
    }
}
