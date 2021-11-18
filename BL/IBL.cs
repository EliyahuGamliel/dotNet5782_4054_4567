using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    /// <summary>
    /// Defining the "IBL" interface
    /// </summary>
    public interface IBL
    {
        string AddStation(Station s);
        string AddDrone(DroneList d, int idStation);
        string AddParcel(Parcel p, int SenderId, int TargetId);
        string AddCustomer(Customer c);
        string UpdateDrone(int id, string model);
        string UpdateCustomer(int id, string nameCustomer, string phoneCustomer);
        string UpdateStation(int id, string name, int chargeSlots);
        string AssignDroneParcel(int DroneId);
        string PickUpDroneParcel(int id);
        string DeliverParcelCustomer(int id);
        string SendDrone(int idDrone);
        string ReleasDrone(int id, double time); 
        Parcel GetParcelById(int Id);
        Drone GetDroneById(int Id);
        Customer GetCustomerById(int Id);
        Station GetStationById(int Id);
        IEnumerable<StationList> GetStations();
        IEnumerable<DroneList> GetDrones();
        IEnumerable<CustomerList> GetCustomers();
        IEnumerable<ParcelList> GetParcels();
        IEnumerable<ParcelList> GetParcelDrone();
        IEnumerable<StationList> GetStationCharge();
    }
}
