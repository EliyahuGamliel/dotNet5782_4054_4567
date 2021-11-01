using System;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// Defining the "Customer" class
        /// </summary>
        public struct DeliveryInCustomer
        {
            public int Id { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public Statuses Status { get; set; }
            public CustomerDelivery CDelivery { get; set; }
            
            /// <summary><returns>
            /// The function returns a string to print on all entity data
            /// </returns></summary>
            public override string ToString() 
            { return $"Id: {Id}\nWeight: {Weight}\nPriority: {Priority}\nStatus: {Status}\n"; } //to fix
        }
    }
}