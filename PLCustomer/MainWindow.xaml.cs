using System.Windows;
using System.Windows.Navigation;

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

        private void MainPage_Navigated(object sender, NavigationEventArgs e) {

        }
    }
}
