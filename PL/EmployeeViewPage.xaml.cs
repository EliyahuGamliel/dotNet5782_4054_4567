﻿using System;
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
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for EmployeeViewPage.xaml
    /// </summary>
    public partial class EmployeeViewPage : Page
    {
        static IBL bl;
        internal MainWindow mWindow;
        public EmployeeViewPage()
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
        }
      
        /// <summary>
        /// When the show drone button has been pressed it navigates to the "DroneListPage"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowDronesButton_Click(object sender, RoutedEventArgs e)
        {
            //Set the value of "CurrentPageBonus" to be "DroneListPage" to don't allow the window to close - Bonus
            mWindow.CurrentPageBonus = typeof(DroneListPage);
            this.NavigationService.Navigate(new DroneListPage(bl, this));
        }

        /// <summary>
        /// Makes sure that the gif keep running over and over again
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
