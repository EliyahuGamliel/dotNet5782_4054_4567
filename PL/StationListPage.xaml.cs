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
    /// Interaction logic for StationListPage.xaml
    /// </summary>
    public partial class StationListPage : Page
    {
        static IBL.IBL blStationList;
        private MainPage mPage;
        public StationListPage(IBL.IBL bl, MainPage mainPage)
        {
            InitializeComponent();
            blStationList = bl;
            StationListView.ItemsSource = blStationList.GetStationByFilter(s => true);
            mPage = mainPage;
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(mPage);
        }
    }
}
