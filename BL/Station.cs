using System;
using System.Collections.Generic;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// Defining the "Customer" class
        /// </summary>
        public class Station
        {
            public int Id { get; set; }
            public int Name { get; set; }
            public Location Location { get; set; } 
            public int ChargeSlots { get; set; }
            public List<DroneCharge> DCharge { get; set; }

            public Station() { this.Location = new Location(); this.DCharge = new List<DroneCharge>(); }
            
            /// <summary><returns>
            /// The function returns a string to print on all entity data
            /// </returns></summary>
            public override string ToString() 
            { return $"Id: {Id}\nName: {Name}\nLocation: {Location.ToString()}ChargeSlots: {ChargeSlots}\n"; }  //to fix    
        }
    }
}
