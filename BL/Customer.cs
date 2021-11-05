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

            /// <summary><returns>
            /// The function returns a string to print on all entity data
            /// </returns></summary>
            public override string ToString() 
            { return $"Id: {Id}\nName: {Name}\nPhone: {Phone}\nLocation: {Location.ToString()}Parcel From Customer: {Phone}\n"; } //to fix
        }
    }
}
