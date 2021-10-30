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
            //public DroneStatuses Status{ get; set; } - targil2
            //public double Battery { get; set; } - targil2

            /// <summary><returns>
            /// The function returns a string to print on all entity data
            /// </returns></summary>
            public override string ToString() 
            { //return $"Id: {Id}\nModel: {Model}\nMaxWeight: {MaxWeight}\nStatus: {Status}\nBattery: {Battery}\n";
                return $"Id: {Id}\nModel: {Model}\nMaxWeight: {MaxWeight}\n";
            }
        }   
    }
}
