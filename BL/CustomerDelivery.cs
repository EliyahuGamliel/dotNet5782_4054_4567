using System;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// Defining the "Customer" class
        /// </summary>
        public struct CustomerDelivery
        {
            public int Id { get; set; }
            public string Name { get; set; }
            
            /// <summary><returns>
            /// The function returns a string to print on all entity data
            /// </returns></summary>
            public override string ToString() 
            { return $"Id: {Id}\nName: {Name}\n"; }
        }
    }
}
