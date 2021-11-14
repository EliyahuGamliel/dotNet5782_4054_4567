using System;
using IDAL.DO;
using System.Collections.Generic;

namespace DalObject
{
    /// <summary>
    /// Class partial DalObject - All functions running on the list of stations
    /// </summary>
    public partial class DalObject : IDAL.IDal
    {
        /// <summary>
        /// If everything is fine, add a station to the list of stations
        /// </summary>
        /// <param name="s">Object of station to add</param>
        public void AddStation(Station s) {
            CheckExistId(DataSource.stations, s.Id);
            DataSource.stations.Add(s);
        }

        /// <summary>
        /// If all is fine, update the station in a list of stations
        /// </summary>
        /// <param name="s">Object of station to update</param>
        public void UpdateStation(Station s) {
            CheckNotExistId(DataSource.stations, s.Id);
            Station st = DataSource.stations.Find(sta => sta.Id == s.Id);
            int index = DataSource.stations.IndexOf(st);
            DataSource.stations[index] = s;
        }

        /// <summary>
        /// If all is fine, return a station object by id
        /// </summary>
        /// <param name="Id">The id of the requested station</param>
        /// <returns>The object of the requested station</returns>
        public Station GetStationById(int Id) {
            CheckNotExistId(DataSource.stations, Id);
            Station s = DataSource.stations.Find(st => Id == st.Id);
            return s;
        }

        /// <summary>
        /// Returns the list of stations
        /// </summary>
        /// <returns>Returns the list of stations</returns>
        public IEnumerable<Station> GetStations() {
            return DataSource.stations;
        }
        
        /// <summary>
        /// Returns a list of all stations that have available chargeSlots
        /// </summary>
        /// <returns>Returns a list of all stations that have available chargeSlots</returns>
        public IEnumerable<Station> GetStationCharge() {
            return DataSource.stations.FindAll(st => 0 != st.ChargeSlots);
        }
    }
}