using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public interface IBL
    {
        void AddStation(Station s);
        void AddDrone(Drone d);
        int AddParcel(Parcel p);
        void AddCustomer(Customer c);
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
