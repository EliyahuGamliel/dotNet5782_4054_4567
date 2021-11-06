using System;
using IDAL.DO;
using System.Collections.Generic;

namespace DalObject
{
    public partial class DalObject : IDAL.IDal
    {
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
        public int AddParcel(Parcel p) {
            CheckExistId(DataSource.parcels, p.Id);
            CheckNotExistId(DataSource.customers, p.SenderId);
            CheckNotExistId(DataSource.customers, p.TargetId);
            DataSource.Config.Number_ID += 1;
            DataSource.parcels.Add(p);
            return DataSource.Config.Number_ID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void AssignDroneParcel(int DroneId, int ParcelId) {// to fix checking
            CheckNotExistId(DataSource.parcels, ParcelId);
            Parcel p = DataSource.parcels.Find(pa => ParcelId == pa.Id);
            int index = DataSource.parcels.IndexOf(p);
            p.DroneId = DroneId;
            p.Scheduled = DateTime.Now;
            DataSource.parcels[index] = p;
        }

        /// <summary>
        /// the function takes care of picking up the drone using a drone
        /// </summary>
        /// <param name="id"></param>
        public void PickUpDroneParcel(int id) {
            CheckNotExistId(DataSource.parcels, id);
            Parcel p = DataSource.parcels.Find(pa => id == pa.Id);
            int index = DataSource.parcels.IndexOf(p);
            p.PickedUp = DateTime.Now;
            DataSource.parcels[index] = p;
        }
        
        public string GetParcelById(int Id) {
            CheckNotExistId(DataSource.parcels, Id);
            Parcel p = DataSource.parcels.Find(pa => Id == pa.Id);
            return p.ToString();
        }

        /// <summary>
        /// prints the parcels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Parcel> GetParcels() {
            return DataSource.parcels;
        }

        /// <summary>
        /// prints all the parcels that dont have a drone
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Parcel> GetParcelDrone() {
            return DataSource.parcels.FindAll(pa => 0 == pa.DroneId);
        }
        
    }
}