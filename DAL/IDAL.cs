using System;
using IDAL.DO;
using System.Collections.Generic;

namespace IDAL
{
    /// <summary>
    /// Defining the "IDal" interface
    /// </summary>
    public interface IDal
    {
        void AddStation(Station s);
        void AddDrone(Drone d);
        int AddParcel(Parcel p);
        void AddCustomer(Customer c);
        void AddDroneCharge(DroneCharge d);
        void UpdateDrone(Drone d);
        void UpdateStation(Station s);
        void UpdateCustomer(Customer c, string phone);
        void UpdateParcel(Parcel p);
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
        //IEnumerable<DroneCharge> GetDroneCharge();
        double[] DroneElectricityUse();
    }
}
