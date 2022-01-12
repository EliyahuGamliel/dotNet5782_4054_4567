using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DalApi;
using DO;

namespace Dal
{
    /// <summary>
    /// Class partial DalObject - All functions running on the list of drones
    /// </summary>
    public partial class DalObject : IDal
    {
        /// <summary>
        /// If everything is fine, add a drone to the list of drones
        /// </summary>
        /// <param name="d">Object of drone to add</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(Drone d) {
            CheckExistId(DataSource.Drones, d.Id);
            DataSource.Drones.Add(d);
        }

        /// <summary>
        /// If all is fine, update the drone in a list of drones
        /// </summary>
        /// <param name="d">Object of drone to update</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDrone(Drone d) {
            CheckNotExistId(DataSource.Drones, d.Id);
            Drone dr = DataSource.Drones.Find(dro => dro.Id == d.Id);
            int index = DataSource.Drones.IndexOf(dr);
            DataSource.Drones[index] = d;
        }

        /// <summary>
        /// If all is fine, return a drone object by id
        /// </summary>
        /// <param name="Id">The id of the requested drone</param>
        /// <returns>The object of the requested drone</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDroneById(int Id) {
            CheckNotExistId(DataSource.Drones, Id);
            Drone d = DataSource.Drones.Find(dr => Id == dr.Id);
            return d;
        }

        /// <summary>
        /// Returns all the drones that fit the filter
        /// </summary>
        /// <param name="droneList">The paradicte to filter</param>
        /// <returns>The Ienumerable to the drones</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDroneByFilter(Predicate<Drone> droneList) {
            return DataSource.Drones.FindAll(droneList);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDrone(DO.Drone drone) {
            int index = DataSource.Drones.FindIndex(d => d.Id == drone.Id && d.Active);
            DataSource.Drones[index] = drone;
        }

        /// <summary>
        /// Returns an array with all fields of power consumption
        /// </summary>
        /// <returns>Returns an array with all fields of power consumption</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public double[] DroneElectricityUse() {
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
