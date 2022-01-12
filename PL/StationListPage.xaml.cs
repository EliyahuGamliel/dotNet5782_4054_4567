using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for StationListPage.xaml
    /// </summary>
    public partial class StationListPage : Page
    {
        private IBL bl = BlFactory.GetBl();
        private ObservableCollection<StationList> stationList;

        public StationListPage() {
            InitializeComponent();
            stationList = new ObservableCollection<StationList>(bl.GetStations());
            this.DataContext = stationList;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddStation(object sender, RoutedEventArgs e) {
            StationPage stationPage = new StationPage();
            stationPage.Unloaded += UpdateList;
            this.NavigationService.Navigate(stationPage);
        }

        private void UpdateList(object sender = null, EventArgs e = null) {
            while (stationList.Count != 0) {
                StationList st = stationList.First();
                stationList.Remove(st);
            }
            foreach (var item in bl.GetStations()) {
                stationList.Add(item);
            }
        }

        /// <summary>
        /// If the user wants to go back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e) {
            this.NavigationService.GoBack();
        }

        /// <summary>
        /// Navigates to the "DronaPage" - drone actions page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StationActions(object sender, MouseButtonEventArgs e) {
            if (StationListView.SelectedItem != null) {
                StationList s = (StationList)StationListView.SelectedItem;
                StationPage stationPage = new StationPage(bl.GetStationById(s.Id));
                stationPage.Unloaded += UpdateList;
                this.NavigationService.Navigate(stationPage);
            }
        }

        private void DeleteStation(object sender, RoutedEventArgs e) {
            try {
                StationList station = (StationList)StationListView.SelectedItem;
                MessageBox.Show(bl.DeleteStation(station.Id));
                stationList.Remove(station);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ChangeViewList(object sender = null, RoutedEventArgs e = null) {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(stationList);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("ChargeSlots");
            if (view.GroupDescriptions.Count != 0)
                view.GroupDescriptions.Clear();
            else
                view.GroupDescriptions.Add(groupDescription);
        }
    }
}
