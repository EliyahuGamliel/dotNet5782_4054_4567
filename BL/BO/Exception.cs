using System;

namespace BO
{
    /// <summary>
    /// If the id already exist - throw Exception
    /// </summary>
    public class IdExistException : Exception
    {
        public int id { get; private set; }

        /// <summary>
        /// If the the id exists it assigns the other id
        /// </summary>
        /// <param name="ID">the id</param>
        public IdExistException(int ID) {
            this.id = ID;
        }

        /// <summary>
        /// Returns a matching string for the exption
        /// </summary>
        /// <returns>a printable matching string</returns>
        public override string ToString() {
            return "IdExistException: The ID " + id + " already exist\n";
        }
    }

    public class IdNotValid : Exception
    {
        public int id { get; private set; }

        /// <summary>
        /// If the id is not valid then it assigns the id
        /// </summary>
        /// <param name="ID">the id</param>
        public IdNotValid(int ID) {
            this.id = ID;
        }

        /// <summary>
        /// Returns a matching string for the exption
        /// </summary>
        /// <returns>a printable matching string</returns>
        public override string ToString() {
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

        /// <summary>
        /// If the id doesnt exists it assigns the id
        /// </summary>
        /// <param name="ID">the id</param>
        public IdNotExistException(int ID) {
            this.id = ID;
        }

        /// <summary>
        /// Returns a matching string for the exption
        /// </summary>
        /// <returns>a printable matching string</returns>
        public override string ToString() {
            return "IdNotExistException: The ID " + id + " doesn't exist\n";
        }
    }

    /// <summary>
    /// If the choice doesn't valid - throw Exception
    /// </summary>
    [Serializable]
    public class ChoiceNotLegal : Exception
    {
        public double choice { get; private set; }

        /// <summary>
        /// If the choice is not legal then it assigns the choice
        /// </summary>
        /// <param name="choice">the choice</param>
        public ChoiceNotLegal(int choice) {
            this.choice = choice;
        }

        /// <summary>
        /// Returns a matching string for the exption
        /// </summary>
        /// <returns>a printable matching string</returns>
        public override string ToString() {
            return "ChoiceNotLegal: The choice it is not in the options (" + choice + ")\n";
        }
    }

    /// <summary>
    /// If the SenderID like the TargetID - throw Exception
    /// </summary>
    [Serializable]
    public class SameCustomerException : Exception
    {
        public int id { get; private set; }

        /// <summary>
        /// If the the customer already exists the it assigns the id
        /// </summary>
        /// <param name="id">the id</param>
        public SameCustomerException(int id) {
            this.id = id;
        }

        /// <summary>
        /// Returns a matching string for the exption
        /// </summary>
        /// <returns>a printable matching string</returns>
        public override string ToString() {
            return "SameCustomersException: The TargetId is like the SenderID (" + id + ")\n";
        }
    }

    /// <summary>
    /// If the Drone can not to send to charging - throw Exception
    /// </summary>
    [Serializable]
    public class DroneCannotSend : Exception
    {

        /// <summary>
        /// Returns a matching string for the exption
        /// </summary>
        /// <returns>a printable matching string</returns>
        public override string ToString() {
            return "DroneCanNotSend: The Drone cann't send to charge\n";
        }
    }

    /// <summary>
    /// If the Drone can not to relese from charging - throw Exception
    /// </summary>
    [Serializable]
    public class DroneCannotRelese : Exception
    {

        /// <summary>
        /// Returns a matching string for the exption
        /// </summary>
        /// <returns>a printable matching string</returns>
        public override string ToString() {
            return "DroneCanNotRelese: The Drone isn't in charge\n";
        }
    }

    /// <summary>
    /// If the time that the drone was in charging small than 0
    /// </summary>
    [Serializable]
    public class TimeNotLegal : Exception
    {
        public double time { get; private set; }

        /// <summary>
        /// If the time is not legal then it assigns the time
        /// </summary>
        /// <param name="time">the time</param>
        public TimeNotLegal(double time) {
            this.time = time;
        }

        /// <summary>
        /// Returns a matching string for the exption
        /// </summary>
        /// <returns>a printable matching string</returns>
        public override string ToString() {
            return "TimeNotLegal: The Drone cann't relese frome charging, because the time is not legal (" + time + ")\n";
        }
    }

    /// <summary>
    /// If the Drone can not to assign to parcel - throw Exception
    /// </summary>
    [Serializable]
    public class DroneCannotAssigan : Exception
    {

        /// <summary>
        /// Returns a matching string for the exption
        /// </summary>
        /// <returns>a printable matching string</returns>
        public override string ToString() {
            return "DroneCanNotAssigan: The Drone cann't assign to parcel\n";
        }
    }

    /// <summary>
    /// If the Drone can not to pickup the parcel - throw Exception
    /// </summary>
    [Serializable]
    public class DroneCannotPickUp : Exception
    {

        /// <summary>
        /// Returns a matching string for the exption
        /// </summary>
        /// <returns>a printable matching string</returns>
        public override string ToString() {
            return "DroneCanNotPickUp: The Drone cann't pick up the parcel\n";
        }
    }

    /// <summary>
    /// If the Drone can not to deliver the parcel - throw Exception
    /// </summary>
    [Serializable]
    public class DroneCannotDeliver : Exception
    {

        /// <summary>
        /// Returns a matching string for the exption
        /// </summary>
        /// <returns>a printable matching string</returns>
        public override string ToString() {
            return "DroneCanNotDeliver: The Drone cann't deliver the parcel\n";
        }
    }

    /// <summary>
    /// If the Drone can not to add because the station is full - throw Exception
    /// </summary>
    [Serializable]
    public class StationIsFull : Exception
    {

        /// <summary>
        /// Returns a matching string for the exption
        /// </summary>
        /// <returns>a printable matching string</returns>
        public override string ToString() {
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

        /// <summary>
        /// If the locatiom is not legal then it assigns the location
        /// </summary>
        /// <param name="longitude">the longitude</param>
        /// <param name="latitude">the latitude</param>
        public LocationNotLegal(double longitude, double latitude) {
            this.longitude = longitude;
            this.latitude = latitude;
        }

        /// <summary>
        /// Returns a matching string for the exption
        /// </summary>
        /// <returns>a printable matching string</returns>
        public override string ToString() {
            return "LocationNotLegal: The Location doesn't exist (" + longitude + ", " + latitude + ")\n";
        }
    }

    /// <summary>
    /// If the time that the drone was in charging small than 0
    /// </summary>
    [Serializable]
    public class ChargeSlotsNotLegal : Exception
    {
        public int chargeSlots { get; private set; }

        /// <summary>
        /// If the chargeslot is not legal the it assigns the charge slots
        /// </summary>
        /// <param name="chargeSlots"></param>
        public ChargeSlotsNotLegal(int chargeSlots) {
            this.chargeSlots = chargeSlots;
        }

        /// <summary>
        /// Returns a matching string for the exption
        /// </summary>
        /// <returns>a printable matching string</returns>
        public override string ToString() {
            return "ChargeSlotsNotLegal: The station cann't add/update, because the chargeSlots is not legal (" + chargeSlots + ")\n";
        }
    }

    [Serializable]
    public class CanntDeleteStation : Exception
    {
        public int Id { get; private set; }
        public CanntDeleteStation(int id) { this.Id = id; }
        public override string ToString() {
            return "CanntDeleteStation: The station (" + Id + ") cann't delete, because the chargeSlots is not empty\n";
        }
    }

    [Serializable]
    public class CanntDeleteCustomer : Exception
    {
        public int Id { get; private set; }
        public CanntDeleteCustomer(int id) { this.Id = id; }
        public override string ToString() {
            return "CanntDeleteCustomer: The customer (" + Id + ") cann't delete, because s/he have parcels in the way\n";
        }
    }

    [Serializable]
    public class CanntDeleteDrone : Exception
    {
        public int Id { get; private set; }
        public CanntDeleteDrone(int id) { this.Id = id; }
        public override string ToString() {
            return "CanntDeleteDrone: The drone (" + Id + ") cann't delete, because it isn't available\n";
        }
    }

    [Serializable]
    public class CanntDeleteParcel : Exception
    {
        public int Id { get; private set; }
        public CanntDeleteParcel(int id) { this.Id = id; }
        public override string ToString() {
            return "CanntDeleteParcel: The parcel (" + Id + ") cann't delete, because it is already associated\n";
        }
    }

    [Serializable]
    public class StationsNotExist : Exception
    {
        public override string ToString() {
            return "StationsNotExist: problem in the program - there is no station\n";
        }
    }
}