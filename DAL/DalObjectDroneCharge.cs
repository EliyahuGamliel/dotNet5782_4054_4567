using System;
using IDAL.DO;
using System.Collections.Generic;

namespace DalObject
{
    public partial class DalObject : IDAL.IDal
    {

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