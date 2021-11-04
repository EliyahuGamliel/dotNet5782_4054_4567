using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL : IBL
    {
        IDAL.IDal data;
        internal static List<Drone> drones = new List<Drone>();

        public BL()
        {
            IDAL.IDal data = new DalObject.DalObject();
        }
        
        public void AssignDroneParcel(int DroneId){

        }
        public void PickUpDroneParcel(int id){

        }
        public void DeliverParcelCustomer(int id){

        }
    }
}
