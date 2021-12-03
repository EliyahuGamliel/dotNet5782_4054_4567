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
        internal MainWindow mWindow;

        /// <summary>
        /// the constructor of the main page 
        /// </summary>
        /// <param name="mainWindow">the main window</param>
        public MainPage(MainWindow mainWindow)
        {
            mainWindow.CurrentPageBonus = typeof(MainPage);
            InitializeComponent();
            bl = new IBL.BL();
            mWindow = mainWindow;
        }

        /// <summary>
        /// when the show drone button has been pressed it navigates to the wanted page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowDronesButton_Click(object sender, RoutedEventArgs e)
        {
            mWindow.CurrentPageBonus = typeof(DroneListPage);// keeps track of the current page  - bonus
            this.NavigationService.Navigate(new DroneListPage(bl, this));
        }

        /// <summary>
        /// makes sure that the gif keep running over and over again
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