using System;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// Defining the "Customer" class
        /// </summary>
        public class ParcelTransfer
        {
            public int Id { get; set; }
            public Priorities Priority { get; set; }
            public CustomerInParcel Sender { get; set; }
            public CustomerInParcel Recipient { get; set; }
            public Location Collection_Location { get; set; }
            public Location Destination_Location { get; set; }
            public double Transport_Distance { get; set; }
            public WeightCategories Weight { get; set; }
            public bool Status { get; set; }
            
            
            /// <summary><returns>
            /// The function returns a string to print on all entity data
            /// </returns></summary>
            public override string ToString() 
            { return $"Id: {Id}\nPriority: {Priority}\nWeight: {Weight}\nTransport Distance: {Transport_Distance}\nStatus: {Status}\nCollection Location: {Collection_Location}Destination Location: {Destination_Location}The Sender: {Sender.ToString()}The Recipient: {Recipient.ToString()}"; }
        }
    }
}
