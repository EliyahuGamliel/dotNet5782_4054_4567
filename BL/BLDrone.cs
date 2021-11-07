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
                IDAL.DO.Station s = data.GetStationById(idStation);
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
                IDAL.DO.Drone d = data.GetDroneById(id);
                d.Model = model;
                data.UpdateDrone(d, id);
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
            return data.GetDroneById(Id).ToString();
        }
        
        public IEnumerable<DroneList> GetDrones(){
            return dronesList;
        }
        public double[] DroneElectricityUse(){
            return data.DroneElectricityUse();
        }
    }
}
