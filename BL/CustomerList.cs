using System;
using System.Collections.Generic;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// Defining the "Customer" class
        /// </summary>
        public class CustomerList
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public int ParcelsSent { get; set; }
            public int ParcelsOnlySend { get; set; }    
            public int ParcelsGet { get; set; }
            public int ParcelsInTheWay { get; set; }

            /// <summary><returns>
            /// The function returns a string to print on all entity data
            /// </returns></summary>
            public override string ToString() 
            { return $"Id: {Id}\nName: {Name}\nPhone: {Phone}\nThe amount of Parcels shipped and arrived: {ParcelsSent}\nThe amount of Parcels shipped and on the way: {ParcelsInTheWay}\nThe amount of Parcels sent and not yet delivered: {ParcelsOnlySend}\nThe amount of Parcels received: {ParcelsGet}\n"; } //to fix
        }
    }
}
