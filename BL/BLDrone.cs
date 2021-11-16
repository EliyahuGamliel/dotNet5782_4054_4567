using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
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
                IDAL.DO.Station s = data.GetStationById(idStation);
                d.Battery = rand.Next(20,41);
                d.Status = DroneStatuses.Maintenance;
                d.CLocation.Lattitude = s.Lattitude;
                d.CLocation.Longitude = s.Longitude;
                d.ParcelId = 0;
                
                IDAL.DO.Drone dr = new IDAL.DO.Drone();
                dr.Id = d.Id;
                dr.Model = d.Model;
                dr.MaxWeight = (IDAL.DO.WeightCategories)((int)d.MaxWeight);
                data.AddDrone(dr);
                IDAL.DO.DroneCharge dc = new IDAL.DO.DroneCharge();
                dc.DroneId = d.Id;
                dc.StationId = idStation;
                data.AddDroneCharge(dc);
                dronesList.Add(d);
                UpdateStation(idStation, -1, s.ChargeSlots);
                return "The addition was successful\n";
            }
            catch (IDAL.DO.IdExistException) {
                throw new IdExistException(d.Id);
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
                IDAL.DO.Drone dr = data.GetDroneById(id);
                dr.Model = model;
                data.UpdateDrone(dr);
                DroneList d = dronesList.Find(dro => dro.Id == id);
                int index = dronesList.IndexOf(d);
                d.Model = model;
                dronesList[index] = d;
                return "The update was successful\n";
            }
            catch (IDAL.DO.IdNotExistException) {
                throw new IdNotExistException(id);
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
            double battery = CheckDroneCannotSend(data.GetStations(), d);
            d.Status = DroneStatuses.Maintenance;
            IDAL.DO.Station st = new IDAL.DO.Station();
            st = ReturnCloseStation(data.GetStations(), d.CLocation);
            d.CLocation.Lattitude = st.Lattitude;
            d.CLocation.Longitude = st.Longitude;
            d.Battery = d.Battery - battery;
            dronesList[index] = d;
            UpdateStation(st.Id, -1, st.ChargeSlots - 1);
            IDAL.DO.DroneCharge dc = new IDAL.DO.DroneCharge();
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
            DroneList d = dronesList.Find(dr => idDrone == dr.Id);
            int index = dronesList.IndexOf(d);
            CheckDroneCannotRelese(d);
            d.Status = DroneStatuses.Available;
            d.Battery = d.Battery + time * ChargingRate;
            if (d.Battery > 100)
                d.Battery = 100;
            dronesList[index] = d;

            IDAL.DO.Station st = new IDAL.DO.Station();
            st = ReturnCloseStation(data.GetStations(), d.CLocation);

            IDAL.DO.DroneCharge dc = new IDAL.DO.DroneCharge();
            dc.DroneId = d.Id;
            dc.StationId = st.Id;
            data.DeleteDroneCharge(dc);

            UpdateStation(st.Id, -1, st.ChargeSlots + 1);
            return "The update was successful\n";
        }

        /// <summary>
        /// If all is fine, return a drone object by id, else throw exception
        /// </summary>
        /// <param name="Id">The id of the requested drone</param>
        /// <returns>The object of the requested drone</returns>
        public Drone GetDroneById(int Id) {
            CheckNotExistId(dronesList, Id);
            Drone dr = new Drone();
            DroneList dl = dronesList.Find(d => d.Id == Id);           
            dr.Id = dl.Id;
            dr.MaxWeight = dl.MaxWeight;
            dr.Model = dr.Model;
            dr.Status = dl.Status;
            dr.Battery = dl.Battery;
            dr.CLocation = dl.CLocation;

            ParcelTransfer pt = new ParcelTransfer();
            foreach (var itemParcels in data.GetParcels()) {
                //If the parcel associated with the drone
                if (itemParcels.Id == dl.ParcelId) {
                    pt.Id = itemParcels.Id;
                    pt.Priority = (Priorities)(int)itemParcels.Priority;
                    pt.Weight = (WeightCategories)(int)itemParcels.Weight;
                    pt.Status = false;
                    //If the parcel has already been collected
                    if (DateTime.Compare(itemParcels.PickedUp, itemParcels.Scheduled) > 0)
                        pt.Status = true;

                    //CustomerInParcel - The Target Customer of Parcel 
                    CustomerInParcel cp1 = new CustomerInParcel();
                    cp1.Id = itemParcels.TargetId;
                    IDAL.DO.Customer c_help = data.GetCustomerById(cp1.Id); 
                    cp1.Name = c_help.Name;
                    pt.Recipient = cp1;
                    pt.Destination_Location.Lattitude = c_help.Lattitude;
                    pt.Destination_Location.Longitude = c_help.Longitude;

                    //CustomerInParcel - The Sender Customer of Parcel 
                    CustomerInParcel cp2 = new CustomerInParcel();
                    cp2.Id = itemParcels.SenderId;
                    c_help = data.GetCustomerById(cp2.Id); 
                    cp2.Name = c_help.Name;
                    pt.Sender = cp2;
                    pt.Collection_Location.Lattitude = c_help.Lattitude;
                    pt.Collection_Location.Longitude = c_help.Longitude;

                    pt.Transport_Distance = DistanceTo(pt.Collection_Location, pt.Destination_Location);

                    dr.PTransfer = pt;
                }
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
    }
}
