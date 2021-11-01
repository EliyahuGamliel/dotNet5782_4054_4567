using System;
using IBL.BO;

namespace IBL
{
    public partial class BL : IBL
    {
        public void AddStation(Station s){
            IDAL.DO.Station st = new IDAL.DO.Station();
            st.Id = s.Id;
            st.Name = s.Name;
            st.Longitude = s.Location.Longitude;
            st.Lattitude = s.Location.Lattitude;
            st.ChargeSlots = s.ChargeSlots;
            data.AddStation(st);
        }
        IEnumerable<Station> PrintListStation(){

        }
    }
}