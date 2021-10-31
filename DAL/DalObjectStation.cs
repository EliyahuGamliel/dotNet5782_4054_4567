using System;
using IDAL.DO;
using System.Collections.Generic;

namespace DalObject
{
    public partial class DalObject : IDAL.IDAL
    {
        /// <summary>
        /// Add a station at the request of the user to the list
        /// </summary>
        /// <param name="Id">id of station</param>
        /// <param name="Name">name of station</param>
        /// <param name="Longitude">Longitude of station</param>
        /// <param name="Lattitude">Lattitude of station</param>
        /// <param name="ChargeSlots">Number of available charging stations</param>
        public void AddStation(int Id, int Name, double Longitude, double Lattitude, int ChargeSlots) {
            Station s = new Station();
            s.Id = Id;
            s.Name = Name;
            s.Longitude = Longitude;
            s.Lattitude = Lattitude;
            s.ChargeSlots = ChargeSlots;
            DataSource.stations.Add(s);
        }

        /// <summary>
        /// prints the stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Station> PrintListStation() {
            return DataSource.stations.ToArray();
        }
        
        /// <summary>
        /// prints all the stations with avaliable charging slots
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Station> PrintListStationCharge() {
            return DataSource.stations.FindAll(st => 0 != st.ChargeSlots);
        }
    }
}