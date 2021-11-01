using System;
using IDAL.DO;
using System.Collections.Generic;

namespace DalObject
{
    public partial class DalObject : IDAL.IDAL
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
        public int AddParcel(int Id, int SenderId, int TargetId, int Weight, int priority, int droneId) {
            DataSource.Config.Number_ID += 1;
            Parcel p = new Parcel();
            p.Id = Id;
            p.SenderId = SenderId;
            p.TargetId = TargetId;
            p.Weight = (WeightCategories)Weight;
            p.Priority = (Priorities)priority;
            p.TargetId = TargetId;
            p.Requested = DateTime.Now;
            p.DroneId = droneId;
            DataSource.parcels.Add(p);
            return DataSource.Config.Number_ID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void AssignDroneParcel(int DroneId, int ParcelId) {
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
            Parcel p = DataSource.parcels.Find(pa => id == pa.Id);
            int index = DataSource.parcels.IndexOf(p);
            p.PickedUp = DateTime.Now;
            DataSource.parcels[index] = p;
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
        
    }
}