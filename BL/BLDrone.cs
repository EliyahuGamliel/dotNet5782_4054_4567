using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL : IBL
    {
        
        public void AddDrone(Drone d, int idStation) {
            List<IDAL.DO.Station> stations = (List<IDAL.DO.Station>)data.GetStations();
            IDAL.DO.Station s = stations.Find(pa => idStation == pa.Id);
            Random rand = new Random();
            d.Battery = rand.Next(20,41);
            d.Status = DroneStatuses.Maintenance;
            d.CLocation.Lattitude = s.Lattitude;
            d.CLocation.Longitude = s.Longitude;
            IDAL.DO.Drone dr = new IDAL.DO.Drone();
            dr.Id = d.Id;
            dr.Model = d.Model;
            dr.MaxWeight = (IDAL.DO.WeightCategories)((int)d.MaxWeight);
            data.AddDrone(dr);
        }
        public void SendDrone(int idDrone, int idStation){

        }
        public void ReleasDrone(int id){

        }
        public IEnumerable<IDAL.DO.Drone> PrintListDrone(){
            return data.GetDrones();
        }
        public double[] DroneElectricityUse(){
            return data.DroneElectricityUse();
        }
    }
}
