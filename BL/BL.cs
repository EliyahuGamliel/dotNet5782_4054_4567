using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL : IBL
    {
        IDAL.IDal data;
        List<DroneList> dronesList = new List<DroneList>();
        double Avaliable;
        double WeightLight;
        double WeightMedium;
        double WeightHeavy;
        double ChargingRate;
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
            foreach (var item in list_d)
            {
                bool help = true;
                double min_battery = 0;

                DroneList dl = new DroneList();
                dl.Id = item.Id;
                dl.MaxWeight = (WeightCategories)(int)item.MaxWeight;
                dl.Model = item.Model;

                IEnumerable<IDAL.DO.Parcel> list_p = data.GetParcels();
                foreach (var itemParcel in list_p) {
                    if (itemParcel.DroneId == dl.Id && (ReturnStatus(itemParcel) == 1 || ReturnStatus(itemParcel) == 2)) {
                        dl.Status = DroneStatuses.Delivery;
                        dl.ParcelId = itemParcel.Id;
                        if (ReturnStatus(itemParcel) == 2) {
                            Customer c = GetCustomerById(itemParcel.SenderId);
                            dl.CLocation.Longitude = ReturnCloseStation(data.GetStations(), c.Location).Longitude;
                            dl.CLocation.Lattitude = ReturnCloseStation(data.GetStations(), c.Location).Lattitude;
                        }
                        else {
                            Customer c = GetCustomerById(itemParcel.SenderId);
                            dl.CLocation = c.Location;
                        }

                        Customer cu = GetCustomerById(itemParcel.TargetId);
                        Location l1 = new Location();
                        l1 = cu.Location;

                        Location l2 = new Location();
                        l2.Lattitude = ReturnCloseStation(data.GetStations(), l1).Lattitude;
                        l2.Longitude = ReturnCloseStation(data.GetStations(), l1).Longitude;

                        min_battery = ReturnBattery((int)itemParcel.Weight, DistanceTo(dl.CLocation, l1)) + ReturnBattery(3, DistanceTo(dl.CLocation, l2));
                        dl.Battery = rand.NextDouble() + rand.Next((int)min_battery + 1, 100);
                        if (dl.Battery > 100)
                            dl.Battery = 100;
                        help = false;
                        break;
                    }
                }
                
                if (help)
                {
                    dl.Status = (DroneStatuses)rand.Next(0,2);
                    if (dl.Status == DroneStatuses.Maintenance) {
                        int counter = 0;
                        IEnumerable<IDAL.DO.Station> list_s = data.GetStations();
                        foreach (var itemStation in list_s)
                            counter += 1;
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

                    else {
                        int counter = 0;
                        IEnumerable<CustomerList> list_c = GetCustomers();
                        foreach (var itemCustomer in list_c)
                            if (itemCustomer.ParcelsGet > 0)
                                counter += 1;
                        int s = rand.Next(0, counter);
                        foreach (var itemCustomer in list_c) {
                            if (s == 0) {
                                Customer c = GetCustomerById(itemCustomer.Id);
                                dl.CLocation = c.Location;
                                break;
                            }
                            if (itemCustomer.ParcelsGet > 0)
                                s -= 1;
                        }
                        Location l = new Location();
                        l.Lattitude = ReturnCloseStation(data.GetStations(), dl.CLocation).Lattitude;
                        l.Longitude = ReturnCloseStation(data.GetStations(), dl.CLocation).Longitude;
                        min_battery = ReturnBattery(3, DistanceTo(dl.CLocation, l));
                        dl.Battery = rand.NextDouble() + rand.Next((int)min_battery + 1, 100);
                        if (dl.Battery > 100)
                            dl.Battery = 100;
                    }
                }

                dronesList.Add(dl);
            }
        }
        
        public void AssignDroneParcel(int DroneId){
            try
            {
                Drone d = GetDroneById(DroneId);
                IDAL.DO.Parcel p_choose = new IDAL.DO.Parcel();
                IEnumerable<IDAL.DO.Parcel> list_p = data.GetParcels();
                p_choose = list_p.GetEnumerator().Current;
                foreach (var item in list_p) {
                    if (ReturnStatus(item) == 0 && p_choose.Priority < item.Priority)
                        p_choose = item;
                    else if (ReturnStatus(item) == 0 && p_choose.Priority == item.Priority) {
                        if (p_choose.Weight < item.Weight && (int)item.Weight <= (int)d.MaxWeight)
                            p_choose = item;
                        else if (p_choose.Weight < item.Weight && (int)item.Weight == (int)d.MaxWeight){
                            if (true)
                            {
                                
                            }
                        }
                    }
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        
        public void PickUpDroneParcel(int id){
            try
            {
                DroneList d = dronesList.Find(dr => dr.Id == id);
                Drone d_help = GetDroneById(id);
                int index = dronesList.IndexOf(d);
                IDAL.DO.Parcel p = data.GetParcelById(d.ParcelId);
                if (ReturnStatus(p) == 1)
                {
                    p.PickedUp = DateTime.Now;
                    double battery = d.Battery - ReturnBattery(3, DistanceTo(d.CLocation, d_help.PTransfer.Collection_Location));
                    d.Battery -= battery;
                    d.CLocation = d_help.PTransfer.Collection_Location;
                    dronesList[index] = d;
                    data.UpdateParcel(p);
                }
            }
            catch (System.Exception)
            { 
                throw;
            }
        }

        public void DeliverParcelCustomer(int id){
            try
            {
                Drone d = GetDroneById(id);
                IDAL.DO.Parcel p = data.GetParcelById(d.PTransfer.Id);
                if (ReturnStatus(p) == 2)
                {
                    
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public int ReturnStatus(IDAL.DO.Parcel p)  {
            if (DateTime.Compare(p.Requested, p.Scheduled) > 0)
                return (int)Statuses.Created;
            else if (DateTime.Compare(p.Scheduled, p.PickedUp) > 0)
                return (int)Statuses.Associated;
            else if (DateTime.Compare(p.PickedUp, p.Delivered) > 0)
                return (int)Statuses.Collected;
            return (int)Statuses.Provided;
        }

        public double ReturnBattery(int w, double distance) {
            if (w == 0)
                return distance * WeightLight;
            else if (w == 1)
                return distance * WeightMedium;
            else if (w == 2)
                return distance * WeightHeavy;
            return distance * Avaliable;
        }

        public IDAL.DO.Station ReturnCloseStation(IEnumerable<IDAL.DO.Station> s, Location drone) {
            Location l1 = new Location();
            Location l2 = new Location();
            IDAL.DO.Station st  = new IDAL.DO.Station();
            bool first = false;
            foreach (var item in s)
            {
                l1.Longitude = item.Longitude;
                l1.Lattitude = item.Lattitude;
                if (!first || DistanceTo(l1, drone) < DistanceTo(l2, drone)) {
                    first = true;
                    st = item;
                    l2.Longitude = st.Longitude;
                    l2.Lattitude = st.Lattitude;
                }
            }
            return st;
        }

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

        public void CheckExistId <T>(IEnumerable<T> list, int id)
        {
            foreach (var item in list) {
                int id_object = (int)(typeof(T).GetProperty("Id").GetValue(item, null));
                if (id_object == id)
                    throw new IdExistException(id);   
            }
        }

        public void CheckNotExistId <T>(IEnumerable<T> list, int id)
        {
            foreach (var item in list) {
                int id_object = (int)(typeof(T).GetProperty("Id").GetValue(item, null));
                if (id_object == id)
                    return;          
            }
            throw new IdNotExistException(id);   
        }

        public void CheckNotExistPhone <T>(IEnumerable<T> list, string phone)
        {
            foreach (var item in list) {
                string phone_object = (string)(typeof(T).GetProperty("Phone").GetValue(item, null));
                if (phone_object == phone)
                    throw new PhoneExistException(phone);         
            }
        }

        public double CheckDroneCannotSend <T>(IEnumerable<T> list, DroneList dl)
        {
            Location l = new Location();
            Location locationStation = new Location();
            bool help = false;
            foreach (var item in list)
            {
                int chargeSlots_object = (int)(typeof(T).GetProperty("chargeSlots").GetValue(item, null));
                locationStation.Longitude = (double)(typeof(T).GetProperty("Longitude").GetValue(item, null));
                locationStation.Lattitude = (double)(typeof(T).GetProperty("Lattitude").GetValue(item, null));
                if ((!help || DistanceTo(locationStation, dl.CLocation) < DistanceTo(l, dl.CLocation)) && chargeSlots_object > 0) {
                    help = true;
                    l = locationStation;
                }
            }
            double dis = DistanceTo(dl.CLocation, l);
            double battery = ReturnBattery(3, dis);
            if (dl.Status == DroneStatuses.Available && dl.Battery < battery)
            {
                throw new DroneCannotSend();     
            }
            return battery;
        }
    }
}
