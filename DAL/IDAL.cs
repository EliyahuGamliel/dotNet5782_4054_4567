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
        /// <summary>
        /// If everything is fine, add a station to the list of stations
        /// </summary>
        /// <param name="s">Object of station to add</param>
        void AddStation(Station s);

        /// <summary>
        /// If everything is fine, add a drone to the list of drones
        /// </summary>
        /// <param name="d">Object of drone to add</param>        
        void AddDrone(Drone d);

        /// <summary>
        /// If everything is fine, add a parcel to the list of parcels
        /// </summary>
        /// <param name="p">Object of parcel to add</param>
        int AddParcel(Parcel p);

        /// <summary>
        /// If everything is fine, add a customer to the list of customers
        /// </summary>
        /// <param name="c">Object of customer to add</param>
        void AddCustomer(Customer c);

        /// <summary>
        /// If everything is fine, add a droneCharge to the list of dronesCharge
        /// </summary>
        /// <param name="d">Object of droneCharge to add</param>
        void AddDroneCharge(DroneCharge d);

        /// <summary>
        /// If all is fine, update the drone in a list of drones
        /// </summary>
        /// <param name="d">Object of drone to update</param>
        void UpdateDrone(Drone d);

        /// <summary>
        /// If all is fine, update the station in a list of stations
        /// </summary>
        /// <param name="s">Object of station to update</param>
        void UpdateStation(Station s);

        /// <summary>
        /// If all is fine, update the customer in a list of customers
        /// </summary>
        /// <param name="c">Object of customer to update</param>
        /// <param name="phone">Object.phone of customer to update</param>
        void UpdateCustomer(Customer c, string phone);

        /// <summary>
        /// If all is fine, update the parcel in a list of parcels
        /// </summary>
        /// <param name="p">Object of parcel to update</param>
        void UpdateParcel(Parcel p);

        /// <summary>
        /// If everything is fine, delete a droneCharge from the list of dronesCharge
        /// </summary>
        /// <param name="d">Object of droneCharge to delete</param>
        void DeleteDroneCharge(DroneCharge d);

        /// <summary>
        /// If all is fine, return a customer object by id
        /// </summary>
        /// <param name="Id">The id of the requested customer</param>
        /// <returns>The object of the requested customer</returns>
        Customer GetCustomerById(int Id);

        /// <summary>
        /// If all is fine, return a drone object by id
        /// </summary>
        /// <param name="Id">The id of the requested drone</param>
        /// <returns>The object of the requested drone</returns>
        Drone GetDroneById(int Id);

        /// <summary>
        /// If all is fine, return a parcel object by id
        /// </summary>
        /// <param name="Id">The id of the requested parcel</param>
        /// <returns>The object of the requested parcel</returns>
        Parcel GetParcelById(int Id);

        /// <summary>
        /// If all is fine, return a station object by id
        /// </summary>
        /// <param name="Id">The id of the requested station</param>
        /// <returns>The object of the requested station</returns>
        Station GetStationById(int Id);

        IEnumerable<Station> GetStationByFilter(Predicate<Station> stationList);

        IEnumerable<Drone> GetDroneByFilter(Predicate<Drone> droneList);

        IEnumerable<Customer> GetCustomerByFilter(Predicate<Customer> cutomerList);

        
        IEnumerable<Parcel> GetParcelByFilter(Predicate<Parcel> parcelList);

        IEnumerable<DroneCharge> GetDroneChargeByFilter(Predicate<DroneCharge> droneChargeList);

        /// <summary>
        /// Returns an array with all fields of power consumption
        /// </summary>
        /// <returns>Returns an array with all fields of power consumption</returns>
        double[] DroneElectricityUse();
    }
}
