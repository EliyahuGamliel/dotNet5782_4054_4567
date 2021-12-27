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
using BO;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private IBL bl = BlApi.BlFactory.GetBl();

        /// <summary>
        /// The ctor of the main page 
        /// </summary>
        /// <param name="mainWindow">Pointer to the Main Window</param>
        public MainPage() {
            InitializeComponent();
        }

        /// <summary>
        /// When the show drone button has been pressed it navigates to the "DroneListPage"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DroneList(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new DroneListPage());
        }

        private void CustomerList(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new CustomerListPage());
        }

        private void StationList(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new StationListPage());
        }

        private void ParcelList(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new ParcelListPage());
        }
    }
}