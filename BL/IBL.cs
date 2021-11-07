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

        IEnumerable<IDAL.DO.Station> GetStations();
        IEnumerable<IDAL.DO.Drone> GetDrones();
        IEnumerable<IDAL.DO.Customer> GetCustomers();
        IEnumerable<IDAL.DO.Parcel> GetParcels();
        IEnumerable<IDAL.DO.Parcel> GetParcelDrone();
        IEnumerable<IDAL.DO.Station> GetStationCharge();
        double[] DroneElectricityUse();
        //IEnumerable<Station> CastingStation(IEnumerable<IDAL.DO.Station> st);
        //void Drone_Can_Send_To_Charge(IEnumerable<IDAL.DO.Station> list, Drone d);
    }
}
