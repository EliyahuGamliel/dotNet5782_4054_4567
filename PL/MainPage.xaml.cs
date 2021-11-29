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

namespace PL
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        static IBL.IBL bl;
        private MainWindow mWindow;
        public MainPage(MainWindow mainWindow)
        {
            InitializeComponent();
            bl = new IBL.BL();
            mWindow = mainWindow;
        }

        private void ShowDronesButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DroneListPage(bl, this));
        }

        private void ShowStationsButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new StationListPage(bl, this));
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            mWindow.Close();
        }
    }
}