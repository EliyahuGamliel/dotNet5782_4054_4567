using System;
using DO;
using DalApi;

namespace BO
{
    /// <summary>
    /// Defining the "DroneCharge" class
    /// </summary>
    public class DroneCharge
    {
        public int Id { get; set; }
        public double Battery { get; set; }

        /// <summary><returns>
        /// The function returns a string to print on all entity data
        /// </returns></summary>
        public override string ToString()
        { return $"\n    Id: {Id}\n    Battery: {Math.Round(Battery, 0)}%\n"; }
    }
}