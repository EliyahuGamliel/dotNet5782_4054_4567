using System;

namespace IDAL
{
    namespace DO
    {
        /// <summary>
        /// Defining the "DroneCharge" class
        /// </summary>
        public struct DroneCharge
        {
            public int DroneId { get; set; }
            public int StationId { get; set; }
            
            /// <summary><returns>
            /// The function returns a string to print on all entity data
            /// </returns></summary>
            public override string ToString() 
            { return $"droneId: {DroneId}\nStationld: {StationId}\n"; }
        } 
    }
}
