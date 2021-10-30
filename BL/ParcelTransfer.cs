using System;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// Defining the "Customer" class
        /// </summary>
        public struct ParcelTransfer
        {
            public int Id { get; set; }
            public Priorities Priority { get; set; }
            public CustomerDelivery Sender { get; set; }
            public CustomerDelivery Recipient { get; set; }
            
            /// <summary><returns>
            /// The function returns a string to print on all entity data
            /// </returns></summary>
            public override string ToString() 
            { return $"Id: {Id}\nPriority: {Priority}\nThe sender: {Sender.ToString()}The recipient: {Recipient.ToString()}"; }
        }
    }
}
