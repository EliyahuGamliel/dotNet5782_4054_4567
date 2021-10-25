using System;

namespace IDAL
{
    namespace DO
    {
        /// <summary>
        /// Defining the "Drone" class
        /// </summary>
       public struct Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public DroneStatuses Status{ get; set; }
            public double Battery { get; set; }

            /// <summary><returns>
            /// The function returns a string to print on all entity data
            /// </returns></summary>
            public override string ToString() 
            { return $"Id: {Id}\nModel: {Model}\nMaxWeight: {MaxWeight}\nStatus: {Status}\nBattery: {Battery}\n"; }
        }   
    }
}
