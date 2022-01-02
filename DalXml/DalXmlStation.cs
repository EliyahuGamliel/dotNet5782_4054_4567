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
            throw new NotImplementedException();
        }

        public void UpdateStation(Station s) {
            throw new NotImplementedException();
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
