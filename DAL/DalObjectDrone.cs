using System;
using IDAL.DO;
using System.Collections.Generic;

namespace DalObject
{
    /// <summary>
    /// Class partial DalObject - All functions running on the list of drones
    /// </summary>
    public partial class DalObject : IDAL.IDal
    {
        /// <summary>
        /// If everything is fine, add a drone to the list of drones
        /// </summary>
        /// <param name="d">Object of drone to add</param>
        public void AddDrone(Drone d) {
            CheckExistId(DataSource.drones, d.Id);
            DataSource.drones.Add(d);
        }

        /// <summary>
        /// If all is fine, update the drone in a list of drones
        /// </summary>
        /// <param name="d">Object of drone to update</param>
        public void UpdateDrone(Drone d) {
            CheckNotExistId(DataSource.drones, d.Id);
            Drone dr = DataSource.drones.Find(dro => dro.Id == d.Id);
            int index = DataSource.drones.IndexOf(dr);
            DataSource.drones[index] = d;
        }

        /// <summary>
        /// If all is fine, return a drone object by id
        /// </summary>
        /// <param name="Id">The id of the requested drone</param>
        /// <returns>The object of the requested drone</returns>
        public Drone GetDroneById(int Id) {
            CheckNotExistId(DataSource.drones, Id);
            Drone d = DataSource.drones.Find(dr => Id == dr.Id);
            return d;
        }

        /// <summary>
        /// Returns the list of drones
        /// </summary>
        /// <returns>Returns the list of drones</returns>
        public IEnumerable<Drone> GetDrones() {
            return DataSource.drones;
        }

        /// <summary>
        /// Returns an array with all fields of power consumption
        /// </summary>
        /// <returns>Returns an array with all fields of power consumption</returns>
        public double[] DroneElectricityUse(){
            double[] arr = new double[5]; 
            arr[0] = DataSource.Config.Avaliable;
            arr[1] = DataSource.Config.WeightLight;
            arr[2] = DataSource.Config.WeightMedium;
            arr[3] = DataSource.Config.WeightHeavy;
            arr[4] = DataSource.Config.ChargingRate;
            return arr;
        }
    }
}