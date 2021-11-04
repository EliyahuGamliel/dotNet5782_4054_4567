using System;
using System.Collections.Generic;


namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// Set up all the enums relevant to the program
        /// </summary>
        public class MyException
        {
            public void Drone_Can_Send_To_Charge(IEnumerable<IDAL.DO.Station> list, Drone d)
            {
                double close_dis = -1;
                IDAL.DO.Station s;
                foreach (var item in list) {
                    if ((close_dis == -1 || item.DistanceTo(d.CLocation.Lattitude, d.CLocation.Longitude, item.Lattitude, item.Longitude) < close_dis)  && item.ChargeSlots > 0)
                        s = item;
                        close_dis = item.DistanceTo(d.CLocation.Lattitude, d.CLocation.Longitude, item.Lattitude, item.Longitude);
                }
                if (d.Status != DroneStatuses.Available ||)
                    throw new Exception("The Drone cann't send to charge");
            }
        }
        

    }
}