using System;
using System.Collections.Generic;

namespace IBL
{
    namespace BO
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

            public Customer() { FromCustomer = new List<ParcelInCustomer>(); ForCustomer = new List<ParcelInCustomer>(); }

            /// <summary><returns>
            /// The function returns a string to print on all entity data
            /// </returns></summary>
            public override string ToString() 
            {
                string str = $"Id: {Id}\nName: {Name}\nPhone: {Phone}\nLocation: {Location.ToString()}Parcels From Customer: ";
                foreach (var item in FromCustomer)
                    str += item.ToString();
                str += $"Parcels For Customer: ";
                foreach (var item in ForCustomer)
                    str += item.ToString();
                return str;
            }
        }
    }
}
