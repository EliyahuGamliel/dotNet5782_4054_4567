using System.ComponentModel;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
