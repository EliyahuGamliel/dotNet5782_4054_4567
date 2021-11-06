using System;
using IDAL.DO;
using System.Collections.Generic;

namespace DalObject
{
    public partial class DalObject : IDAL.IDal
    {
        /// <summary>
        /// uses the function Initialize() in order to initialise the data
        /// </summary>
        public DalObject() { DataSource.Initialize(); }

/*
        /// <summary>
        /// prints the distance between a given point and a customer/station
        /// </summary>
        /// <param name="lat1">the lattitude of place number 1</param>
        /// <param name="lon1">the longitude of place number 1</param>
        /// <param name="letter">if the user wants to check the distance from a station or a customer</param>
        /// <param name="id">the id of the customer/station</param>
        /// <returns></returns>
        public double DistancePrint(double lat1, double lon1, char letter, int id) {
            double dis;
            if (letter == 'c') {
                Customer c = DataSource.customers.Find(cu => id == cu.Id);
                int index = DataSource.customers.IndexOf(c);
                dis = DataSource.customers[index].DistanceTo(lat1, lon1, DataSource.customers[index].Lattitude, DataSource.customers[index].Longitude);///the sis between the customers
			}
            else {
                Station s = DataSource.stations.Find(st => id == st.Id);
                int index = DataSource.stations.IndexOf(s);
                dis = DataSource.stations[index].DistanceTo(lat1, lon1, DataSource.stations[index].Lattitude, DataSource.stations[index].Longitude);///the dis between the customer and the station
			}
            return dis;
        }
        */

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
            DataSource.drones[index] = d;
        }

        public void CheckExistId <T>(List<T> list, int id)
        {
            foreach (var item in list) {
                int id_object = (int)(typeof(T).GetProperty("Id").GetValue(item, null));
                if (id_object == id)
                    throw new IdExistException(id);   
            }
        }

        public void CheckNotExistId <T>(List<T> list, int id)
        {
            foreach (var item in list) {
                int id_object = (int)(typeof(T).GetProperty("Id").GetValue(item, null));
                if (id_object == id)
                    return;          
            }
            throw new IdNotExistException(id);   
        }

        public void CheckNotExistPhone <T>(List<T> list, string phone)
        {
            foreach (var item in list) {
                string phone_object = (string)(typeof(T).GetProperty("Phone").GetValue(item, null));
                if (phone_object == phone)
                    throw new PhoneExistException(phone);         
            }
        }
    }
}
