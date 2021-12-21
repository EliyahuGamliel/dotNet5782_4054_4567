using System;
using System.Collections.Generic;
using DO;
using DalApi;

namespace Dal
{
    /// <summary>
    /// Class partial DalObject - All functions running on the list of stations
    /// </summary>
    public partial class DalObject : IDal
    {
        /// <summary>
        /// If everything is fine, add a station to the list of stations
        /// </summary>
        /// <param name="s">Object of station to add</param>
        public void AddStation(Station s) {
            CheckExistId(DataSource.Stations, s.Id);
            DataSource.Stations.Add(s);
        }

        /// <summary>
        /// If all is fine, update the station in a list of stations
        /// </summary>
        /// <param name="s">Object of station to update</param>
        public void UpdateStation(Station s) {
            CheckNotExistId(DataSource.Stations, s.Id);
            Station st = DataSource.Stations.Find(sta => sta.Id == s.Id);
            int index = DataSource.Stations.IndexOf(st);
            DataSource.Stations[index] = s;
        }

        /// <summary>
        /// If all is fine, return a station object by id
        /// </summary>
        /// <param name="Id">The id of the requested station</param>
        /// <returns>The object of the requested station</returns>
        public Station GetStationById(int Id) {
            CheckNotExistId(DataSource.Stations, Id);
            Station s = DataSource.Stations.Find(st => Id == st.Id);
            return s;
        }

        /// <summary> 
        /// Returns all the stations that fit the filter
        /// </summary>
        /// <param name="stationList">The paradicte to filter</param>
        /// <returns>The Ienumerable to the stations</returns>
        public IEnumerable<Station> GetStationByFilter(Predicate<Station> stationList) {
            return DataSource.Stations.FindAll(stationList);
        }

        public void DeleteStation(DO.Station s) {
            int index = DataSource.Stations.FindIndex(st => st.Id == s.Id);
            DataSource.Stations[index] = s;
        }
    }
}