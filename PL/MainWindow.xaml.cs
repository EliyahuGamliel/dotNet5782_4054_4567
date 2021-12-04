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
        //Keeps track of the page we are at so we will know if we can close the window - Bonus
        internal Type CurrentPageBonus { set; get; }

        /// <summary>
        /// The ctor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            //Open in the window new Page
            MainPage.Content = new MainPage(this);
        }

        /// <summary>
        /// checks if we can close the windows using CurrentPageBonus- bonus
        /// </summary>
        /// <param name="e">The closing event</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            //Checks of we are at the mainpage - Bonus
            if (CurrentPageBonus != typeof(MainPage))
            {
                base.OnClosing(e);
                e.Cancel = true;
                MessageBox.Show("Cannot Close! Back to Main Page.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
