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
using BO;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        static IBL bl;
        internal MainWindow mWindow;

        /// <summary>
        /// The ctor of the main page 
        /// </summary>
        /// <param name="mainWindow">Pointer to the Main Window</param>
        public MainPage(MainWindow mainWindow)
        {
            //Set the value of "CurrentPageBonus" to be "MainPage" to allow the window to close - Bonus
            mainWindow.CurrentPageBonus = typeof(MainPage);
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
            mWindow = mainWindow;
        }


        private void SignUp(object sender, RoutedEventArgs e)
        {

        }

        private void SignIn(object sender, RoutedEventArgs e)
        {

        }
    }
}