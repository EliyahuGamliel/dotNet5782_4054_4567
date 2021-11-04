using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL : IBL
    {
        public string AddStation(Station s){
            IDAL.DO.Station st = new IDAL.DO.Station();
            st.Id = s.Id;
            st.Name = s.Name;
            st.Longitude = s.Location.Longitude;
            st.Lattitude = s.Location.Lattitude;
            st.ChargeSlots = s.ChargeSlots;
            return data.AddStation(st);
        }
        
        public void UpdateStation(int id, int name, int chargeSlots) {
    
        }

        public string GetStationById(int Id) {
            return data.GetStationById(Id);
        }
        
        public IEnumerable<Station> GetStations(){
            return (List<Station>)(data.GetStations());
        }

        public IEnumerable<Station> GetStationCharge(){
            return CastingStation(data.GetStationCharge());
        }

        public IEnumerable<Station> CastingStation(IEnumerable<IDAL.DO.Station> st) {
            List<Station> station = new List<Station>();
            foreach (var item in st)
            {
                Station s = new Station();
                s.ChargeSlots = item.ChargeSlots;
                s.Id = item.Id;
                s.Name = item.Name;
                s.Location.Lattitude = item.Lattitude;
                s.Location.Longitude = item.Longitude;
            }
            return station;
        }
    }
}