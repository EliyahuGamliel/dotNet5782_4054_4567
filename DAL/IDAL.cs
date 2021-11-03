using System;
using IDAL.DO;
using System.Collections.Generic;

namespace IDAL
{
    public interface IDAL
    { 
        double DistancePrint(double lat1, double lon1, char letter, int id);
        void AddStation(Station s);
        void AddDrone(Drone d);
        int AddParcel(Parcel p);
        void AddCustomer(Customer c);
        void AssignDroneParcel(int DroneId, int ParcelId);
        void PickUpDroneParcel(int id);
        void DeliverParcelCustomer(int id);
        void SendDrone(int idDrone, int idStation);
        void ReleasDrone(int id); 
        string GetCustomerById(int Id);
        string GetDroneById(int Id);
        string GetParcelById(int Id);
        string GetStationById(int Id);
        IEnumerable<Station> GetStations();
        IEnumerable<Drone> GetDrones();
        IEnumerable<Customer> GetCustomers();
        IEnumerable<Parcel> GetParcels();
        IEnumerable<Parcel> GetParcelDrone();
        IEnumerable<Station> GetStationCharge();
        double[] DroneElectricityUse();
    }
}
