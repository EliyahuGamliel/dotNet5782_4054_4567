using System;
using IDAL.DO;
using System.Collections.Generic;

namespace DalObject
{
    public partial class DalObject : IDAL.IDal
    {
        /// <summary>
        /// Add a drone at the request of the user to the list
        /// </summary>
        /// <param name="Id">id of drone</param>
        /// <param name="Model">model of drone</param>
        /// <param name="MaxWeight">MaxWeight of drone</param>
        /// <param name="Status">Status of drone</param>
        /// <param name="Battery">Battery of drone</param>
        public void AddDroneCharge(DroneCharge d) {
            CheckExistId(DataSource.drones, d.DroneId);
            CheckExistId(DataSource.stations, d.StationId);
            DataSource.droneCharges.Add(d);
        }

        public void DeleteDroneCharge(DroneCharge d) {
            CheckNotExistId(DataSource.drones, d.DroneId);
            CheckNotExistId(DataSource.stations, d.StationId);
            DataSource.droneCharges.Remove(d);
        }

        public IEnumerable<DroneCharge> GetDroneCharge() {
            return DataSource.droneCharges;
        }
    }
}