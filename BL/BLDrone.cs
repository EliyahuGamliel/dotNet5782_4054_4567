using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL : IBL
    {
        Random rand = new Random();
        
        public string AddDrone(DroneList d, int idStation) {
            try
            {
                List<IDAL.DO.Station> stations = (List<IDAL.DO.Station>)data.GetStations();
                IDAL.DO.Station s = stations.Find(pa => idStation == pa.Id);
                d.Battery = rand.Next(20,41);
                d.Status = DroneStatuses.Maintenance;
                d.CLocation.Lattitude = s.Lattitude;
                d.CLocation.Longitude = s.Longitude;
                d.ParcelId = 0;
                IDAL.DO.Drone dr = new IDAL.DO.Drone();
                dr.Id = d.Id;
                dr.Model = d.Model;
                dr.MaxWeight = (IDAL.DO.WeightCategories)((int)d.MaxWeight);
                data.AddDrone(dr);
                dronesList.Add(d);
                return "The addition was successful";
            }
            catch (IDAL.DO.IdExistException)
            {
                throw new IdExistException(d.Id);
            }
        }

        public string UpdateDrone(int id, string model) {
            try
            {
                List<IDAL.DO.Drone> list_d = (List<IDAL.DO.Drone>)data.GetDrones();
                CheckNotExistId(list_d, id);
                IDAL.DO.Drone d = list_d.Find(dr => id == dr.Id);
                int index = list_d.IndexOf(d);
                d.Model = model;
                data.UpdateDrone(d, index);
                //Drone d = dronesList.Find(dr => id.)
                //IDAL.DO.Drone d = drones.Find(dr => idStation == pa.Id);
                return "The addition was successful";
            }
            catch (IDAL.DO.IdNotExistException exp)
            {
                throw exp;
            }
        }

        public void SendDrone(int idDrone) {
            DroneList d = dronesList.Find(dr => idDrone == dr.Id);
            return;
        }

        public void ReleasDrone(int id, double time){
            

            return;
        }

        public string GetDroneById(int Id) {
            return data.GetDroneById(Id);
        }
        
        public IEnumerable<IDAL.DO.Drone> GetDrones(){
            return data.GetDrones();
            //return (List<Drone>)data.GetDrones();
        }
        public double[] DroneElectricityUse(){
            return data.DroneElectricityUse();
        }
    }
}
