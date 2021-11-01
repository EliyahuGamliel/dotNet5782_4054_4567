using System;
using IBL.BO;

namespace IBL
{
    public class BL : IBL
    {
        public void AddStation(int Id, int Name, Location loca, int ChargeSlots){
            
        }
        public void AddDrone(int Id, string Model, int MaxWeight, int Status, double Battery){

        }
        public int AddParcel(int Id, int SenderId, int TargetId, int Weight, int priority, int droneId){

        }
        public void AddCustomer(int Id, string Name, string Phone, double Longitude, double Lattitude){

        }
        public void AssignDroneParcel(int DroneId, int ParcelId){

        }
        public void PickUpDroneParcel(int id){

        }
        public void DeliverParcelCustomer(int id){

        }
        public void SendDrone(int idDrone, int idStation){

        }
        void ReleasDrone(int id){

        }
        string PrintById(int Id, int num){

        }
        IEnumerable<Station> PrintListStation(){

        }
        IEnumerable<Drone> PrintListDrone(){

        }
        IEnumerable<Customer> PrintListCustomer()|{

        }
        IEnumerable<Parcel> PrintListParcel(){

        }
        IEnumerable<Parcel> PrintListParcelDrone(){

        }
        IEnumerable<Station> PrintListStationCharge(){

        }
        double[] DroneElectricityUse(){

        }
    }
}
