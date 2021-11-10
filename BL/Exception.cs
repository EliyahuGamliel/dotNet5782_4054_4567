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

        [Serializable]
        public class DroneCannotSend : Exception
        {
            public override string ToString()
            {
                return "DroneCannotSend: The Drone cann't send to charge\n";
            }
        }
    }
}