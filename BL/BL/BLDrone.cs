using System;
using System.Linq;
using System.Collections.Generic;
using BO;
using BlApi;
using DO;
using DalApi;

namespace BL
{
    public partial class BL : IBL
    { 
        /// <summary>
        /// If everything is fine, add a drone to the list of drones, else throw exception
        /// </summary>
        /// <param name="d">Object of drone to add</param>
        /// <param name="idStation">The ID number of the station where the drone will be located</param>
        /// <returns>Notice if the addition was successful</returns>
        public string AddDrone(DroneList d, int idStation) {
            try {
                CheckValidId(d.Id);
                DO.Station s = data.GetStationById(idStation);
                CheckLegelChoice((int)d.MaxWeight);
                d.CLocation = new Location();
                d.Battery = rand.Next(20,41);
                d.Status = DroneStatuses.Maintenance;
                d.CLocation.Lattitude = s.Lattitude;
                d.CLocation.Longitude = s.Longitude;
                d.ParcelId = 0;

                if (s.ChargeSlots == 0)
                    throw new StationIsFull();

                DO.Drone dr = new DO.Drone();
                dr.Id = d.Id;
                dr.Model = d.Model;
                dr.MaxWeight = (DO.WeightCategories)((int)d.MaxWeight);
                data.AddDrone(dr);
                dronesList.Add(d);

                int chargeSlots = ChargeSlotsCatched(idStation) + s.ChargeSlots;

                DO.DroneCharge dc = new DO.DroneCharge();
                dc.DroneId = d.Id;
                dc.StationId = idStation;
                data.AddDroneCharge(dc);

                UpdateStation(idStation, "", chargeSlots);
                return "The addition was successful\n";
            }
            catch (DO.IdExistException) {
                throw new BO.IdExistException(d.Id);
            }
        }

        /// <summary>
        /// If all is fine, update the drone in a list of drones, else throw exception
        /// </summary>
        /// <param name="id">The ID of the drone for updating</param>
        /// <param name="model">The new model for the drone update</param>
        /// <returns>Notice if the addition was successful</returns>
        public string UpdateDrone(int id, string model) {
            try {
                DO.Drone dr = data.GetDroneById(id);
                dr.Model = model;
                data.UpdateDrone(dr);
                DroneList d = dronesList.Find(dro => dro.Id == id);
                int index = dronesList.IndexOf(d);
                d.Model = model;
                dronesList[index] = d;
                return "The update was successful\n";
            }
            catch (DO.IdNotExistException) {
                throw new BO.IdNotExistException(id);
            }
        }

        /// <summary>
        /// If all is fine, the drone sent to the close station to charge, else throw exception
        /// </summary>
        /// <param name="idDrone">ID of the drone sent for charging</param>
        /// <returns>Notice if the addition was successful</returns>
        public string SendDrone(int idDrone) {
            CheckNotExistId(dronesList, idDrone);

            DroneList d = dronesList.Find(dr => idDrone == dr.Id);
            int index = dronesList.IndexOf(d);
            double battery = CheckDroneCannotSend(data.GetStationByFilter(s => true).Where(s => s.ChargeSlots > 0), d);
            d.Status = DroneStatuses.Maintenance;
            BO.Station st = new BO.Station();
            st = ReturnCloseStation(data.GetStationByFilter(s => true), d.CLocation);
            d.CLocation = st.Location;
            d.Battery = d.Battery - battery;
            dronesList[index] = d;
            UpdateStation(st.Id, "", st.ChargeSlots - 1);

            DO.DroneCharge dc = new DO.DroneCharge();
            dc.DroneId = d.Id;
            dc.StationId = st.Id;
            data.AddDroneCharge(dc);
            return "The update was successful\n";
        }

        /// <summary>
        /// If all is fine, the drone relese from the station and from the charge, else throw exception
        /// </summary>
        /// <param name="idDrone">ID of the drone relese from charging</param>
        /// <param name="time">The time the drone was in charge (in hours)</param>
        /// <returns>Notice if the addition was successful</returns>
        public String ReleasDrone(int idDrone, double time){
            CheckNotExistId(dronesList, idDrone);
            if (time < 0)
                throw new TimeNotLegal(time);

            DroneList d = dronesList.Find(dr => idDrone == dr.Id);
            int index = dronesList.IndexOf(d);
           
            if (d.Status != DroneStatuses.Maintenance)
                throw new DroneCannotRelese();

            d.Status = DroneStatuses.Available;
            d.Battery = d.Battery + time * ChargingRate;
            if (d.Battery > 100)
                d.Battery = 100;
            dronesList[index] = d;

            BO.Station st = new BO.Station();
            st = ReturnCloseStation(data.GetStationByFilter(s => true), d.CLocation);
            int chargeSlots = ChargeSlotsCatched(st.Id) + st.ChargeSlots;

            DO.DroneCharge dc = new DO.DroneCharge();
            dc.DroneId = d.Id;
            dc.StationId = st.Id;
            data.DeleteDroneCharge(dc);

            UpdateStation(st.Id, "", chargeSlots);
            return "The update was successful\n";
        }

        /// <summary>
        /// If all is fine, return a drone object by id, else throw exception
        /// </summary>
        /// <param name="Id">The id of the requested drone</param>
        /// <returns>The object of the requested drone</returns>
        public BO.Drone GetDroneById(int Id) {
            CheckNotExistId(dronesList, Id);
            BO.Drone dr = new BO.Drone();
            DroneList dl = dronesList.Find(d => d.Id == Id);           
            dr.Id = dl.Id;
            dr.MaxWeight = dl.MaxWeight;
            dr.Model = dl.Model;
            dr.Status = dl.Status;
            dr.Battery = dl.Battery;
            dr.CLocation = dl.CLocation;

            if (dr.Status == DroneStatuses.Delivery)
            {
                ParcelTransfer pt = new ParcelTransfer();
                DO.Parcel parcel = data.GetParcelByFilter(p => p.Id == dl.ParcelId).First();
                //If the parcel associated with the drone
                pt.Id = parcel.Id;
                pt.Priority = (BO.Priorities)(int)parcel.Priority;
                pt.Weight = (BO.WeightCategories)(int)parcel.Weight;
                pt.Status = false;
                //If the parcel has already been collected
                if (parcel.PickedUp != null)
                    pt.Status = true;

                //CustomerInParcel - The Target Customer of Parcel 
                CustomerInParcel cp1 = new CustomerInParcel();
                cp1.Id = parcel.TargetId;
                DO.Customer customerhelp = data.GetCustomerById(cp1.Id);
                cp1.Name = customerhelp.Name;
                pt.Recipient = cp1;
                pt.DestinationLocation = new Location();
                pt.DestinationLocation.Lattitude = customerhelp.Lattitude;
                pt.DestinationLocation.Longitude = customerhelp.Longitude;

                //CustomerInParcel - The Sender Customer of Parcel 
                CustomerInParcel cp2 = new CustomerInParcel();
                cp2.Id = parcel.SenderId;
                customerhelp = data.GetCustomerById(cp2.Id);
                cp2.Name = customerhelp.Name;
                pt.Sender = cp2;
                pt.CollectionLocation = new Location();
                pt.CollectionLocation.Lattitude = customerhelp.Lattitude;
                pt.CollectionLocation.Longitude = customerhelp.Longitude;

                pt.TransportDistance = DistanceTo(pt.CollectionLocation, pt.DestinationLocation);

                dr.PTransfer = pt;
            }
            return dr;
        }
        
        /// <summary>
        /// Returns the list of drones
        /// </summary>
        /// <returns>Returns the list of drones</returns>
        public IEnumerable<DroneList> GetDrones(){
            return dronesList;
        }

        /// <summary>
        /// Gets parameters (status and weight) for filtering and returns the drones that equal to the pararmeters
        /// </summary>
        /// <param name="weight">The weight value of the required drones</param>
        /// <param name="status">The status value of the required drones</param>
        /// <returns>Return list of drones by filter</returns>
        public IEnumerable<DroneList> GetDroneByFilter(object weight, object status)
        {
            if (weight is null or "All" && status is null or "All")
                return dronesList;
            else if (weight is null or "All")
                return dronesList.FindAll(d => d.Status == (DroneStatuses)status);
            else if (status is null or "All")
                return dronesList.FindAll(d => d.MaxWeight == (BO.WeightCategories)weight);
            return dronesList.FindAll(d => d.Status == (DroneStatuses)status && d.MaxWeight == (BO.WeightCategories)weight);   
        }
    }
}
