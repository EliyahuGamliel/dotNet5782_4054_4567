using System;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// Defining the "Customer" class
        /// </summary>
        public struct DroneCharge
        {
            public int Id { get; set; }
            public Double Battery { get; set; }
            
            /// <summary><returns>
            /// The function returns a string to print on all entity data
            /// </returns></summary>
            public override string ToString() 
            { return $"Id: {Id}\nBattery: {Battery}ֿֿ\n"; }
        }
    }
}
