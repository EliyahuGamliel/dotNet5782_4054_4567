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
            List<Station> ls = Read<Station>();
            CheckExistId<Station>(ls, s.Id);
            ls.Add(s);
            Write<Station>(ls);
        }

        public void UpdateStation(Station s) {
            List<Station> ls = Read<Station>();
            CheckNotExistId<Station>(ls, s.Id);
            ls[Update<Station>(ls, s)] = s;
            Write<Station>(ls);
        }

        public void DeleteStation(Station s) {
            List<Station> ls = Read<Station>();
            CheckNotExistId<Station>(ls, s.Id);
            ls[Update<Station>(ls, s)] = s;
            Write<Station>(ls);
        }

        public Station GetStationById(int Id) {
            List<Station> ls = Read<Station>();
            CheckNotExistId<Station>(ls, Id);
            return ls.Find(ls => ls.Id == Id);
        }

        public IEnumerable<Station> GetStationByFilter(Predicate<Station> stationList) {
            List<Station> l = Read<Station>();
            return l.FindAll(stationList);
        }
    }
}
