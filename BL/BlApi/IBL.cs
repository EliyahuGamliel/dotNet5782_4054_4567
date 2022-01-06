using System;
using System.Collections.Generic;
using BO;
using DO;
using DalApi;

namespace BlApi
{
    /// <summary>
    /// Defining the "IBL" interface
    /// </summary>
    public interface IBL
    {
        #region Addition
        /// <summary>
        /// If everything is fine, add a station to the list of stations, else throw exception
        /// </summary>
        /// <param name="s">Object of station to add</param>
        /// <returns>Notice if the addition was successful</returns>
        string AddStation(BO.Station s);

        /// <summary>
        /// If everything is fine, add a drone to the list of drones, else throw exception
        /// </summary>
        /// <param name="d">Object of drone to add</param>
        /// <param name="idStation">The ID number of the station where the drone will be located</param>
        /// <returns>Notice if the addition was successful</returns>
        string AddDrone(BO.Drone d, int idStation);

        /// <summary>
        /// If everything is fine, add a parcel to the list of parcels, else throw exception
        /// </summary>
        /// <param name="p">Object of parcel to add</param>
        /// <param name="SenderId">The ID of the sender of the parcel</param>
        /// <param name="TargetId">The ID of the target of the parcel</param>
        /// <returns>Notice if the addition was successful</returns>
        string AddParcel(BO.Parcel p, int SenderId, int TargetId);

        /// <summary>
        /// If everything is fine, add a customer to the list of customers, else throw exception
        /// </summary>
        /// <param name="c">Object of customer to add</param>
        /// <returns>Notice if the addition was successful</returns>
        string AddCustomer(BO.Customer c);
        #endregion

        #region Update
        /// If all is fine, update the drone in a list of drones, else throw exception
        /// </summary>
        /// <param name="id">The ID of the drone for updating</param>
        /// <param name="model">The new model for the drone update</param>
        /// <returns>Notice if the addition was successful</returns>
        string UpdateDrone(int id, string model, double? battry = null/*, Location? loc = null*/);

        /// <summary>
        /// If all is fine, update the customer in a list of customers, else throw exception
        /// </summary>
        /// <param name="id">The ID of the customer for updating</param>
        /// <param name="nameCustomer">If requested - the new name for the customer update</param>
        /// <param name="phoneCustomer">If requested - the new phone for the customer update</param>
        /// <returns>Notice if the addition was successful</returns>
        string UpdateCustomer(int id, string nameCustomer, string phoneCustomer);

        /// <summary> updates the station
        /// </summary>
        /// <param name="id">The ID of the station for updating</param>
        /// <param name="name">If requested - the new name for the station update</param>
        /// <param name="chargeSlots">If requested - the number of charge slots for the station update</param>
        /// <returns>Notice if the addition was successful</returns>
        string UpdateStation(int id, string name, int? chargeSlots);

        /// <summary>
        /// /// If all is fine, the drone assign to a parcel, else throw exception
        /// </summary>
        /// <param name="DroneId">ID of the drone to assign a parcel</param>
        /// <returns>Notice if the addition was successful</returns>

        string UpdateParcel(int id, BO.Priorities priorty);
        #endregion

        #region DroneAction
        string AssignDroneParcel(int DroneId);

        /// <summary>
        /// If all is fine, the drone pick up the parcel, else throw exception
        /// </summary>
        /// <param name="id">ID of the drone to pickup a parcel</param>
        /// <returns>Notice if the addition was successful</returns>    
        string PickUpDroneParcel(int id);

        /// <summary>
        /// If all is fine, the drone deliver the parcel, else throw exception
        /// </summary>
        /// <param name="id">ID of the drone to deliver a parcel</param>
        /// <returns>Notice if the addition was successful</returns>  
        string DeliverParcelCustomer(int id);

        /// <summary>
        /// If all is fine, the drone sent to the close station to charge, else throw exception
        /// </summary>
        /// <param name="idDrone">ID of the drone sent for charging</param>
        /// <returns>Notice if the addition was successful</returns>
        string SendDrone(int idDrone);

        /// <summary>
        /// If all is fine, the drone relese from the station and from the charge, else throw exception
        /// </summary>
        /// <param name="idDrone">ID of the drone relese from charging</param>
        /// <returns>Notice if the addition was successful</returns>
        string ReleasDrone(int id);
        #endregion

        #region Getting
        /// <summary>
        /// If all is fine, return a parcel object by id, else throw exception
        /// </summary>
        /// <param name="Id">The id of the requested parcel</param>
        /// <returns>The object of the requested parcel</returns>
        BO.Parcel GetParcelById(int Id);

        /// <summary>
        /// If all is fine, return a drone object by id, else throw exception
        /// </summary>
        /// <param name="Id">The id of the requested drone</param>
        /// <returns>The object of the requested drone</returns>
        BO.Drone GetDroneById(int Id);

        /// <summary>
        /// If all is fine, return a customer object by id
        /// </summary>
        /// <param name="Id">The id of the requested customer</param>
        /// <returns>The object of the requested customer</returns>
        BO.Customer GetCustomerById(int Id);

        /// <summary>
        /// If all is fine, return a station object by id, else throw exception
        /// </summary>
        /// <param name="Id">The id of the requested station</param>
        /// <returns>The object of the requested station</returns>
        BO.Station GetStationById(int Id);

        /// <summary>
        /// Returns the list of stations
        /// </summary>
        /// <returns>Returns the list of stations</returns>
        IEnumerable<StationList> GetStations();

        /// <summary>
        /// Returns the list of drones
        /// </summary>
        /// <returns>Returns the list of drones</returns>
        IEnumerable<DroneList> GetDrones();

        /// <summary>
        /// Returns the list of customers
        /// </summary>
        /// <returns>Returns the list of customers</returns>
        IEnumerable<CustomerList> GetCustomers();

        /// <summary>
        /// Returns the list of parcels
        /// </summary>
        /// <returns>Returns the list of parcels</returns>
        IEnumerable<ParcelList> GetParcels();

        /// <summary>
        /// Returns a list of all unassigned parcels
        /// </summary>
        /// <returns>Returns a list of all unassigned parcels</returns>
        IEnumerable<ParcelList> GetParcelDrone();

        /// <summary>
        /// Returns a list of all stations that have available chargeSlots
        /// </summary>
        /// <returns>Returns a list of all stations that have available chargeSlots</returns>
        IEnumerable<StationList> GetStationCharge();

        /// <summary>
        /// Gets parameters (status and weight) for filtering and returns the drones that equal to the pararmeters
        /// </summary>
        /// <param name="weight">The weight value of the required drones</param>
        /// <param name="status">The status value of the required drones</param>
        /// <returns>Return list of drones by filter</returns>
        IEnumerable<DroneList> GetDroneByFilter(object weight, object status);

        IEnumerable<ParcelList> GetParcelByFilter(object weight, object status, object priorty, object fromate, object toDate);
        #endregion

        #region Delete
        string DeleteParcel(int id);

        string DeleteDrone(BO.DroneList drone);

        string DeleteCustomer(int customerID);

        string DeleteStation(int id);
        #endregion

        #region Simulator
        void PlaySimulator(int Id, Action updateDrone, Func<bool> stop);
        #endregion
    }
}
