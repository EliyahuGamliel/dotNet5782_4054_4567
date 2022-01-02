using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;

namespace Dal
{
    partial class DalXml : IDal
    {

        public void AddStation(Station s) {
            List<Station> l = Read<Station>();
            ////////////////////////////////////////check for same id error
            l.Add(s);
            Write<Station>(l);
        }

        public void UpdateStation(Station s) {
            List<Station> l = Read<Station>();
            l[Update<Station>(l, s)] = s;
            Write<Station>(l);
        }

        public void DeleteStation(Station s) {
            List<Station> l = Read<Station>();
            l[Update<Station>(l, s)] = s;
            Write<Station>(l);
        }

        public Station GetStationById(int Id) {
            List<Station> l = Read<Station>();
            return l.Find(l => l.Id == Id);
        }

        public IEnumerable<Station> GetStationByFilter(Predicate<Station> stationList) {
            List<Station> l = Read<Station>();
            return l.FindAll(stationList);
        }
    }
}
