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
                if (phone_object == phone) {
                    System.Console.WriteLine(phone_object); //
                    throw new PhoneExistException(phone);
                }         
            }
        }
    }
}
