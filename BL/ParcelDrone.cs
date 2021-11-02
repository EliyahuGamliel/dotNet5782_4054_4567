using System;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// Defining the "Customer" class
        /// </summary>
        public class ParcelDrone
        {
            public int Id { get; set; }
            public Double Battery { get; set; }
            public Location Location { get; set; }
            
            /// <summary><returns>
            /// The function returns a string to print on all entity data
            /// </returns></summary>
            public override string ToString() 
            { return $"Id: {Id}\nBattery: {Battery}ֿֿ\nCurrent Location: {Location.ToString()}"; }
        }
    }
}
