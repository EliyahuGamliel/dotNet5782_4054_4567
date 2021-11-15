using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL : IBL
    {
        Random rand = new Random();
        IDAL.IDal data;
        List<DroneList> dronesList = new List<DroneList>();
        double Avaliable;
        double WeightLight;
        double WeightMedium;
        double WeightHeavy;
        double ChargingRate;

        //Ctor of BL
        public BL() {
            data = new DalObject.DalObject();

            double[] arr = new double[5]; 
            arr = data.DroneElectricityUse();
            Avaliable = arr[0];
            WeightLight = arr[1];
            WeightMedium = arr[2];
            WeightHeavy = arr[3];
            ChargingRate = arr[4];
            
            IEnumerable<IDAL.DO.Drone> list_d = data.GetDrones();
            //
            foreach (var item in list_d) {
                bool help = true;
                double min_battery = 0;

                DroneList dl = new DroneList();
                dl.Id = item.Id;
                dl.MaxWeight = (WeightCategories)(int)item.MaxWeight;
                dl.Model = item.Model;

                IEnumerable<IDAL.DO.Parcel> list_p = data.GetParcels();
                foreach (var itemParcel in list_p) {
                    //If the parcel is associated with the drone and also the parcrel in the middle of the shipment
                    if (itemParcel.DroneId == dl.Id && (ReturnStatus(itemParcel) == 1 || ReturnStatus(itemParcel) == 2)) {
                        dl.Status = DroneStatuses.Delivery;
                        dl.ParcelId = itemParcel.Id;
                        //If the parcel was only associated
                        if (ReturnStatus(itemParcel) == 1) {
                            Customer c = GetCustomerById(itemParcel.SenderId);
                            dl.CLocation.Longitude = ReturnCloseStation(data.GetStations(), c.Location).Longitude;
                            dl.CLocation.Lattitude = ReturnCloseStation(data.GetStations(), c.Location).Lattitude;
                        }
                        //If the parcel was also collected
                        else {
                            Customer c = GetCustomerById(itemParcel.SenderId);
                            dl.CLocation = c.Location;
                        }
                        
                        //The customer target of the parcel
                        Customer cu = GetCustomerById(itemParcel.TargetId);
                        Location l1 = new Location();
                        l1 = cu.Location;

                        Location l2 = new Location();
                        l2.Lattitude = ReturnCloseStation(data.GetStations(), l1).Lattitude;
                        l2.Longitude = ReturnCloseStation(data.GetStations(), l1).Longitude;

                        //Minimum battery to finish the shipment
                        min_battery = ReturnBattery((int)itemParcel.Weight, dl.CLocation, l1) + ReturnBattery(3, dl.CLocation, l2);
                        dl.Battery = rand.NextDouble() + rand.Next((int)min_battery + 1, 100);
                        if (dl.Battery > 100)
                            dl.Battery = 100;
                        //The drone is in delivery mode
                        help = false;
                        break;
                    }
                }
                //If the drone is not in delivery mode (no parcel associated with it)
                if (help) {
                    //The drone mode is randomized
                    dl.Status = (DroneStatuses)rand.Next(0,2);
                    //If the situation that came out is: maintenance
                    if (dl.Status == DroneStatuses.Maintenance) {
                        int counter = 0;
                        IEnumerable<IDAL.DO.Station> list_s = data.GetStations();
                        foreach (var itemStation in list_s)
                            counter += 1;
                        //The drone is at a random station
                        int s = rand.Next(0, counter);
                        foreach (var itemStation in list_s) {
                            if (s == 0) {
                                dl.CLocation.Longitude = itemStation.Longitude;
                                dl.CLocation.Lattitude = itemStation.Lattitude;
                                UpdateStation(itemStation.Id, itemStation.Name, itemStation.ChargeSlots - 1);
                                break;
                            }
                            s -= 1;
                        }
                        dl.Battery = rand.NextDouble() + rand.Next(0,20);
                    }
                    //If the situation that came out is: available
                    else {
                        int counter = 0;
                        IEnumerable<CustomerList> list_c = GetCustomers();
                        foreach (var itemCustomer in list_c)
                            if (itemCustomer.ParcelsGet > 0)
                                counter += 1;
                        //The drone is at a random customer Location
                        int s = rand.Next(0, counter);
                        foreach (var itemCustomer in list_c) {
                            if (s == 0) {
                                Customer c = GetCustomerById(itemCustomer.Id);
                                dl.CLocation = c.Location;
                                break;
                            }
                            //If the customer get least one parcel
                            if (itemCustomer.ParcelsGet > 0)
                                s -= 1;
                        }

                        Location l = new Location();
                        l.Lattitude = ReturnCloseStation(data.GetStations(), dl.CLocation).Lattitude;
                        l.Longitude = ReturnCloseStation(data.GetStations(), dl.CLocation).Longitude;
                        min_battery = ReturnBattery(3, dl.CLocation, l);
                        dl.Battery = rand.NextDouble() + rand.Next((int)min_battery + 1, 100);
                        if (dl.Battery > 100)
                            dl.Battery = 100;
                    }
                }
                dronesList.Add(dl);
            }
        }
        
        /// <summary>
        /// /// If all is fine, the drone assign to a parcel, else throw exception
        /// </summary>
        /// <param name="DroneId">ID of the drone to assign a parcel</param>
        /// <returns>Notice if the addition was successful</returns>
        public string AssignDroneParcel(int DroneId){
            CheckNotExistId(dronesList, DroneId);
            DroneList d = dronesList.Find(dr => dr.Id == DroneId);
            int index = dronesList.IndexOf(d);

            IDAL.DO.Parcel p_choose = new IDAL.DO.Parcel();
            IEnumerable<IDAL.DO.Parcel> list_p = data.GetParcels();
            p_choose.Id = -1;
            bool first = true;
            foreach (var item in list_p) {
                Customer c_sender = GetCustomerById(item.SenderId);
                Customer c_target = GetCustomerById(item.TargetId);
                IDAL.DO.Station s_close = ReturnCloseStation(data.GetStations(), c_target.Location);
                Location l_s = new Location();
                l_s.Lattitude = s_close.Lattitude;
                l_s.Longitude = s_close.Longitude;
                //drone go to the coustomer sender location
                double min_battery = ReturnBattery(3, d.CLocation, c_sender.Location);
                //and fron there drone go to the coustomer target location
                min_battery += ReturnBattery((int)item.Weight, c_sender.Location, c_target.Location);
                //and fron there drone go to the close station
                min_battery += ReturnBattery(3, c_target.Location, l_s);
                if (d.Battery >= min_battery)
                    //First parcel that not associated
                    if (first && ReturnStatus(item) == 0)
                        p_choose = item;
                    else if (ReturnStatus(item) == 0)
                        //The most preferred parcel for associated
                        p_choose = CompressParcels(p_choose, item, d);
            }

            //There are no matching parcels
            if (p_choose.Id == -1)
                throw new DroneCannotAssigan();

            d.Status = DroneStatuses.Delivery;
            d.ParcelId = p_choose.Id;
            dronesList[index] = d;
            p_choose.Scheduled = DateTime.Now;
            p_choose.DroneId = d.Id;

            data.UpdateParcel(p_choose);
            return "The update was successful\n";
        }

        /// <summary>
        /// Returns the most preferred parcel for pairing
        /// </summary>
        /// <param name="p1">Object of parcel 1 for comparison</param>
        /// <param name="p2">Object of parcel 2 for comparison</param>
        /// <param name="d">Object of drone</param>
        /// <returns>Returns the most preferred parcel</returns>
        public IDAL.DO.Parcel CompressParcels(IDAL.DO.Parcel p1, IDAL.DO.Parcel p2, DroneList d) {
            Customer c1 = GetCustomerById(p1.SenderId);
            Customer c2 = GetCustomerById(p2.SenderId);
            if (p1.Priority > p2.Priority)
                return p1;
            if (p2.Priority > p1.Priority)
                return p2;
            if (p1.Weight > p2.Weight && (int)p1.Weight <= (int)d.MaxWeight)
                return p1;
            if (p2.Weight > p1.Weight && (int)p2.Weight <= (int)d.MaxWeight)
                return p2;
            if (DistanceTo(d.CLocation, c1.Location) > DistanceTo(d.CLocation, c2.Location))
                return p1;
            if (DistanceTo(d.CLocation, c2.Location) > DistanceTo(d.CLocation, c1.Location))
                return p2;
            return p1;
        }
        
        /// <summary>
        /// If all is fine, the drone pick up the parcel, else throw exception
        /// </summary>
        /// <param name="id">ID of the drone to pickup a parcel</param>
        /// <returns>Notice if the addition was successful</returns>        
        public string PickUpDroneParcel(int id){
            CheckNotExistId(dronesList, id);
            DroneList d = dronesList.Find(dr => dr.Id == id);
            Drone d_help = GetDroneById(id);
            int index = dronesList.IndexOf(d);

            IDAL.DO.Parcel p = data.GetParcelById(d.ParcelId);

            CheckDroneCannotPickUp(p);
            p.PickedUp = DateTime.Now;
            double battery = ReturnBattery(3, d.CLocation, d_help.PTransfer.Collection_Location);
            d.Battery -= battery;
            d.CLocation = d_help.PTransfer.Collection_Location;
            dronesList[index] = d;
            data.UpdateParcel(p);
            return "The update was successful\n";
        }

        /// <summary>
        /// If all is fine, the drone deliver the parcel, else throw exception
        /// </summary>
        /// <param name="id">ID of the drone to deliver a parcel</param>
        /// <returns>Notice if the addition was successful</returns>   
        public string DeliverParcelCustomer(int id){
            CheckNotExistId(dronesList, id);
            DroneList d = dronesList.Find(dr => dr.Id == id);
            Drone d_help = GetDroneById(id);
            int index = dronesList.IndexOf(d);

            IDAL.DO.Parcel p = data.GetParcelById(d.ParcelId);
            CheckDroneCannotDeliver(p);
            p.Delivered = DateTime.Now;
            double battery = ReturnBattery(3, d.CLocation, d_help.PTransfer.Destination_Location);
            d.Status = DroneStatuses.Available;
            d.Battery -= battery;
            d.CLocation = d_help.PTransfer.Destination_Location;
            d.ParcelId = -1;
            dronesList[index] = d;
            data.UpdateParcel(p);
            return "The update was successful\n";
        }

        /// <summary>
        /// According to calculations, returns the status of the parcel
        /// </summary>
        /// <param name="p">Object of parcel</param>
        /// <returns>int of Status of parcel</returns>
        public int ReturnStatus(IDAL.DO.Parcel p)  {
            if (DateTime.Compare(p.Requested, p.Scheduled) > 0)
                return (int)Statuses.Created;
            else if (DateTime.Compare(p.Scheduled, p.PickedUp) > 0)
                return (int)Statuses.Associated;
            else if (DateTime.Compare(p.PickedUp, p.Delivered) > 0)
                return (int)Statuses.Collected;
            return (int)Statuses.Provided;
        }

        /// <summary>
        /// Calculates the amount of battery consumed according to weight and distance and returns the amount
        /// </summary>
        /// <param name="w">The weight the drone carries in int</param>
        /// <param name="l1">Object Location 1</param>
        /// <param name="l2">Object Location 2</param>
        /// <returns>Returns the amount of battery consumed</returns>
        public double ReturnBattery(int w, Location l1, Location l2) {
            if (w == 0)
                return DistanceTo(l1, l2) * WeightLight;
            else if (w == 1)
                return DistanceTo(l1, l2) * WeightMedium;
            else if (w == 2)
                return DistanceTo(l1, l2) * WeightHeavy;
            //w==3: the drone carries nothing
            return DistanceTo(l1, l2) * Avaliable;
        }

        /// <summary>
        /// Calculate what station is closest to the drone's location and returns it
        /// </summary>
        /// <param name="s">list of Station</param>
        /// <param name="drone">Object of Drone's Location</param>
        /// <returns>Returns a station object</returns>
        public IDAL.DO.Station ReturnCloseStation(IEnumerable<IDAL.DO.Station> s, Location drone) {
            Location l1 = new Location();
            Location l2 = new Location();
            IDAL.DO.Station st  = new IDAL.DO.Station();
            bool first = false;
            foreach (var item in s) {
                l1.Longitude = item.Longitude;
                l1.Lattitude = item.Lattitude;
                //First stop or closer stop
                if (!first || DistanceTo(l1, drone) < DistanceTo(l2, drone)) {
                    first = true;
                    st = item;
                    l2.Longitude = st.Longitude;
                    l2.Lattitude = st.Lattitude;
                }
            }
            return st;
        }
        /// <summary>
        /// Calculates the distance between two locations and returns it
        /// </summary>
        /// <param name="l1">Object Location 1</param>
        /// <param name="l2">Object Location 2</param>
        /// <returns>Returns the distance between two locations</returns>
        public double DistanceTo(Location l1, Location l2) {
                double rlat1 = Math.PI * l1.Lattitude / 180;
                double rlat2 = Math.PI * l2.Lattitude / 180;
                double theta = l1.Longitude - l2.Longitude;
                double rtheta = Math.PI * theta / 180;
                double dist = Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) * Math.Cos(rlat2) * Math.Cos(rtheta);
                dist = Math.Acos(dist);
                dist = dist * 180 / Math.PI;
                dist = dist * 60 * 1.1515;
                return Math.Round(dist * 1.609344, 2);
            }

        /// <summary>
        /// Checks if the ״id״ already exists, if there is an error return
        /// </summary>
        /// <param name="list">List of T-objects</param>
        /// <param name="id">The id for check</param>
        /// <typeparam name="T">The type of the list</typeparam>
        /// <returns>Nothing</returns>
        public void CheckExistId <T>(IEnumerable<T> list, int id)
        {
            foreach (var item in list) {
                int id_object = (int)(typeof(T).GetProperty("Id").GetValue(item, null));
                if (id_object == id)
                    throw new IdExistException(id);   
            }
        }

        /// <summary>
        /// Checks if ״id״ does not exist, if not returns error
        /// </summary>
        /// <param name="list">List of T-objects</param>
        /// <param name="id">The id for check</param>
        /// <typeparam name="T">The type of the list</typeparam>
        /// <returns>Nothing</returns>
        public void CheckNotExistId <T>(IEnumerable<T> list, int id)
        {
            foreach (var item in list) {
                int id_object = (int)(typeof(T).GetProperty("Id").GetValue(item, null));
                if (id_object == id)
                    return;          
            }
            throw new IdNotExistException(id);   
        }

        /// <summary>
        /// Checks if the drone can to send for cahrge, if not returns error
        /// </summary>
        /// <param name="list">List of T-objects</param>
        /// <param name="dl">Object of Drone</param>
        /// <typeparam name="T">he type of the list</typeparam>
        /// <returns>Nothing</returns>
        public double CheckDroneCannotSend(IEnumerable<IDAL.DO.Station> list, DroneList dl)
        {
            Location l = new Location();
            Location locationStation = new Location();
            bool help = false;
            foreach (var item in list) {
                locationStation.Lattitude = item.Lattitude;
                locationStation.Longitude = item.Longitude;
                if ((!help || DistanceTo(locationStation, dl.CLocation) < DistanceTo(l, dl.CLocation)) && item.ChargeSlots > 0) {
                    help = true;
                    l = locationStation;
                }
            }
            double battery = ReturnBattery(3, dl.CLocation, l);
            
            //If the drone is not available or the drone will not be able to reach the station
            if (dl.Status != DroneStatuses.Available || dl.Battery < battery)
                throw new DroneCannotSend();     

            return battery;
        }

        /// <summary>
        /// Checks if the drone can to relese from cahrge, if not returns error
        /// </summary>
        /// <param name="dl">Object of Drone</param>
        /// <returns>Nothing</returns>
        public void CheckDroneCannotRelese(DroneList dl)
        {
            if (dl.Status != DroneStatuses.Maintenance )
                throw new DroneCannotRelese();
        }  

        /// <summary>
        /// Checks if the drone can to pick up the parcel, if not returns error
        /// </summary>
        /// <param name="p">Object of parcel</param>
        /// <returns>Nothing</returns>
        public void CheckDroneCannotPickUp(IDAL.DO.Parcel p)
        {
            if (ReturnStatus(p) != 1)
                throw new DroneCannotPickUp();
        } 

        /// <summary>
        /// Checks if the drone can to deliver the parcel, if not returns error
        /// </summary>
        /// <param name="p">Object of parcel</param>
        /// <returns>Nothing</returns>
        public void CheckDroneCannotDeliver(IDAL.DO.Parcel p)
        {
            if (ReturnStatus(p) != 2)
                throw new DroneCannotDeliver();
        } 
    }
}
