using System;
using System.Collections.Generic;


namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// Set up all the enums relevant to the program
        /// </summary>
        public class IdExistException : Exception
        {
            public int id { get; private set;}
            public IdExistException(int ID)
            {
                this.id = ID;
            }
            public override string ToString()
            {
                return "IdExistException: The ID " + id + " already exist\n";
            }
        }

        [Serializable]
        public class IdNotExistException : Exception
        {
            public int id { get; private set;}
            public IdNotExistException(int ID)
            {
                this.id = ID;
            }
            public override string ToString()
            {
                return "IdNotExistException: The ID " + id + " doesn't exist\n";
            }
        }

        [Serializable]
        public class PhoneExistException : Exception
        {
            public string phone { get; private set;}
            public PhoneExistException(string Phone)
            {
                this.phone = Phone;
            }
            public override string ToString()
            {
                return "PhoneExistException: The Phone " + phone + " exist\n";
            }
        }

        [Serializable]
        public class SameCustomerException : Exception
        {
            public int id { get; private set;}
            public SameCustomerException(int id)
            {
                this.id = id;
            }
            public override string ToString()
            {
                return "SameCustomersException: The TargetId is like the SenderID (" + id + ")\n";
            }
        }

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
                if (d.Status != DroneStatuses.Available)
                    throw new Exception("The Drone cann't send to charge");
            }
        }
    }
}