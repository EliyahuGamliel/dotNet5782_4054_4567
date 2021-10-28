using System;
using IDAL.DO;
using System.Collections.Generic;

namespace DalObject
{
    public class DataSource
    {
        internal static List<Drone> drones = new List<Drone>();
        internal static List<Station> stations = new List<Station>();
        internal static List<Customer> customers = new List<Customer>();
        internal static List<Parcel> parcels = new List<Parcel>();
        internal static List<DroneCharge> droneCharges = new List<DroneCharge>();

        internal class Config {
            public static int Number_ID = 0;
        }

        /// <summary>
        /// The function initialise the data at the beginning
        /// </summary>
        public static void Initialize() {
            Random r = new Random();
            //Drones
            int l = r.Next(5, 11);
            for (int i = 0; i < l; ++i) {
                int rid = r.Next();
                for (int h = 0; h < i; ++h) {
					if (rid == drones[h].Id) {
                        ///to check if the id already exists
                        i -= 1;
                        rid = r.Next();
                    }
                }

                Drone d = new Drone();
                d.Id = rid;
                d.Model = ("Mark" + i);
                d.MaxWeight = (WeightCategories)(r.Next(0, 3));
                d.Status = (DroneStatuses)(r.Next(0, 3));  //change the status so they wil be different 
                d.Battery = 100;
                drones.Add(d);
            }


            //Station
            l = r.Next(2, 6);
            for (int i = 0; i < l; ++i) {
                int rid = r.Next();
                for (int h = 0; h < i; ++h) {
                    if (rid == stations[h].Id) {
                        h = -1;
                        rid = r.Next();
                    }
                }

                Station s = new Station();
                s.ChargeSlots = 2 + r.Next(0, 3);
                s.Id = rid;
                s.Name = s.Id;
                s.Longitude = r.NextDouble() + r.Next(-180, 180);
                s.Lattitude = r.NextDouble() + r.Next(-90, 90);
                stations.Add(s);
            }


            //Customer
            l = r.Next(10, 101);
            for (int i = 0; i < l; ++i) {
                int rid = r.Next();
                for (int h = 0; h < i; ++h) {
                    if (rid == customers[h].Id) {
                        h = -1;
                        rid = r.Next();
                    }
                }

                Customer c = new Customer();
                c.Id = rid;
                c.Name = ("MyNameIs" + i);
                string ph = "0537589982";
                rid = r.Next(1000, 10000);
                for (int h = 0; h < i; ++h) {
                    ph = "053758" + rid;
                    if (ph == customers[h].Phone) {
                        //it takes a random number from 0000 to 9999 and add it to "053758"
                        rid = r.Next();
                        h = -1;
                    }
                }
                c.Phone = ph;
                c.Longitude = r.NextDouble() + r.Next(-180, 180);
                c.Lattitude = r.NextDouble() + r.Next(-90, 90);
                customers.Add(c);
            }

            ///Parcel
            int IndexOfSender = 0;
            l = r.Next(10, 1001);
            for (int i = 0; i < l; ++i) {
                Parcel p = new Parcel();
                p.Id = i;
                p.Requested = new DateTime(2021, r.Next(10, 13), r.Next(1, 28), r.Next(0, 24), r.Next(0, 60), r.Next(0, 60));
                int len = customers.Count;
                int ind = r.Next(0, len);
                p.TargetId = customers[ind].Id;
                IndexOfSender = ind;
                len = drones.Count;

                //if(p.Id < len)
                //{
                //    p.DroneId = drones[p.Id].Id;
                //    p.Scheduled = new DateTime(2021, r.Next(10, 13), r.Next(1, 28), r.Next(0, 24), r.Next(0, 60), r.Next(0, 60));
                //    while (DateTime.Compare(p.Requested, p.Scheduled) > 0)
                //        p.Scheduled = new DateTime(2021, r.Next(10, 13), r.Next(1, 28), r.Next(0, 24), r.Next(0, 60), r.Next(0, 60));
                //}
                //else
                //{
                //    p.DroneId = 0;
                //}

                for (int h = 0; h < len; ++h) {
                    if (drones[h].Status == DroneStatuses.Delivery) {
                        p.DroneId = drones[p.Id].Id;
                        p.Scheduled = new DateTime(2021, r.Next(10, 13), r.Next(1, 28), r.Next(0, 24), r.Next(0, 60), r.Next(0, 60));
                        while (DateTime.Compare(p.Requested, p.Scheduled) > 0)
                            p.Scheduled = new DateTime(2021, r.Next(10, 13), r.Next(1, 28), r.Next(0, 24), r.Next(0, 60), r.Next(0, 60));
                    }
                }


                len = customers.Count;
                int ind2 = r.Next(0, len);
                //so that the sender and the target won't be the same customer
                while (ind2 == ind) {
                    ind2 = r.Next(0, len);
                }
                p.SenderId = customers[ind2].Id;
                p.Weight = (WeightCategories)(r.Next(0, 3));
                p.priority = (Priorities)(r.Next(0, 3));
                parcels.Add(p);
            }
        }
    }

    public class DalObject : IDAL.IDAL
    {
        /// <summary>
        /// uses the function Initialize() in order to initialise the data
        /// </summary>
        public DalObject() { DataSource.Initialize(); }

        /// <summary>
        /// prints the distance between a given point and a customer/station
        /// </summary>
        /// <param name="lat1">the lattitude of place number 1</param>
        /// <param name="lon1">the longitude of place number 1</param>
        /// <param name="letter">if the user wants to check the distance from a station or a customer</param>
        /// <param name="id">the id of the customer/station</param>
        /// <returns></returns>
        public double DistancePrint(double lat1, double lon1, char letter, int id)
        {
            double dis;
            if (letter == 'c')
            {
                Customer c = DataSource.customers.Find(cu => id == cu.Id);
                int index = DataSource.customers.IndexOf(c);
                dis = DataSource.customers[index].DistanceTo(lat1, lon1, DataSource.customers[index].Lattitude, DataSource.customers[index].Longitude);///the sis between the customers
			}
            else
            {
                Station s = DataSource.stations.Find(st => id == st.Id);
                int index = DataSource.stations.IndexOf(s);
                dis = DataSource.stations[index].DistanceTo(lat1, lon1, DataSource.stations[index].Lattitude, DataSource.stations[index].Longitude);///the dis between the customer and the station
			}
            return dis;
        }

        /// <summary>
        /// Add a station at the request of the user to the list
        /// </summary>
        /// <param name="Id">id of station</param>
        /// <param name="Name">name of station</param>
        /// <param name="Longitude">Longitude of station</param>
        /// <param name="Lattitude">Lattitude of station</param>
        /// <param name="ChargeSlots">Number of available charging stations</param>
        public void AddStation(int Id, int Name, double Longitude, double Lattitude, int ChargeSlots)
        {
            Station s = new Station();
            s.Id = Id;
            s.Name = Name;
            s.Longitude = Longitude;
            s.Lattitude = Lattitude;
            s.ChargeSlots = ChargeSlots;
            DataSource.stations.Add(s);
        }

        /// <summary>
        /// Add a drone at the request of the user to the list
        /// </summary>
        /// <param name="Id">id of drone</param>
        /// <param name="Model">model of drone</param>
        /// <param name="MaxWeight">MaxWeight of drone</param>
        /// <param name="Status">Status of drone</param>
        /// <param name="Battery">Battery of drone</param>
        public void AddDrone(int Id, string Model, int MaxWeight, int Status, double Battery)
        {
            Drone d = new Drone();
            d.Id = Id;
            d.Model = Model;
            d.MaxWeight = (WeightCategories)MaxWeight;
            d.Status = (DroneStatuses)Status;
            d.Battery = Battery;
            DataSource.drones.Add(d);
        }

        /// <summary>
        /// Add a parcel at the request of the user to the list
        /// </summary>
        /// <param name="Id">id of parcel</param>
        /// <param name="SenderId">SenderId of parcel</param>
        /// <param name="TargetId">TargetId of parcel</param>
        /// <param name="Weight">Weight of parcel</param>
        /// <param name="priority">priority of parcel</param>
        /// <param name="droneId">droneId of parcel</param>
        /// <returns></returns>
        public int AddParcel(int Id, int SenderId, int TargetId, int Weight, int priority, int droneId)
        {
            DataSource.Config.Number_ID += 1;
            Parcel p = new Parcel();
            p.Id = Id;
            p.SenderId = SenderId;
            p.TargetId = TargetId;
            p.Weight = (WeightCategories)Weight;
            p.priority = (Priorities)priority;
            p.TargetId = TargetId;
            p.Requested = DateTime.Now;
            p.DroneId = droneId;
            DataSource.parcels.Add(p);
            return DataSource.Config.Number_ID;
        }

        /// <summary>
        /// Add a customer at the request of the user to the list
        /// </summary>
        /// <param name="Id">id of customer</param>
        /// <param name="Name">name of customer</param>
        /// <param name="Phone"> phone of customer</param>
        /// <param name="Longitude">Longitude of customer</param>
        /// <param name="Lattitude">Lattitude of customer</param>
        public void AddCustomer(int Id, string Name, string Phone, double Longitude, double Lattitude) {
            Customer c = new Customer();
            c.Id = Id;
            c.Name = Name;
            c.Phone = Phone;
            c.Longitude = Longitude;
            c.Lattitude = Lattitude;
            DataSource.customers.Add(c);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void AssignDroneParcel(int id) {
            Parcel p = DataSource.parcels.Find(pa => id == pa.Id);
            WeightCategories w = p.Weight;
            int index = DataSource.parcels.IndexOf(p);
            for (int i = 0; i < DataSource.drones.Count; i++)
            {
                if (DataSource.drones[i].MaxWeight >= w && DataSource.drones[i].Status == DroneStatuses.Available)
                {///if the drone can pick up the parcel
					Drone d = DataSource.drones[i];
                    d.Status = DroneStatuses.Delivery;
                    DataSource.drones[i] = d;
                    p.DroneId = d.Id;
                    p.Scheduled = DateTime.Now;
                    DataSource.parcels[index] = p;
                    return;
                }
            }
        }

        /// <summary>
        /// the function takes care of picking up the drone using a drone
        /// </summary>
        /// <param name="id"></param>
        public void PickUpDroneParcel(int id) {
			Parcel p = DataSource.parcels.Find(pa => id == pa.Id);
            int index = DataSource.parcels.IndexOf(p);
            p.PickedUp = DateTime.Now;
            DataSource.parcels[index] = p;
        }

        /// <summary>
        /// the function takes care of delivering the parcel to the customer
        /// </summary>
        /// <param name="id">the id of the parcel that needs to be delivered</param>
        public void DeliverParcelCustomer(int id) {
			Parcel p = DataSource.parcels.Find(pa => id == pa.Id);
            int index = DataSource.parcels.IndexOf(p);
            p.Delivered = DateTime.Now;
            DataSource.parcels[index] = p;
            Drone d = DataSource.drones.Find(dr => p.DroneId == dr.Id);
            index = DataSource.drones.IndexOf(d);
            d.Status = DroneStatuses.Available;
            DataSource.drones[index] = d;
        }

        /// <summary>
        /// Sends the drone
        /// </summary>
        /// <param name="idDrone">the drone's id</param>
        /// <param name="idStation">the station's id</param>
        public void SendDrone(int idDrone, int idStation) {
            DroneCharge dc = new DroneCharge();
            dc.DroneId = idDrone;
            dc.StationId = idStation;
            DataSource.droneCharges.Add(dc);
            Drone d = DataSource.drones.Find(dr => idDrone == dr.Id);
            int index = DataSource.drones.IndexOf(d);
            d.Status = DroneStatuses.Maintenance;
            DataSource.drones[index] = d;
            Station s = DataSource.stations.Find(st => idStation == st.Id);
            index = DataSource.stations.IndexOf(s);
            s.ChargeSlots -= 1;
            DataSource.stations[index] = s;
        }

        /// <summary>
        /// Releases the drone from the charging cell
        /// </summary>
        /// <param name="id">the id of the drone that needs releasing</param>
        public void ReleasDrone(int id) {
            int index;
            DroneCharge dc = DataSource.droneCharges.Find(drch => id == drch.DroneId);
            int stationId = dc.StationId;
            int droneId = dc.DroneId;
            DataSource.droneCharges.Remove(dc);
            Drone d = DataSource.drones.Find(dr => droneId == dr.Id);
            index = DataSource.drones.IndexOf(d);
            d.Status = DroneStatuses.Available;
            d.Battery = 100;
            DataSource.drones[index] = d;
            Station s = DataSource.stations.Find(st => stationId == st.Id);
            index = DataSource.stations.IndexOf(s);
            s.ChargeSlots += 1;
            DataSource.stations[index] = s;
        }

        /// <summary>
        /// Prints the item
        /// </summary>
        /// <param name="Id">the id of the item</param>
        /// <param name="num">the number of what needs to be printed</param>
        /// <returns></returns>
        public string PrintById(int Id, int num) {
            switch (num)
            {
                //For print a selected station
                case 1:
                    Station s = DataSource.stations.Find(st => Id == st.Id);
                    return s.ToString();

                //For print a selected drone	
                case 2:
                    Drone d = DataSource.drones.Find(dr => Id == dr.Id);
                    return d.ToString();

                //For print a selected customer	
                case 3:
                    Customer c = DataSource.customers.Find(cu => Id == cu.Id);
                    return c.ToString();

                //For print a selected parcel	
                case 4:
                    Parcel p = DataSource.parcels.Find(pa => Id == pa.Id);
                    return p.ToString();
            }
            return " ";
        }

        /// <summary>
        /// prints the stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Station> PrintListStation() {
            return DataSource.stations.ToArray();
        }

        /// <summary>
        /// prints the drones
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Drone> PrintListDrone() {
            return DataSource.drones;
        }

        /// <summary>
        /// prints the customers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> PrintListCustomer() {
            return DataSource.customers;
        }

        /// <summary>
        /// prints the parcels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Parcel> PrintListParcel() {
            return DataSource.parcels;
        }

        /// <summary>
        /// prints all the parcels that dont have a drone
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Parcel> PrintListParcelDrone() {
            return DataSource.parcels.FindAll(pa => 0 == pa.DroneId);
        }

        /// <summary>
        /// prints all the stations with avaliable charging slots
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Station> PrintListStationCharge() {
            return DataSource.stations.FindAll(st => 0 != st.ChargeSlots);
        }
    }
}
