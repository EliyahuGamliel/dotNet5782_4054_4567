using System;

namespace DO
{
    /// <summary>
    /// Defining the "DroneCharge" struct
    /// </summary>
    public struct DroneCharge
    {
        public int DroneId { get; set; }
        public int StationId { get; set; }
        public DateTime? Start { get; set; }

        /// <summary><returns>
        /// The function returns a string to print on all entity data
        /// </returns></summary>
        public override string ToString()
        { return $"droneId: {DroneId}\nStationld: {StationId}\n"; }
    }
}
