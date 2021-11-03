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
        IEnumerable<IDAL.DO.Station> GetStations();
        IEnumerable<IDAL.DO.Drone> PrintListDrone();
        IEnumerable<IDAL.DO.Customer> PrintListCustomer();
        IEnumerable<IDAL.DO.Parcel> PrintListParcel();
        IEnumerable<IDAL.DO.Parcel> PrintListParcelDrone();
        IEnumerable<IDAL.DO.Station> PrintListStationCharge();
        double[] DroneElectricityUse();
    }
}
