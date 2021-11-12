using System;
using IDAL.DO;
using System.Collections.Generic;

namespace DalObject
{
    public partial class DalObject : IDAL.IDal
    {
        
        public void AddDrone(Drone d) {
            CheckExistId(DataSource.drones, d.Id);
            DataSource.drones.Add(d);
        }

        public void UpdateDrone(Drone d)
        {
            CheckNotExistId(DataSource.drones, d.Id);
            Drone dr = DataSource.drones.Find(dro => dro.Id == d.Id);
            int index = DataSource.drones.IndexOf(dr);
            DataSource.drones[index] = d;
        }

        public Drone GetDroneById(int Id) {
            CheckNotExistId(DataSource.drones, Id);
            Drone d = DataSource.drones.Find(dr => Id == dr.Id);
            return d;
        }

        public IEnumerable<Drone> GetDrones() {
            return DataSource.drones;
        }

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