using System;
using System.Collections.Generic;

namespace BO
{
    /// <summary>
    /// Defining the "Station" class
    /// </summary>
    public class Station
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public int ChargeSlots { get; set; }
        public List<DroneCharge> DCharge { get; set; }

        /// <summary><returns>
        /// The function returns a string to print on all entity data
        /// </returns></summary>
        public override string ToString()
        {
            string str = $"\nId: {Id}\nName: {Name}\nLocation: {Location}Charge Slots: {ChargeSlots}\nDrones in Charging: ";
            foreach (var item in DCharge)
                str += item;
            str += $"\n";
            return str;
        }
    }
}