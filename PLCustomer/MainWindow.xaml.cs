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

namespace PLCustomer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Keeps track of the page we are at so we will know if we can close the window - Bonus
        internal bool ExitBonus { set; get; }

        /// <summary>
        /// The ctor
        /// </summary>
        public MainWindow() {
            InitializeComponent();
            //Open in the window new Page
            MainPage.Content = new MainPage();
        }
    }
}
