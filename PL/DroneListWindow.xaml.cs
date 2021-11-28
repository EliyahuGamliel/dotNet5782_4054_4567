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
using System.Windows.Shapes;
using IBL.BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneListWindow.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
        static IBL.IBL bl_drones;
        public DroneListWindow(IBL.IBL bl)
        {
            InitializeComponent();
            bl_drones = bl;
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            DroneListView.ItemsSource = bl_drones.GetDroneByFilter(d => true);
                      
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DroneListView.ItemsSource = bl_drones.GetDroneByFilter(d => (int)d.Status == (int)StatusSelector.SelectedItem);
        }

        private void MaxWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DroneListView.ItemsSource = bl_drones.GetDroneByFilter(d => (int)d.MaxWeight == (int)MaxWeightSelector.SelectedItem);
        }

        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            new DroneWindow(bl_drones).Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void action_drone(object sender, MouseButtonEventArgs e)
        {

        }
    }
}