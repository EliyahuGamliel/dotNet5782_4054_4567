using System;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// Defining the "DroneInParcel" class
        /// </summary>
        public class DroneInParcel
        {
            public int Id { get; set; }
            public double Battery { get; set; }
            public Location CLocation { get; set; }
            
            /// <summary>
            /// Ctor of DroneInParcel
            /// </summary>
            public DroneInParcel() { this.CLocation = new Location(); }

            /// <summary><returns>
            /// The function returns a string to print on all entity data
            /// </returns></summary>
            public override string ToString() 
            { return $"Id: {Id}\nBattery: {Math.Round(Battery, 3)}%\nCurrent Location: {CLocation.ToString()}"; }
        }
    }
}