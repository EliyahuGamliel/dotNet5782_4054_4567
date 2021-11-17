using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL : IBL
    {
        /// <summary>
        /// If everything is fine, add a station to the list of stations, else throw exception
        /// </summary>
        /// <param name="s">Object of station to add</param>
        /// <returns>Notice if the addition was successful</returns>
        public string AddStation(Station s) {
            try {
                IDAL.DO.Station st = new IDAL.DO.Station();
                st.Id = s.Id;
                st.Name = s.Name;
                st.Longitude = s.Location.Longitude;
                st.Lattitude = s.Location.Lattitude;
                st.ChargeSlots = s.ChargeSlots;
                data.AddStation(st);
                return "The addition was successful\n";
            }
            catch (IDAL.DO.IdExistException) {
                throw new IdExistException(s.Id);
            }     
        }
        
        /// <summary>
        /// If all is fine, update the station in a list of stations, else throw exception
        /// </summary>
        /// <param name="id">The ID of the station for updating</param>
        /// <param name="name">If requested - the new name for the station update</param>
        /// <param name="chargeSlots">If requested - the number of charge slots for the station update</param>
        /// <returns>Notice if the addition was successful</returns>
        public string UpdateStation(int id, int name, int chargeSlots) {
            try {
                IDAL.DO.Station s = data.GetStationById(id);
                if (name != -1)
                    s.Name = name;
                if (chargeSlots != -1) {
                    int amountavalible = chargeSlots;
                    foreach (var item in data.GetDroneCharge())
                        if (item.StationId == s.Id)
                            amountavalible -= 1;
                    s.ChargeSlots = amountavalible;
                }
                data.UpdateStation(s);
                return "The update was successful\n";
            }
            catch (IDAL.DO.IdNotExistException) {
                throw new IdNotExistException(id);
            }
        }

        /// <summary>
        /// If all is fine, return a station object by id, else throw exception
        /// </summary>
        /// <param name="Id">The id of the requested station</param>
        /// <returns>The object of the requested station</returns>
        public Station GetStationById(int Id) {
            try {
                IDAL.DO.Station s = data.GetStationById(Id);
                Station st = new Station();
                st.Id = s.Id;
                st.Name = s.Name;
                st.ChargeSlots = s.ChargeSlots;
                st.Location.Longitude = s.Longitude;
                st.Location.Lattitude = s.Lattitude;
                foreach (var item in dronesList) {
                    //if drone is in charge in this station
                    if (item.CLocation.Lattitude == st.Location.Lattitude && item.CLocation.Longitude == st.Location.Longitude && item.Status == DroneStatuses.Maintenance) {
                        DroneCharge dc = new DroneCharge();
                        dc.Id = item.Id;
                        dc.Battery = item.Battery;
                        st.DCharge.Add(dc);
                    }
                }
                return st;
            }
            catch (IDAL.DO.IdNotExistException) {
                throw new IdNotExistException(Id);
            }
        }
        
        /// <summary>
        /// Returns the list of stations
        /// </summary>
        /// <returns>Returns the list of stations</returns>
        public IEnumerable<StationList> GetStations(){
            IEnumerable<IDAL.DO.Station> listcustomers = data.GetStations();
            List<StationList> station = new List<StationList>();
            foreach (var item in listcustomers) {
                StationList sl = new StationList();
                sl.Id = item.Id;
                sl.Name = item.Name;
                sl.ChargeSlots = item.ChargeSlots;
                sl.ChargeSlotsCatched = 0;
                foreach (var item2 in data.GetDroneCharge())
                    if (item2.StationId == sl.Id)
                        sl.ChargeSlotsCatched += 1;
                station.Add(sl);
            }
            return station;
        }

        /// <summary>
        /// Returns a list of all stations that have available chargeSlots
        /// </summary>
        /// <returns>Returns a list of all stations that have available chargeSlots</returns>
        public IEnumerable<StationList> GetStationCharge(){
            IEnumerable<IDAL.DO.Station> listcustomersC = data.GetStationCharge();
            IEnumerable<IDAL.DO.DroneCharge> listdronecharge = data.GetDroneCharge();
            List<StationList> station = new List<StationList>();
            foreach (var item in listcustomersC) {
                StationList sl = new StationList();
                sl.Id = item.Id;
                sl.Name = item.Name;
                sl.ChargeSlots = item.ChargeSlots;
                sl.ChargeSlotsCatched = 0;
                //How much charging places are occupied
                foreach (var item2 in listdronecharge)
                    if (item.Id == item2.StationId)
                        sl.ChargeSlotsCatched += 1;
                station.Add(sl);
            }
            return station;
        }
    }
}