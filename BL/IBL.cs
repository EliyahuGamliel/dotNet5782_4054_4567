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
        string SendDrone(int idDrone);
        string ReleasDrone(int id, double time); 
        Parcel GetParcelById(int Id);
        Drone GetDroneById(int Id);
        Customer GetCustomerById(int Id);
        Station GetStationById(int Id);
        int ReturnStatus(IDAL.DO.Parcel p);
        double DistanceTo(Location l1, Location l2);
        IEnumerable<StationList> GetStations();
        IEnumerable<DroneList> GetDrones();
        IEnumerable<CustomerList> GetCustomers();
        IEnumerable<ParcelList> GetParcels();
        IEnumerable<ParcelList> GetParcelDrone();
        IEnumerable<StationList> GetStationCharge();
        double[] DroneElectricityUse();
    }
}
