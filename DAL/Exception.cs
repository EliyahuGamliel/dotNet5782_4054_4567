using System;
using System.Collections.Generic;

namespace IDAL
{
    namespace DO
    {
        [Serializable]
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
    }
}