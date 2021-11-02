using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL : IBL
    {
        
        public void AddDrone(Drone d){
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
            return data.PrintListDrone();
        }
        public double[] DroneElectricityUse(){
            return data.DroneElectricityUse();
        }
    }
}
