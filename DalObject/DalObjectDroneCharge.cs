using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DalApi;
using DO;

namespace Dal
{
    /// <summary>
    /// Class partial DalObject - All functions running on the list of dronesCharge
    /// </summary>
    public partial class DalObject : IDal
    {
        /// <summary>
        /// If everything is fine, add a droneCharge to the list of dronesCharge
        /// </summary>
        /// <param name="d">Object of droneCharge to add</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDroneCharge(DroneCharge d) {
            DataSource.DroneCharges.Add(d);
        }

        /// <summary>
        /// If everything is fine, delete a droneCharge from the list of dronesCharge
        /// </summary>
        /// <param name="d">Object of droneCharge to delete</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDroneCharge(DroneCharge d) {
            CheckNotExistId(DataSource.Drones, d.DroneId);
            CheckNotExistId(DataSource.Stations, d.StationId);
            int index = DataSource.DroneCharges.FindIndex(dc => dc.Active && d.DroneId == dc.DroneId);
            DataSource.DroneCharges[index] = d;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneCharge GetDroneChargeById(int Id) {
            DroneCharge dC = DataSource.DroneCharges.Find(d => Id == d.DroneId && d.Active);
            return dC;
        }

        /// <summary>
        /// Returns all the drone charges that fit the filter
        /// </summary>
        /// <param name="droneChargeList">The paradicte to filter</param>
        /// <returns>The Ienumerable to the drone charges</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> GetDroneChargeByFilter(Predicate<DroneCharge> droneChargeList) {
            return DataSource.DroneCharges.FindAll(droneChargeList);
        }
    }
}
