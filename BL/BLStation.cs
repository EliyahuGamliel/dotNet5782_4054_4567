using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL : IBL
    {
        public string AddStation(Station s) {
            try
            {
                IDAL.DO.Station st = new IDAL.DO.Station();
                st.Id = s.Id;
                st.Name = s.Name;
                st.Longitude = s.Location.Longitude;
                st.Lattitude = s.Location.Lattitude;
                st.ChargeSlots = s.ChargeSlots;
                data.AddStation(st);
                return "The addition was successful";
            }
            catch (IDAL.DO.IdExistException exp)
            {
                throw exp;
            }     
        }
        
        public string UpdateStation(int id, int name, int chargeSlots) {
            try
            {
                List<IDAL.DO.Station> list_s = data.GetStations();
                CheckNotExistId(list_s, id);
                IDAL.DO.Station s = list_s.Find(st => id == st.Id);
                int index = list_s.IndexOf(s);
                s.Name = name;
                int amount_avalible = chargeSlots;
                foreach (var item in dronesList)
                    if (item.CLocation.Longitude == s.Longitude && item.CLocation.Lattitude == s.Lattitude)
                        amount_avalible -= 1;
                s.ChargeSlots = amount_avalible;
                data.UpdateStation(s, index);
                return "The update was successful";
            }
            catch (IDAL.DO.IdNotExistException exp)
            {
                throw;
            }
            
        
        }

        public string GetStationById(int Id) {
            return data.GetStationById(Id);
        }
        
        public List<IDAL.DO.Station> GetStations(){
            return data.GetStations();
        }

        public IEnumerable<IDAL.DO.Station> GetStationCharge(){
            return data.GetStationCharge();
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