using System;
using IDAL.DO;
using System.Collections.Generic;

namespace DalObject
{
    public partial class DalObject : IDAL.IDal
    {
        MyException exp = new MyException();

        /// <summary>
        /// Add a station at the request of the user to the list
        /// </summary>
        /// <param name="Id">id of station</param>
        /// <param name="Name">name of station</param>
        /// <param name="Longitude">Longitude of station</param>
        /// <param name="Lattitude">Lattitude of station</param>
        /// <param name="ChargeSlots">Number of available charging stations</param>
        public string AddStation(Station s) {
            exp.Check_Add_ID<Station>(DataSource.stations, s.Id);
            DataSource.stations.Add(s);
            return "The addition was successful";
        }

        public string GetStationById(int Id) {
            exp.Check_Update_or_Get_By_ID<Station>(DataSource.stations, Id);
            Station s = DataSource.stations.Find(st => Id == st.Id);
            return s.ToString();
        }

        /// <summary>
        /// prints the stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Station> GetStations() {
            return DataSource.stations;
        }
        
        /// <summary>
        /// prints all the stations with avaliable charging slots
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Station> GetStationCharge() {
            return DataSource.stations.FindAll(st => 0 != st.ChargeSlots);
        }
    }
}