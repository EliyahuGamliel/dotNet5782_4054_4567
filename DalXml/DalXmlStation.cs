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

        public void DeleteStation(Station station) {
            throw new NotImplementedException();
        }

        public Station GetStationById(int Id) {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetStationByFilter(Predicate<Station> stationList) {
            throw new NotImplementedException();
        }
    }
}
