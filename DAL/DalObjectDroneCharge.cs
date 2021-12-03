using System;
using IDAL.DO;
using System.Collections.Generic;

namespace DalObject
{
    /// <summary>
    /// Class partial DalObject - All functions running on the list of dronesCharge
    /// </summary>
    public partial class DalObject : IDAL.IDal
    {
        /// <summary>
        /// If everything is fine, add a droneCharge to the list of dronesCharge
        /// </summary>
        /// <param name="d">Object of droneCharge to add</param>
        public void AddDroneCharge(DroneCharge d) {
            DataSource.DroneCharges.Add(d);
        }

        /// <summary>
        /// If everything is fine, delete a droneCharge from the list of dronesCharge
        /// </summary>
        /// <param name="d">Object of droneCharge to delete</param>
        public void DeleteDroneCharge(DroneCharge d) {
            CheckNotExistId(DataSource.Drones, d.DroneId);
            CheckNotExistId(DataSource.Stations, d.StationId);
            DataSource.DroneCharges.Remove(d);
        }

        /// <summary>
        /// Returns all the drone charges that fit the filter
        /// </summary>
        /// <param name="droneChargeList">the paradicte</param>
        /// <returns> the Ienumerable to the drone charges</returns>
        public IEnumerable<DroneCharge> GetDroneChargeByFilter(Predicate<DroneCharge> droneChargeList) {
            return DataSource.DroneCharges.FindAll(droneChargeList);
        }
    }
}
