using System;
using DO;
using DalApi;

namespace BO
{
    /// <summary>
    /// Defining the "DroneList" class
    /// </summary>
    public class DroneList
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public double Battery { get; set; }
        public DroneStatuses Status { get; set; }
        public Location CLocation { get; set; }
        public int ParcelId { get; set; }

        /// <summary><returns>
        /// The function returns a string to print on all entity data
        /// </returns></summary>
        public override string ToString() {
            string str = $"Id: {Id}\nBattery: {Math.Round((decimal)Battery, 0)}%\nModel: {Model}\nMaxWeight: {MaxWeight}\nStatus: {Status}\nParcelId: ";
            if (ParcelId == 0)
                str += "not exist";
            else
                str += ParcelId;
            str += $"\nCurrent Location: {CLocation}";
            return str;
        }
    }
}