using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL : IBL
    {
        IDAL.IDal data;
        List<DroneList> drones = new List<DroneList>();
        MyException exp = new MyException();
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
