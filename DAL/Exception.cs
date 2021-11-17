using System;
using System.Collections.Generic;

namespace IDAL.DO
{

    /// <summary>
    /// If the id already exist - throw Exception
    /// </summary>
    [Serializable]
    public class IdExistException : Exception
    {
        public int id { get; private set; }
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
        public int id { get; private set; }
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
        public string phone { get; private set; }
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