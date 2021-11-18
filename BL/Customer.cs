using System;
using System.Collections.Generic;

namespace IBL.BO
{


    /// <summary>
    /// Defining the "Customer" class
    /// </summary>
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Location Location { get; set; }
        public List<ParcelInCustomer> FromCustomer { get; set; }
        public List<ParcelInCustomer> ForCustomer { get; set; }

        /// <summary>
        /// Ctor of Customer
        /// </summary>
        public Customer() { FromCustomer = new List<ParcelInCustomer>(); ForCustomer = new List<ParcelInCustomer>(); this.Location = new Location(); }

        /// <summary><returns>
        /// The function returns a string to print on all entity data
        /// </returns></summary>
        public override string ToString()
        {
            string str = $"Id: {Id}\nName: {Name}\nPhone: {Phone}\nLocation: {Location}Parcels From Customer: \n";
            foreach (var item in FromCustomer)
                str += item;
            str += $"Parcels For Customer: \n";
            foreach (var item in ForCustomer)
                str += item;
            return str;
        }
    }

}
