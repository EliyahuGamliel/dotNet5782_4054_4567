using System;
using System.Collections.Generic;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// Defining the "Customer" class
        /// </summary>
        public struct Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public Location Location { get; set; }
            public List<DeliveryInCustomer> FromCustomer { get; set; }
            public List<DeliveryInCustomer> ForCustomer { get; set; }

            /// <summary><returns>
            /// The function returns a string to print on all entity data
            /// </returns></summary>
            public override string ToString() 
            { return $"Id: {Id}\nName: {Name}\nPhone: {Phone}\nLocation: {Location.ToString()}Parcel From Customer: {}\n"; } //to fix

            /// <summary>
            /// The function gets a longitube and turns it into a longitube at base 60
            /// </summary>
            /// <param name="num">The number to change</param>
            /// <returns>The function returns a string of a number representing base 60</returns>
        }
    }
}
