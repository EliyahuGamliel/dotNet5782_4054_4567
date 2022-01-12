using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BlApi;
using BO;

namespace BL
{
    public partial class BL : IBL
    {
        /// <summary>
        /// If everything is fine, add a station to the list of stations, else throw exception
        /// </summary>
        /// <param name="s">Object of station to add</param>
        /// <returns>Notice if the addition was successful</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string AddStation(BO.Station s) {
            try {
                lock (data) {

                    CheckValidId(s.Id.Value);
                    DO.Station st = new DO.Station();
                    st.Id = s.Id.Value;
                    st.Name = s.Name;
                    st.Longitude = s.Location.Longitude.Value;
                    st.Lattitude = s.Location.Lattitude.Value;
                    CheckLegelLocation(st.Longitude, st.Lattitude);
                    st.ChargeSlots = s.ChargeSlots.Value;
                    if (st.ChargeSlots < 0)
                        throw new ChargeSlotsNotLegal(st.ChargeSlots);
                    st.Active = true;
                    data.AddStation(st);
                }
                return "The addition was successful\n";
            }
            catch (DO.IdExistException exp) {
                throw new BO.IdExistException(exp.id);
            }
        }

        /// <summary>
        /// If all is fine, update the station in a list of stations, else throw exception
        /// </summary>
        /// <param name="id">The ID of the station for updating</param>
        /// <param name="name">If requested - the new name for the station update</param>
        /// <param name="chargeSlots">If requested - the number of charge slots for the station update</param>
        /// <returns>Notice if the addition was successful</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string UpdateStation(int id, string name, int? chargeSlots) {
            try {
                lock (data) {
                    DO.Station s = data.GetStationById(id);
                    if (name != "")
                        s.Name = name;
                    if (chargeSlots != null) {
                        if (chargeSlots < 0)
                            throw new ChargeSlotsNotLegal((int)chargeSlots);
                        s.ChargeSlots = (int)chargeSlots;
                    }
                    data.UpdateStation(s);
                }
                return "The update was successful\n";
            }
            catch (DO.IdNotExistException) {
                throw new BO.IdNotExistException(id);
            }
        }

        /// <summary>
        /// If all is fine, return a station object by id, else throw exception
        /// </summary>
        /// <param name="Id">The id of the requested station</param>
        /// <returns>The object of the requested station</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Station GetStationById(int Id) {
            try {
                lock (data) {
                    DO.Station chosens = data.GetStationById(Id);
                    BO.Station st = new BO.Station();
                    st.Id = chosens.Id;
                    st.Name = chosens.Name;
                    st.ChargeSlots = chosens.ChargeSlots;
                    st.Location = new Location();
                    st.Location.Longitude = chosens.Longitude;
                    st.Location.Lattitude = chosens.Lattitude;
                    st.DCharge = new List<BO.DroneCharge>();
                    foreach (var item in dronesList.FindAll(d => d.Status == DroneStatuses.Maintenance)) {
                        //if drone is in charge in this station
                        if (item.CLocation.Lattitude == st.Location.Lattitude && item.CLocation.Longitude == st.Location.Longitude) {
                            BO.DroneCharge dc = new BO.DroneCharge();
                            dc.Id = item.Id;
                            dc.Battery = item.Battery;
                            st.DCharge.Add(dc);
                        }
                    }
                    return st;
                }
            }
            catch (DO.IdNotExistException exp) {
                throw new BO.IdNotExistException(exp.id);
            }
        }

        /// <summary>
        /// Returns the list of stations
        /// </summary>
        /// <returns>Returns the list of stations</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationList> GetStations() {
            lock (data) {
                return ConvertToBL(data.GetStationByFilter(st => st.Active));
            }
        }

        /// <summary>
        /// Returns a list of all stations that have available chargeSlots
        /// </summary>
        /// <returns>Returns a list of all stations that have available chargeSlots</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationList> GetStationCharge() {
            lock (data) {
                return ConvertToBL(data.GetStationByFilter(st => st.ChargeSlots > 0 && st.Active));
            }
        }

        /// <summary>
        /// Convert the list from type of DAL(Station) to type of BL(StationList)
        /// </summary>
        /// <param name="listCustomers">The list we want to convert</param>
        /// <returns>The same list converted to BL(StationList)</returns>
        private IEnumerable<StationList> ConvertToBL(IEnumerable<DO.Station> listStation) {
            List<StationList> station = new List<StationList>();
            foreach (var item in listStation) {
                StationList sl = new StationList();
                sl.Id = item.Id;
                sl.Name = item.Name;
                sl.ChargeSlots = item.ChargeSlots;
                sl.ChargeSlotsCatched = ChargeSlotsCatched(sl.Id);
                station.Add(sl);
            }
            return station;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public string DeleteStation(int id) {
            lock (data) {
                DO.Station s = data.GetStationById(id);
                if (ChargeSlotsCatched(s.Id) != 0)
                    throw new CanntDeleteStation(s.Id);
                s.Active = false;
                data.DeleteStation(s);
            }
            return "The delete was successful\n";
        }
    }
}