using System;
using System.Collections.Generic;


namespace IBL.BO
{
    /// <summary>
    /// If the id already exist - throw Exception
    /// </summary>
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

    public class IdNotValid : Exception
    {
        public int id { get; private set; }
        public IdNotValid(int ID)
        {
            this.id = ID;
        }
        public override string ToString()
        {
            return "IdNotValid: The ID must be a positive number (" + id + ")\n";
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
    /// 
    /// </summary>
    [Serializable]
    public class ChoiceNotLegal : Exception
    {
        public double choice { get; private set; }
        public ChoiceNotLegal(int choice)
        {
            this.choice = choice;
        }
        public override string ToString()
        {
            return "ChoiceNotLegal: The choice it is not in the options (" + choice + ")\n";
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

    /// <summary>
    /// If the SenderID like the TargetID - throw Exception
    /// </summary>
    [Serializable]
    public class SameCustomerException : Exception
    {
        public int id { get; private set; }
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
    /// if the time that the drone was in charging small than 0
    /// </summary>
    [Serializable]
    public class TimeNotLegal : Exception
    {
        public double time { get; private set; }
        public TimeNotLegal(double time)
        {
            this.time = time;
        }
        public override string ToString()
        {
            return "TimeNotLegal: The Drone cann't relese frome charging, because the time is not legal (" + time + ")\n";
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

    /// <summary>
    /// If the Drone can not to add because the station is full - throw Exception
    /// </summary>
    [Serializable]
    public class StationIsFull : Exception
    {
        public override string ToString()
        {
            return "StationIsFull: The Drone cann't send/add because the station is full\n";
        }
    }

    /// <summary>
    /// if the time that the drone was in charging small than 0
    /// </summary>
    [Serializable]
    public class LocationNotLegal : Exception
    {
        public double longitude { get; private set; }
        public double latitude { get; private set; }
        public LocationNotLegal(double longitude, double latitude)
        {
            this.longitude = longitude;
            this.latitude = latitude;
        }
        public override string ToString()
        {
            return "LocationNotLegal: The Location doesn't exist (" + longitude +  ", " + latitude + ")\n";
        }
    }

    /// <summary>
    /// if the time that the drone was in charging small than 0
    /// </summary>
    [Serializable]
    public class ChargeSlotsNotLegal : Exception
    {
        public int chargeSlots { get; private set; }
        public ChargeSlotsNotLegal(int chargeSlots)
        {
            this.chargeSlots = chargeSlots;
        }
        public override string ToString()
        {
            return "ChargeSlotsNotLegal: The station cann't add/update, because the chargeSlots is not legal (" + chargeSlots + ")\n";
        }
    }
}