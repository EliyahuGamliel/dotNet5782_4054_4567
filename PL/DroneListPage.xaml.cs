using System;
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
using System.Collections.Specialized;
using System.Collections.Generic;
using BO;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneListPage.xaml
    /// </summary>
    public partial class DroneListPage : Page
    {
        private BlApi.IBL bl = BlFactory.GetBl();
        private ObservableCollection<BO.DroneList> droneList;
        private bool isGroup;

        /// <summary>
        /// The ctor
        /// </summary>
        /// <param name="bl">Data Base</param>
        /// <param name="mainPage">Pointer to the Main Page</param>
        public DroneListPage()
        {
            InitializeComponent();
            foreach (var item in Enum.GetValues(typeof(DroneStatuses)))
                StatusSelector.Items.Add(item);
            StatusSelector.Items.Add("All");
            foreach (var item in Enum.GetValues(typeof(WeightCategories)))
                MaxWeightSelector.Items.Add(item);
            MaxWeightSelector.Items.Add("All");

            droneList = new ObservableCollection<DroneList>(bl.GetDrones());
            this.DataContext = droneList;
        }

        /// <summary>
        /// If the selected choice in the combo box changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Selector_SelectionChanged(object sender = null, SelectionChangedEventArgs e = null)
        {
            droneList.Clear();
            foreach (var item in bl.GetDroneByFilter(MaxWeightSelector.SelectedItem, StatusSelector.SelectedItem)) {
                droneList.Add(item);
            }
            SaveDisplay();
        }

        /// <summary>
        /// Navigates to the "DronePage" - drone add page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            DronePage dronePage = new DronePage();
            dronePage.Unloaded += UpdateList;
            this.NavigationService.Navigate(dronePage);
        }

         private void UpdateList(object sender = null, EventArgs e = null) {
            droneList.Clear();
            foreach (var item in bl.GetDroneByFilter(MaxWeightSelector.SelectedItem, StatusSelector.SelectedItem)) {
                droneList.Add(item);
            }
        }

        /// <summary>
        /// If the user wants to go back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        /// <summary>
        /// If the user clicks the reset button the it resets the filters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            StatusSelector.SelectedItem = null;
            MaxWeightSelector.SelectedItem = null;
            UpdateList();
        }

        /// <summary>
        /// Navigates to the "DronaPage" - drone actions page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DroneActions(object sender, MouseButtonEventArgs e)
        {
            if (DroneListView.SelectedItem != null)
            {
                DroneList d = (DroneList)DroneListView.SelectedItem;
                this.NavigationService.Navigate(new DronePage(bl.GetDroneById(d.Id)));
            }
        }

        private void ChangeViewList(object sender = null, RoutedEventArgs e = null) {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(DroneListView.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("SenderId");
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
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(DroneListView.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("SenderId");
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