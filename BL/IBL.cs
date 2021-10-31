using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public interface IBL
    {
        void AddStation(int Id, int Name, double Longitude, double Lattitude, int ChargeSlots);
        void AddDrone(int Id, string Model, int MaxWeight, int Status, double Battery);
        int AddParcel(int Id, int SenderId, int TargetId, int Weight, int priority, int droneId);
        void AddCustomer(int Id, string Name, string Phone, double Longitude, double Lattitude);
        void AssignDroneParcel(int DroneId, int ParcelId);
        void PickUpDroneParcel(int id);
        void DeliverParcelCustomer(int id);
        void SendDrone(int idDrone, int idStation);
        void ReleasDrone(int id); 
        string PrintById(int Id, int num);
        IEnumerable<Station> PrintListStation();
        IEnumerable<Drone> PrintListDrone();
        IEnumerable<Customer> PrintListCustomer();
        IEnumerable<Parcel> PrintListParcel();
        IEnumerable<Parcel> PrintListParcelDrone();
        IEnumerable<Station> PrintListStationCharge();
        double[] DroneElectricityUse();
    }
}
