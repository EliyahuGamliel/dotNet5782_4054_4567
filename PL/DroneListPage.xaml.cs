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

        /// <summary>
        /// the constructor
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="mainPage">the main page</param>
        public DroneListPage(IBL.IBL bl, MainPage mainPage)
        {
            InitializeComponent();
            blDroneList = bl;
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            DroneListView.ItemsSource = blDroneList.GetDrones();
            mPage = mainPage;
        }

        /// <summary>
        /// if the selected choice in the combo box changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Selector_SelectionChanged(object sender = null, SelectionChangedEventArgs e = null)
        {
            DroneListView.ItemsSource = null;
            if (StatusSelector.SelectedItem == null && MaxWeightSelector.SelectedItem == null)
                DroneListView.ItemsSource = blDroneList.GetDroneByFilter(null, null);
            else if (StatusSelector.SelectedItem == null)
                DroneListView.ItemsSource = blDroneList.GetDroneByFilter((int)MaxWeightSelector.SelectedItem, null);
            else if (MaxWeightSelector.SelectedItem == null)
                DroneListView.ItemsSource = blDroneList.GetDroneByFilter(null, (int)StatusSelector.SelectedItem);
            else
                DroneListView.ItemsSource = blDroneList.GetDroneByFilter((int)MaxWeightSelector.SelectedItem, (int)StatusSelector.SelectedItem);
        }

        /// <summary>
        /// navigates to the drone adding page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DronePage(blDroneList, this));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            mPage.mWindow.CurrentPageBonus = typeof(MainPage);
            this.NavigationService.Navigate(mPage);
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            DroneListView.ItemsSource = null;
            DroneListView.ItemsSource = blDroneList.GetDrones();
            StatusSelector.SelectedItem = null;
            MaxWeightSelector.SelectedItem = null;
        }

        private void DroneActions(object sender, MouseButtonEventArgs e)
        {
            DroneList d = (DroneList)DroneListView.SelectedItem;
            this.NavigationService.Navigate(new DronePage(blDroneList, blDroneList.GetDroneById(d.Id), this));
        }

        private void Again_MediaEnded(object sender, RoutedEventArgs e)
        {
            Gif.Position = new TimeSpan(0, 0, 1);
            Gif.Play();
        }
    }
}
