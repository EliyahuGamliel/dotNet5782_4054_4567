using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public interface IBL
    {
        void AddStation(Station s);
        void AddDrone(Drone d, int idStation);
        int AddParcel(Parcel p);
        void AddCustomer(Customer c);
        void UpdateDrone(int id, int name);
        void UpdateCustomer(int id, string nameCustomer, string phoneCustomer);
        void UpdateStation(int id, int nameStation, int chargeSlots);
        void UpdateParcel(int id, int name);
        void AssignDroneParcel(int DroneId);
        void PickUpDroneParcel(int id);
        void DeliverParcelCustomer(int id);
        void SendDrone(int idDrone);
        void ReleasDrone(int id, double time); 
        string GetParcelById(int Id);
        string GetDroneById(int Id);
        string GetCustomerById(int Id);
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
