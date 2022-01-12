using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DalApi;
using DO;

namespace Dal
{
    internal partial class DalXml : IDal
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(Station s) {
            List<Station> stations = Read<Station>();
            CheckExistId<Station>(stations, s.Id);
            stations.Add(s);
            Write<Station>(stations);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStation(Station s) {
            List<Station> stations = Read<Station>();
            CheckNotExistId<Station>(stations, s.Id);
            stations[Update<Station>(stations, s)] = s;
            Write<Station>(stations);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteStation(Station s) {
            List<Station> stations = Read<Station>();
            CheckNotExistId<Station>(stations, s.Id);
            stations[Update<Station>(stations, s)] = s;
            Write<Station>(stations);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station GetStationById(int Id) {
            List<Station> stations = Read<Station>();
            CheckNotExistId<Station>(stations, Id);
            return stations.Find(ls => ls.Id == Id);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetStationByFilter(Predicate<Station> stationList) {
            List<Station> stations = Read<Station>();
            return stations.FindAll(stationList);
        }
    }
}
