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
        /// The ctor
        /// </summary>
        /// <param name="bl">Data Base</param>
        /// <param name="mainPage">Pointer to the Main Page</param>
        public DroneListPage(IBL.IBL bl, MainPage mainPage)
        {
            InitializeComponent();
            blDroneList = bl;
            foreach (var item in Enum.GetValues(typeof(DroneStatuses)))
                StatusSelector.Items.Add(item);
            StatusSelector.Items.Add("All");
            foreach (var item in Enum.GetValues(typeof(WeightCategories)))
                MaxWeightSelector.Items.Add(item);
            MaxWeightSelector.Items.Add("All");
            DroneListView.ItemsSource = blDroneList.GetDrones();
            mPage = mainPage;
        }

        /// <summary>
        /// If the selected choice in the combo box changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Selector_SelectionChanged(object sender = null, SelectionChangedEventArgs e = null)
        {
            DroneListView.ItemsSource = null;
            DroneListView.ItemsSource = blDroneList.GetDroneByFilter(MaxWeightSelector.SelectedItem, StatusSelector.SelectedItem);
        }

        /// <summary>
        /// Navigates to the "DronePage" - drone add page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DronePage(blDroneList, this));
        }

        /// <summary>
        /// If the user wants to go back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            //Set the value of "CurrentPageBonus" to be "MainPage" to allow the window to close - Bonus
            mPage.mWindow.CurrentPageBonus = typeof(MainPage);
            this.NavigationService.Navigate(mPage);
        }

        /// <summary>
        /// If the user clicks the reset button the it resets the filters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            DroneListView.ItemsSource = null;
            DroneListView.ItemsSource = blDroneList.GetDrones();
            StatusSelector.SelectedItem = null;
            MaxWeightSelector.SelectedItem = null;
        }

        /// <summary>
        /// Navigates to the "DronaPage" - drone actions page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DroneActions(object sender, MouseButtonEventArgs e)
        {
            DroneList d = (DroneList)DroneListView.SelectedItem;
            this.NavigationService.Navigate(new DronePage(blDroneList, blDroneList.GetDroneById(d.Id), this));
        }

        /// <summary>
        /// Makes sure the gif keeps running over and over
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Again_MediaEnded(object sender, RoutedEventArgs e)
        {
            Gif.Position = new TimeSpan(0, 0, 1);
            Gif.Play();
        }
    }
}
