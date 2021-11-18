using System;

namespace IBL.BO
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
        public override string ToString()
        { return $"Id: {Id}\nBattery: {Math.Round(Battery, 3)}%\nModel: {Model}\nMaxWeight: {MaxWeight}\nStatus: {Status}\nParcelId: {ParcelId}\nCurrent Location: {CLocation}"; }
    }
}