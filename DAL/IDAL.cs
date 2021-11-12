using System;
using IDAL.DO;
using System.Collections.Generic;

namespace IDAL
{
    public interface IDal
    { 
        //double DistancePrint(double lat1, double lon1, char letter, int id);
        void AddStation(Station s);
        void AddDrone(Drone d);
        int AddParcel(Parcel p);
        void AddCustomer(Customer c);
        void AddDroneCharge(DroneCharge d);
        void UpdateDrone(Drone d);
        void UpdateStation(Station s);
        void UpdateCustomer(Customer c, string phone);
        void DeleteDroneCharge(DroneCharge d);
        Customer GetCustomerById(int Id);
        Drone GetDroneById(int Id);
        Parcel GetParcelById(int Id);
        Station GetStationById(int Id);
        IEnumerable<Station> GetStations();
        IEnumerable<Drone> GetDrones();
        IEnumerable<Customer> GetCustomers();
        IEnumerable<Parcel> GetParcels();
        IEnumerable<Parcel> GetParcelDrone();
        IEnumerable<Station> GetStationCharge();
        IEnumerable<DroneCharge> GetDroneCharge();
        double[] DroneElectricityUse();
    }
}
