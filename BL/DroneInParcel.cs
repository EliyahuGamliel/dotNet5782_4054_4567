using System;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// Defining the "Customer" class
        /// </summary>
        public class DroneInParcel
        {
            public int Id { get; set; }
            public double Battery { get; set; }
            public Location CLocation { get; set; }
            

            /// <summary><returns>
            /// The function returns a string to print on all entity data
            /// </returns></summary>
            public override string ToString() 
            { return $"Id: {Id}\nBattery: {Battery}ֿֿ\n"; } //to fix
        }
    }
}