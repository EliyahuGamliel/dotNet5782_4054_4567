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
using IBL.BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneListPage.xaml
    /// </summary>
    public partial class DroneListPage : Page
    {
        static IBL.IBL blDroneList;
        private MainPage mPage;
        public DroneListPage(IBL.IBL bl, MainPage mainPage)
        {
            InitializeComponent();
            blDroneList = bl;
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            DroneListView.ItemsSource = blDroneList.GetDroneByFilter(d => true);
            mPage = mainPage;
        }

        public void Selector_SelectionChanged(object sender = null, SelectionChangedEventArgs e = null)
        {
            if (StatusSelector.SelectedItem == null && MaxWeightSelector.SelectedItem == null)
                DroneListView.ItemsSource = blDroneList.GetDroneByFilter(d => true);
            else if (StatusSelector.SelectedItem == null)
                DroneListView.ItemsSource = blDroneList.GetDroneByFilter(d => (int)d.MaxWeight == (int)MaxWeightSelector.SelectedItem);
            else if (MaxWeightSelector.SelectedItem == null)
                DroneListView.ItemsSource = blDroneList.GetDroneByFilter(d => (int)d.Status == (int)StatusSelector.SelectedItem);
            else
                DroneListView.ItemsSource = blDroneList.GetDroneByFilter(d => (int)d.Status == (int)StatusSelector.SelectedItem && (int)d.MaxWeight == (int)MaxWeightSelector.SelectedItem);   
        }

        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DroneAddPage(blDroneList, this));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(mPage);
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            DroneListView.ItemsSource = blDroneList.GetDroneByFilter(d => true);
            StatusSelector.SelectedItem = null;
            MaxWeightSelector.SelectedItem = null;
        }

        private void DroneActions(object sender, MouseButtonEventArgs e)
        {
            DroneList d = (DroneList)DroneListView.SelectedItem;
            this.NavigationService.Navigate(new DroneActionsPage(blDroneList, blDroneList.GetDroneById(d.Id), this));
        }
    }
}
