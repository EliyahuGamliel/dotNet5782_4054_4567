using System;
using IBL.BO;

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
        void ReleasDrone(int id){

        }
        IEnumerable<Drone> PrintListDrone(){

        }
        double[] DroneElectricityUse(){

        }
    }
}
