using System;
using System.ComponentModel;
using System.Windows;
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        private Drone dr;
        private Parcel pa;
        private IBL bl = BlFactory.GetBl();
        private BackgroundWorker worker;
        private bool closeWindow;
        
        /// <summary>
        /// The ctor
        /// </summary>
        /// <param name="drone">The drone to make on it actions</param>
        public DroneWindow(Drone drone = null) {
            InitializeComponent();

            worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.ProgressChanged += worker_PrograssChanged;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            if (drone == null) {
                dr = new Drone();
                dr.Status = DroneStatuses.Maintenance;
            }
            else
                dr = drone;

            idStationToChrging.ItemsSource = bl.GetStationCharge();
            maxWeightDrone.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            if (dr.Status == DroneStatuses.Delivery)
                pa = bl.GetParcelById(dr.PTransfer.Id);

            this.DataContext = dr;

            if (drone == null) {
                DroneAddGrid.Visibility = Visibility.Visible;

                action1.Content = "Add Drone";
                action1.Click += new RoutedEventHandler(Add_Click);
            }
            else {
                idDrone.IsEnabled = false;
                maxWeightDrone.IsEnabled = false;
                Initialize();
            }
        }

        /// <summary>
        /// Initialise all the buttons of actions
        /// </summary>
        private void Initialize(object sender = null, RoutedEventArgs e = null) {
            dr = bl.GetDroneById(dr.Id.Value);
            this.DataContext = dr;
            IniClick();
            //If the choosen drone is in delivery
            if (dr.Status == DroneStatuses.Delivery) {
                pa = bl.GetParcelById(dr.PTransfer.Id);
                if (pa.PickedUp == null)
                    ChangePickUP();
                else
                    ChangeDelivery();
            }
            else if (dr.Status == DroneStatuses.Maintenance)
                ChangeRelese();
            else
                ChangeAssignSend();
        }

        #region possible functions for buttons (Drone View)

        /// <summary>
        /// If the update button has been pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show(bl.UpdateDrone(dr.Id.Value, modelDrone.Text));
        }

        /// <summary>
        /// If the assign button had been pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Assign_Click(object sender, RoutedEventArgs e) {
            try {
                MessageBox.Show(bl.AssignDroneParcel(dr.Id.Value));
                Initialize();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //action1.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// If the pickup button has been pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PickUp_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show(bl.PickUpDroneParcel(dr.Id.Value));
            Initialize();
        }

        /// <summary>
        /// If the deliver button has been pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Deliver_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show(bl.DeliverParcelCustomer(dr.Id.Value));
            Initialize();
        }

        /// <summary>
        /// If the send button has been pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Send_Click(object sender, RoutedEventArgs e) {
            try {
                MessageBox.Show(bl.SendDrone(dr.Id.Value));
                Initialize();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        /// <summary>
        /// If the release button has been pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Release_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show(bl.ReleasDrone(dr.Id.Value));
            Initialize();
        }
        #endregion

        /// <summary>
        /// If the add button has been pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e) {
            try {
                StationList st = (StationList)idStationToChrging.SelectedItem;
                MessageBox.Show(bl.AddDrone(dr, st.Id));
                this.Close();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// If the users wants to go back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void IniClick() {
            action1.Click -= new RoutedEventHandler(Deliver_Click);
            action2.Click -= new RoutedEventHandler(Release_Click);
            action2.Click -= new RoutedEventHandler(Send_Click);
            action1.Click -= new RoutedEventHandler(Assign_Click);
            action1.Click -= new RoutedEventHandler(PickUp_Click);
            action1.Click -= new RoutedEventHandler(Deliver_Click);
        }

        #region change the button(s) according the drone status
        /// <summary>
        /// Changes action1 to be the clickup button
        /// </summary>
        private void ChangePickUP() {
            action1.Content = "PickUp the Parcel \n  from Customer";
            action1.Click += new RoutedEventHandler(PickUp_Click);
        }

        /// <summary>
        /// Changes action2 to be the changedelivery  button
        /// </summary>
        private void ChangeDelivery() {
            action1.Content = "Deliver Parcel \n by the Drone";
            action1.Click += new RoutedEventHandler(Deliver_Click);
        }

        /// <summary>
        /// Makes action1 and action2 to visible and clickable buttons and changes them to be drone to parcel assigning
        ///and sending drone for charging buttons
        /// </summary>
        private void ChangeAssignSend() {
            action1.Content = "Assign the Drone \n      to Parcel";
            action1.Click += new RoutedEventHandler(Assign_Click);
            action2.Content = "Send the Drone \n  to Charging";
            action2.Click += new RoutedEventHandler(Send_Click);
        }

        /// <summary>
        /// Hides action1 and changes action2 to drone releasing button
        /// </summary>
        private void ChangeRelese() {
            action2.Content = "Release the Drone \n   from Charging";
            action2.Click += new RoutedEventHandler(Release_Click);
        }
        #endregion

        private void ParcelInDrone(object sender, RoutedEventArgs e) {
            ParcelPage parcelPage = new ParcelPage(bl.GetParcelById(dr.PTransfer.Id));
            parcelPage.Unloaded += Initialize;
            this.HostPage.Content = parcelPage;
        }

        private void SimulatorChecked(object sender, RoutedEventArgs e) {
            if (!worker.IsBusy)
                worker.RunWorkerAsync();
        }

        private void SimulatorUnChecked(object sender, RoutedEventArgs e) {
            closeWindow = false;
            worker.CancelAsync();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (closeWindow)
                this.Close();
            else
                Initialize();
        }

        private void worker_PrograssChanged(object sender, ProgressChangedEventArgs e) {
            dr = bl.GetDroneById(dr.Id.Value);
            this.DataContext = dr;
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e) {
            bl.PlaySimulator(dr.Id.Value, () => worker.ReportProgress(1), () => worker.CancellationPending);
        }

        protected override void OnClosing(CancelEventArgs e) {
            if (worker != null && worker.IsBusy) {
                e.Cancel = true;
                closeWindow = true;
                worker.CancelAsync();
            }
        }
    }
}
