using System;
using IDAL.DO;
using System.Collections.Generic;

namespace DalObject
{
    public partial class DalObject : IDAL.IDAL
    {
        /// <summary>
        /// Add a drone at the request of the user to the list
        /// </summary>
        /// <param name="Id">id of drone</param>
        /// <param name="Model">model of drone</param>
        /// <param name="MaxWeight">MaxWeight of drone</param>
        /// <param name="Status">Status of drone</param>
        /// <param name="Battery">Battery of drone</param>
        public void AddDrone(Drone d) {
            DataSource.drones.Add(d);
        }

        /// <summary>
        /// Releases the drone from the charging cell
        /// </summary>
        /// <param name="id">the id of the drone that needs releasing</param>
        public void ReleasDrone(int id) {
            int index;
            DroneCharge dc = DataSource.droneCharges.Find(drch => id == drch.DroneId);
            int stationId = dc.StationId;
            int droneId = dc.DroneId;
            DataSource.droneCharges.Remove(dc);
            Drone d = DataSource.drones.Find(dr => droneId == dr.Id);
            index = DataSource.drones.IndexOf(d);
            DataSource.drones[index] = d;
            Station s = DataSource.stations.Find(st => stationId == st.Id);
            index = DataSource.stations.IndexOf(s);
            s.ChargeSlots += 1;
            DataSource.stations[index] = s;
        }

        /// <summary>
        /// Sends the drone
        /// </summary>
        /// <param name="idDrone">the drone's id</param>
        /// <param name="idStation">the station's id</param>
        public void SendDrone(int idDrone, int idStation) {
            DroneCharge dc = new DroneCharge();
            dc.DroneId = idDrone;
            dc.StationId = idStation;
            DataSource.droneCharges.Add(dc);
            Drone d = DataSource.drones.Find(dr => idDrone == dr.Id);
            int index = DataSource.drones.IndexOf(d);
            DataSource.drones[index] = d;
            Station s = DataSource.stations.Find(st => idStation == st.Id);
            index = DataSource.stations.IndexOf(s);
            s.ChargeSlots -= 1;
            DataSource.stations[index] = s;
        }

        public string GetDroneById(int Id) {
            Drone d = DataSource.drones.Find(dr => Id == dr.Id);
            return d.ToString();
        }

        /// <summary>
        /// prints the drones
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Drone> GetDrones() {
            return DataSource.drones;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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