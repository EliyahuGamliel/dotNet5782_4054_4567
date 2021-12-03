using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal Type CurrentPageBonus { set; get; }//keeps track of the page we are at so we will know if we can close the window - bonus

        /// <summary>
        ///the constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            MainPage.Content = new MainPage(this);//updates the CurrentPageBonus
        }

        /// <summary>
        /// checks if we can close the windows using CurrentPageBonus- bonus
        /// </summary>
        /// <param name="e">the closing event</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (CurrentPageBonus != typeof(MainPage))//checks of we are at the mainpage - bonus
            {
                base.OnClosing(e);
                e.Cancel = true;
                MessageBox.Show("Cannot Close! Back to Main Page.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
