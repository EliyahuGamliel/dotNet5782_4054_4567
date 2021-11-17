using System;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// Defining the "Drone" class
        /// </summary>
        public class Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public double Battery { get; set; }
            public DroneStatuses Status{ get; set; }
            public ParcelTransfer PTransfer { get; set; }
            public Location CLocation { get; set; }
            
            /// <summary>
            /// Ctor of Drone
            /// </summary>
            public Drone() { this.CLocation = new Location(); this.PTransfer = new ParcelTransfer(); }

            /// <summary><returns>
            /// The function returns a string to print on all entity data
            /// </returns></summary>
            public override string ToString() 
            { return $"Id: {Id}\n    Battery: {Math.Round(Battery, 3)}%\n    Model: {Model}\n    Status: {Status}\n    MaxWeight: {MaxWeight}\n    Location: {CLocation}    Parcel In Transfer: {PTransfer}"; }
        }
    }
}