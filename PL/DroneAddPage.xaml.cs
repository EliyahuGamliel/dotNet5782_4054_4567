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
    /// Interaction logic for DroneAddPage.xaml
    /// </summary>
    public partial class DroneAddPage : Page
    {
        static IBL.IBL blDroneAdd;
        private DroneListPage dlPage;
        private DroneList droneList = new DroneList();
        private int idStation;
        public DroneAddPage(IBL.IBL bl, DroneListPage droneListPage)
        {
            InitializeComponent();
            blDroneAdd = bl;
            dlPage = droneListPage;
            maxWeight.ItemsSource = Enum.GetValues(typeof(WeightCategories));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(dlPage);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show(blDroneAdd.AddDrone(droneList, idStation));
                dlPage.Selector_SelectionChanged();
                this.NavigationService.Navigate(dlPage);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void GetIdStationToChrging(object sender, RoutedEventArgs e)
        {
            GetInt(idStationToChrging);
            if (idStationToChrging.Background == Brushes.White)
                Int32.TryParse(idStationToChrging.Text, out idStation);
        }

        private void GetMaxWeight(object sender, RoutedEventArgs e)
        {
            droneList.MaxWeight = (WeightCategories)(int)maxWeight.SelectedItem;
        }

        private void GetModel(object sender, RoutedEventArgs e)
        {
            droneList.Model = modelDrone.Text;
        }

        private void GetId(object sender, RoutedEventArgs e)
        {
            GetInt(idDrone);
            if (idDrone.Background == Brushes.White)
            {
                int id;
                Int32.TryParse(idDrone.Text, out id);
                droneList.Id = id;
            }
        }

        private void GetInt(TextBox tBox)
        {
            int num;
            bool error = Int32.TryParse(tBox.Text, out num);
            if (!error)
                tBox.Background = Brushes.Red;
            else
                tBox.Background = Brushes.White;
        }
    }
}
