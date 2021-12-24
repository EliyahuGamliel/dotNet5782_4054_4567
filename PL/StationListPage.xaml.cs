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
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for StationListPage.xaml
    /// </summary>
    public partial class StationListPage : Page
    {
        private BlApi.IBL bl = BlFactory.GetBl();
        private bool isGroup;
        public StationListPage()
        {
            InitializeComponent();
            StationListView.ItemsSource = bl.GetStations();
        }

        /// <summary>
        /// Navigates to the "DronePage" - drone add page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddStation(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new StationPage());
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
                BO.StationList s = (BO.StationList)StationListView.SelectedItem;
                this.NavigationService.Navigate(new StationPage(bl.GetStationById(s.Id)));
            }
        }

        private void ChangeViewList(object sender = null, RoutedEventArgs e = null) {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(StationListView.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("ChargeSlots");
            if (view.GroupDescriptions.Count != 0) {
                isGroup = false;
                view.GroupDescriptions.Clear();
            }
            else {
                isGroup = true;
                view.GroupDescriptions.Add(groupDescription);
            }
        }

        private void SaveDisplay() {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(StationListView.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("ChargeSlots");
            if (isGroup) {
                if (view.GroupDescriptions.Count != 0) {
                    view.GroupDescriptions.Clear();
                    view.GroupDescriptions.Add(groupDescription);
                }
                else
                    view.GroupDescriptions.Add(groupDescription);
            }
        }
    }
}
