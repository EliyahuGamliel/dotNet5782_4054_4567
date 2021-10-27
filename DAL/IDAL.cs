using System;
using IDAL.DO;
using System.Collections.Generic;

namespace IDAL
{
    public interface IDAL
    { 
        public double DistancePrint(double lat1, double lon1, char letter, int id);
        public void AddStation(int Id, int Name, double Longitude, double Lattitude, int ChargeSlots);
        public void AddDrone(int Id, string Model, int MaxWeight, int Status, double Battery);
        public int AddParcel(int Id, int SenderId, int TargetId, int Weight, int priority, int droneId);
        public void AddCustomer(int Id, string Name, string Phone, double Longitude, double Lattitude);
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
        public Station[] PrintListStation() {
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
