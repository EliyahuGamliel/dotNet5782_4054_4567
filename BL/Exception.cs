using System;
using System.Collections.Generic;


namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// If the id already exist - throw Exception
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

        /// <summary>
        /// If the id doesn't exist - throw Exception
        /// </summary>
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

        /// <summary>
        /// If the phone already exist - throw Exception
        /// </summary>
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

        /// <summary>
        /// If the SenderID like the TargetID - throw Exception
        /// </summary>
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

        /// <summary>
        /// If the Drone can not to send to charging - throw Exception
        /// </summary>
        [Serializable]
        public class DroneCannotSend : Exception
        {
            public override string ToString()
            {
                return "DroneCanNotSend: The Drone cann't send to charge\n";
            }
        }

        /// <summary>
        /// If the Drone can not to relese from charging - throw Exception
        /// </summary>
        [Serializable]
        public class DroneCannotRelese : Exception
        {
            public override string ToString()
            {
                return "DroneCanNotRelese: The Drone isn't in charge\n";
            }
        }

        /// <summary>
        /// If the Drone can not to assign to parcel - throw Exception
        /// </summary>
        [Serializable]
        public class DroneCannotAssigan : Exception
        {
            public override string ToString()
            {
                return "DroneCanNotAssigan: The Drone cann't assign to parcel\n";
            }
        }

        /// <summary>
        /// If the Drone can not to pickup the parcel - throw Exception
        /// </summary>
        [Serializable]
        public class DroneCannotPickUp : Exception
        {
            public override string ToString()
            {
                return "DroneCanNotPickUp: The Drone cann't pick up the parcel\n";
            }
        }

        /// <summary>
        /// If the Drone can not to deliver the parcel - throw Exception
        /// </summary>
        [Serializable]
        public class DroneCannotDeliver : Exception
        {
            public override string ToString()
            {
                return "DroneCanNotDeliver: The Drone cann't deliver the parcel\n";
            }
        }
    }
}