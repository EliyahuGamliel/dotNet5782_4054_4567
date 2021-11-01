using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL : IBL
    {
        static IDAL.IDAL data;
        
        public void AssignDroneParcel(int DroneId, int ParcelId){

        }
        public void PickUpDroneParcel(int id){

        }
        public void DeliverParcelCustomer(int id){

        }
        public string PrintById(int Id, int num){
            return data.PrintById(Id, num);
        }
        
        public IEnumerable<IDAL.DO.Parcel> PrintListParcelDrone(){
            return data.PrintListParcelDrone();
        }
        public IEnumerable<IDAL.DO.Station> PrintListStationCharge(){
            return data.PrintListStationCharge();
        }
    }
}
