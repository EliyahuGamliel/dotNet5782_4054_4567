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
using BO;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneListPage.xaml
    /// </summary>
    public partial class DroneListPage : Page
    {
        private IBL bl = BlFactory.GetBl();
        private ObservableCollection<DroneList> droneList;
        private bool isGroup;

        /// <summary>
        /// The ctor
        /// </summary>
        public DroneListPage() {
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
        private void Selector_SelectionChanged(object sender = null, SelectionChangedEventArgs e = null) {
            UpdateList();
            SaveDisplay();
        }

        /// <summary>
        /// Navigates to the "DronePage" - drone add page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDrone(object sender, RoutedEventArgs e) {
            DroneWindow dronePage = new DroneWindow();
            dronePage.Unloaded += UpdateList;
            dronePage.ShowDialog();
        }

        private void UpdateList(object sender = null, EventArgs e = null) {
            while (droneList.Count != 0) {
                DroneList dr = droneList.First();
                droneList.Remove(dr);
            }
            foreach (var item in bl.GetDroneByFilter(MaxWeightSelector.SelectedItem, StatusSelector.SelectedItem)) {
                droneList.Add(item);
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
        /// If the user clicks the reset button the it resets the filters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reset(object sender, RoutedEventArgs e) {
            StatusSelector.SelectedItem = null;
            MaxWeightSelector.SelectedItem = null;
        }

        private void DeleteDrone(object sender, RoutedEventArgs e) {
            try {
                MessageBox.Show(bl.DeleteDrone((DroneList)DroneListView.SelectedItem));
                UpdateList();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Navigates to the "DronaPage" - drone actions page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DroneActions(object sender, MouseButtonEventArgs e) {
            if (DroneListView.SelectedItem != null) {
                DroneList d = (DroneList)DroneListView.SelectedItem;
                DroneWindow dronePage = new DroneWindow(bl.GetDroneById(d.Id));
                dronePage.Closed += UpdateList;
                dronePage.Show();
            }
        }

        private void ChangeViewList(object sender = null, RoutedEventArgs e = null) {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(droneList);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Status");
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
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Status");
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