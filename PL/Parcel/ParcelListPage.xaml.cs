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
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelListPage.xaml
    /// </summary>
    public partial class ParcelListPage : Page
    {
        private IBL bl = BlFactory.GetBl();
        private ObservableCollection<ParcelList> parcelList;
        private string Group = "";

        public ParcelListPage() {
            InitializeComponent();
            foreach (var item in Enum.GetValues(typeof(Statuses)))
                StatusSelector.Items.Add(item);
            StatusSelector.Items.Add("All");
            foreach (var item in Enum.GetValues(typeof(Priorities)))
                PriortySelector.Items.Add(item);
            PriortySelector.Items.Add("All");
            foreach (var item in Enum.GetValues(typeof(WeightCategories)))
                WeightSelector.Items.Add(item);
            WeightSelector.Items.Add("All");

            parcelList = new ObservableCollection<BO.ParcelList>(bl.GetParcels());
            this.DataContext = parcelList;
        }

        private void UpdateList(object sender = null, EventArgs e = null) {
            while (parcelList.Count != 0) {
                ParcelList pa = parcelList.First();
                parcelList.Remove(pa);
            }
            foreach (var item in bl.GetParcelByFilter(WeightSelector.SelectedItem, StatusSelector.SelectedItem,
                                            PriortySelector.SelectedItem, fromDate.SelectedDate, toDate.SelectedDate)) {
                parcelList.Add(item);
            }
        }

        /// <summary>
        /// If the selected choice in the combo box changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Selector_SelectionChanged(object sender = null, SelectionChangedEventArgs e = null) {
            UpdateList();
            SaveDisplay();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddParcel_Click(object sender, RoutedEventArgs e) {
            ParcelPage parcelPage = new ParcelPage();
            parcelPage.Unloaded += UpdateList;
            this.NavigationService.Navigate(parcelPage);
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
            StatusSelector.SelectedItem = null;
            WeightSelector.SelectedItem = null;
            PriortySelector.SelectedItem = null;
            toDate.SelectedDate = null;
            fromDate.SelectedDate = null;
        }

        /// <summary>
        /// Navigates to the "DronaPage" - drone actions page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelActions(object sender, MouseButtonEventArgs e) {
            if (ParcelListView.SelectedItem != null) {
                ParcelList p = (ParcelList)ParcelListView.SelectedItem;
                ParcelPage parcelPage = new ParcelPage(bl.GetParcelById(p.Id));
                parcelPage.Unloaded += UpdateList;
                this.NavigationService.Navigate(parcelPage);
            }
        }

        private void DeleteParcel(object sender, RoutedEventArgs e) {
            try {
                ParcelList parcel = (ParcelList)ParcelListView.SelectedItem;
                MessageBox.Show(bl.DeleteParcel(parcel.Id));
                parcelList.Remove(parcel);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ChangeViewList(object sender = null, RoutedEventArgs e = null) {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
            view.GroupDescriptions.Clear();
            if (Group == "TargetId") {
                Group = "";
                textGroup.Content = "";
            }
            else {
                if (Group == "SenderId") {
                    Group = "TargetId";
                    textGroup.Content = "Grouping By TargetID:";
                }
                else {
                    Group = "SenderId";
                    textGroup.Content = "Grouping By SenderID:";
                }
                PropertyGroupDescription groupDescription = new PropertyGroupDescription(Group);
                view.GroupDescriptions.Add(groupDescription);
            }
        }


        private void SaveDisplay() {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription(Group);
            if (Group != "") {
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
