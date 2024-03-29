using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using BlApi;
using BO;

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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string AddDrone(BO.Drone d, int idStation) {
            try {
                lock (data) {
                    CheckValidId(d.Id.Value);
                    DO.Station s = data.GetStationById(idStation);
                    BO.DroneList dr = new BO.DroneList();
                    CheckLegelChoice((int)d.MaxWeight);
                    dr.Id = d.Id.Value;
                    dr.Model = d.Model;
                    dr.Active = true;
                    dr.CLocation = new Location();
                    dr.Battery = rand.Next(20, 41);
                    dr.Status = DroneStatuses.Maintenance;
                    dr.CLocation.Lattitude = s.Lattitude;
                    dr.CLocation.Longitude = s.Longitude;
                    dr.ParcelId = 0;

                    if (s.ChargeSlots == 0)
                        throw new StationIsFull();

                    DO.Drone drone = new DO.Drone();
                    drone.Id = dr.Id;
                    drone.Model = dr.Model;
                    drone.Active = true;
                    drone.MaxWeight = (DO.WeightCategories)((int)dr.MaxWeight);
                    data.AddDrone(drone);
                    dronesList.Add(dr);

                    int chargeSlots = ChargeSlotsCatched(idStation) + s.ChargeSlots;

                    DO.DroneCharge dc = new DO.DroneCharge();
                    dc.DroneId = d.Id.Value;
                    dc.StationId = idStation;
                    dc.Active = true;
                    dc.Start = DateTime.Now;
                    data.AddDroneCharge(dc);

                    UpdateStation(idStation, "", chargeSlots);
                }
                return "The addition was successful\n";
            }
            catch (DO.IdExistException) {
                throw new BO.IdExistException(d.Id.Value);
            }
        }

        /// <summary>
        /// If all is fine, update the drone in a list of drones, else throw exception
        /// </summary>
        /// <param name="id">The ID of the drone for updating</param>
        /// <param name="model">The new model for the drone update</param>
        /// <returns>Notice if the addition was successful</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string UpdateDrone(int id, string model, double? battry = null, Location loc = null) {
            lock (data) {
                try {
                    DO.Drone dr = data.GetDroneById(id);
                    dr.Model = model;
                    data.UpdateDrone(dr);
                    DroneList d = dronesList.Find(dro => dro.Id == id);
                    int index = dronesList.IndexOf(d);
                    d.Model = model;
                    if (battry != null) { d.Battery = battry.Value > 100 ? 100 : battry.Value; }
                    if (loc != null) {
                        d.CLocation.Lattitude = loc.Lattitude.Value;
                        d.CLocation.Longitude = loc.Longitude.Value;
                    }
                    dronesList[index] = d;
                    return "The update was successful\n";
                }
                catch (DO.IdNotExistException) {
                    throw new BO.IdNotExistException(id);
                }
            }
        }

        /// <summary>
        /// If all is fine, the drone sent to the close station to charge, else throw exception
        /// </summary>
        /// <param name="idDrone">ID of the drone sent for charging</param>
        /// <returns>Notice if the addition was successful</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string SendDrone(int idDrone) {
            CheckNotExistId(dronesList, idDrone);
            DroneList d = dronesList.Find(dr => idDrone == dr.Id);
            int index = dronesList.IndexOf(d);
            lock (data) {
                IEnumerable<DO.Station> stations = data.GetStationByFilter(s => s.Active && s.ChargeSlots > 0);
                double battery = CheckDroneCannotSend(stations, d);
                d.Status = DroneStatuses.Maintenance;
                BO.Station st = new BO.Station();
                st = ReturnCloseStation(stations, d.CLocation);
                d.CLocation = st.Location;
                d.Battery = d.Battery - battery;
                dronesList[index] = d;
                UpdateStation(st.Id.Value, "", st.ChargeSlots - 1);

                DO.DroneCharge dc = new DO.DroneCharge();
                dc.DroneId = d.Id;
                dc.StationId = st.Id.Value;
                dc.Active = true;
                dc.Start = DateTime.Now;
                data.AddDroneCharge(dc);
            }
            return "The update was successful\n";
        }

        /// <summary>
        /// If all is fine, the drone relese from the station and from the charge, else throw exception
        /// </summary>
        /// <param name="idDrone">ID of the drone relese from charging</param>
        /// <param name="time">The time the drone was in charge (in hours)</param>
        /// <returns>Notice if the addition was successful</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public String ReleasDrone(int idDrone) {
            CheckNotExistId(dronesList, idDrone);

            DroneList d = dronesList.Find(dr => idDrone == dr.Id);
            int index = dronesList.IndexOf(d);

            if (d.Status != DroneStatuses.Maintenance)
                throw new DroneCannotRelese();

            lock (data) {
                d.Status = DroneStatuses.Available;
                DO.DroneCharge dc = data.GetDroneChargeById(d.Id);
                dc.Active = false;
                TimeSpan time = DateTime.Now - dc.Start;
                d.Battery = d.Battery + (time.TotalSeconds * ChargingRate);
                if (d.Battery > 100)
                    d.Battery = 100;
                dronesList[index] = d;

                BO.Station st = GetStationById(dc.StationId);
                //int chargeSlots = ChargeSlotsCatched(st.Id.Value) + st.ChargeSlots.Value;

                data.DeleteDroneCharge(dc);
                UpdateStation(st.Id.Value, "", st.ChargeSlots.Value + 1);
            }
            return "The update was successful\n";

        }

        /// <summary>
        /// If all is fine, return a drone object by id, else throw exception
        /// </summary>
        /// <param name="Id">The id of the requested drone</param>
        /// <returns>The object of the requested drone</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Drone GetDroneById(int Id) {
            lock (data) {
                CheckNotExistId(dronesList, Id);
                BO.Drone dr = new BO.Drone();
                DroneList dl = dronesList.Find(d => d.Id == Id);
                dr.Id = dl.Id;
                dr.MaxWeight = dl.MaxWeight;
                dr.Model = dl.Model;
                dr.Status = dl.Status;
                dr.Battery = dl.Battery;
                dr.CLocation = dl.CLocation;

                if (dr.Status == DroneStatuses.Delivery) {
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

                    if (parcel.PickedUp == null)
                        pt.TransportDistance = DistanceTo(dr.CLocation, pt.CollectionLocation);
                    else
                        pt.TransportDistance = DistanceTo(dr.CLocation, pt.DestinationLocation);

                    dr.PTransfer = pt;
                }
                return dr;
            }
        }

        /// <summary>
        /// Returns the list of drones
        /// </summary>
        /// <returns>Returns the list of drones</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneList> GetDrones() {
            lock (data)
                return dronesList.FindAll(d => d.Active);
        }

        /// <summary>
        /// Gets parameters (status and weight) for filtering and returns the drones that equal to the pararmeters
        /// </summary>
        /// <param name="weight">The weight value of the required drones</param>
        /// <param name="status">The status value of the required drones</param>
        /// <returns>Return list of drones by filter</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneList> GetDroneByFilter(object weight, object status) {
            lock (data) {
                if (weight is null or "All" && status is null or "All")
                    return dronesList.FindAll(d => d.Active);
                else if (weight is null or "All")
                    return dronesList.FindAll(d => d.Status == (DroneStatuses)status && d.Active);
                else if (status is null or "All")
                    return dronesList.FindAll(d => d.MaxWeight == (BO.WeightCategories)weight && d.Active);
                return dronesList.FindAll(d => d.Status == (DroneStatuses)status && d.MaxWeight == (BO.WeightCategories)weight && d.Active);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public string DeleteDrone(BO.DroneList drone) {
            lock (data) {
                CheckDeleteDrone(drone);
                int index = dronesList.IndexOf(drone);
                drone.Active = false;
                dronesList[index] = drone;
                DO.Drone dr = new DO.Drone();
                dr.Active = false;
                dr.Id = drone.Id;
                dr.Model = drone.Model;
                dr.MaxWeight = (DO.WeightCategories)((int)drone.MaxWeight);
                data.DeleteDrone(dr);
            }
            return "The delete was successful\n";
        }
    }
}
