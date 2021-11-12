using System;
using IDAL.DO;
using System.Collections.Generic;

namespace DalObject
{
    public partial class DalObject : IDAL.IDal
    {
        
        public void AddStation(Station s) {
            CheckExistId(DataSource.stations, s.Id);
            DataSource.stations.Add(s);
        }

        public void UpdateStation(Station s)
        {
            CheckNotExistId(DataSource.stations, s.Id);
            Station st = DataSource.stations.Find(sta => sta.Id == s.Id);
            int index = DataSource.stations.IndexOf(st);
            DataSource.stations[index] = s;
        }

        public Station GetStationById(int Id) {
            CheckNotExistId(DataSource.stations, Id);
            Station s = DataSource.stations.Find(st => Id == st.Id);
            return s;
        }

        public IEnumerable<Station> GetStations() {
            return DataSource.stations;
        }
        
        public IEnumerable<Station> GetStationCharge() {
            return DataSource.stations.FindAll(st => 0 != st.ChargeSlots);
        }
    }
}