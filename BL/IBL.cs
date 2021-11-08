using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public interface IBL
    {
        string AddStation(Station s);
        string AddDrone(DroneList d, int idStation);
        string AddParcel(Parcel p, int SenderId, int TargetId);
        string AddCustomer(Customer c);
        string UpdateDrone(int id, string model);
        string UpdateCustomer(int id, string nameCustomer, string phoneCustomer);
        string UpdateStation(int id, int name, int chargeSlots);
        void AssignDroneParcel(int DroneId);
        void PickUpDroneParcel(int id);
        void DeliverParcelCustomer(int id);
        void SendDrone(int idDrone);
        void ReleasDrone(int id, double time); 
        string GetParcelById(int Id);
        string GetDroneById(int Id);
        string GetCustomerById(int Id);
        string GetStationById(int Id);
        IEnumerable<StationList> GetStations();
        IEnumerable<DroneList> GetDrones();
        IEnumerable<CustomerList> GetCustomers();
        IEnumerable<ParcelList> GetParcels();
        IEnumerable<IDAL.DO.Parcel> GetParcelDrone();
        IEnumerable<IDAL.DO.Station> GetStationCharge();
        double[] DroneElectricityUse();
    }
}
