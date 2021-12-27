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
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelListPage.xaml
    /// </summary>
    public partial class ParcelListPage : Page
    {
        private BlApi.IBL bl = BlApi.BlFactory.GetBl();
        private ObservableCollection<BO.ParcelList> parcelList;
        private string Group = "";

        public ParcelListPage()
        {
            InitializeComponent();
            foreach (var item in Enum.GetValues(typeof(BO.Statuses)))
                StatusSelector.Items.Add(item);
            StatusSelector.Items.Add("All");
            foreach (var item in Enum.GetValues(typeof(BO.WeightCategories)))
                MaxWeightSelector.Items.Add(item);
            MaxWeightSelector.Items.Add("All");

            parcelList = new ObservableCollection<BO.ParcelList>(bl.GetParcels());
            this.DataContext = parcelList;
        }
        
        /// <summary>
        /// If the selected choice in the combo box changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Selector_SelectionChanged(object sender = null, SelectionChangedEventArgs e = null) {
            parcelList.Clear();
            //foreach (var item in bl.GetParcelByFilter(MaxWeightSelector.SelectedItem, StatusSelector.SelectedItem)) {
              //  parcelList.Add(item);
            //}
            SaveDisplay();
        }

        /// <summary>
        /// Navigates to the "DronePage" - drone add page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDrone_Click(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new ParcelPage());
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
        /// If the user clicks the reset button the it resets the filters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reset_Click(object sender, RoutedEventArgs e) {
            DroneListView.ItemsSource = null;
            DroneListView.ItemsSource = bl.GetDrones();
            StatusSelector.SelectedItem = null;
            MaxWeightSelector.SelectedItem = null;
        }

        /// <summary>
        /// Navigates to the "DronaPage" - drone actions page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DroneActions(object sender, MouseButtonEventArgs e) {
            if (DroneListView.SelectedItem != null) {
                BO.ParcelList p = (BO.ParcelList)DroneListView.SelectedItem;
                this.NavigationService.Navigate(new ParcelPage(bl.GetParcelById(p.Id)));
            }
        }

        private void ChangeViewList(object sender = null, RoutedEventArgs e = null) {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(DroneListView.ItemsSource);
            view.GroupDescriptions.Clear();
            if (Group == "TargetId")
                Group = "";
            else {
                if (Group == "SenderId")
                    Group = "TargetId";
                else
                    Group = "SenderId";
                PropertyGroupDescription groupDescription = new PropertyGroupDescription(Group);
                view.GroupDescriptions.Add(groupDescription);
            }
        }


        private void SaveDisplay() {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(DroneListView.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Status");
            if (Group == "") {
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
