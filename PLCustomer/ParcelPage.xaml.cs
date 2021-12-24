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

namespace PLCustomer
{
    /// <summary>
    /// Interaction logic for ParcelPage.xaml
    /// </summary>
    public partial class ParcelPage : Page
    {
        private BlApi.IBL bl = BlApi.BlFactory.GetBl();
        private BO.Customer cu;
        private MainPage clPage;

        private int numInt;
        private double numDouble;

        /// <summary>
        /// The second constructor (Drone Add)
        /// </summary>
        /// <param name="bl">Data Base</param>
        /// <param name="droneListPage">Pointer to the Drone List Page</param>
        public ParcelPage(MainPage customerListPage) {
            InitializeComponent();
            clPage = customerListPage;


            
        }
    }
}
