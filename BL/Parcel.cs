using System;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// Defining the "Parcel" class
        /// </summary>
        public class Parcel
        {
            public int Id { get; set; }
            public CustomerInParcel Sender { get; set; }
            public CustomerInParcel Target { get; set; }
            public Drone Drone { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public DateTime Requested{ get; set; }
            public DateTime Scheduled{ get; set; }
            public DateTime PickedUp{ get; set; }
            public DateTime Delivered{ get; set; }

            /// <summary><returns>
            /// The function returns a string to print on all entity data
            /// </returns></summary>
            public override string ToString() 
            { return $"Id: {Id}\nWeight: {Weight}\nPriority: {Priority}\nRequested: {Requested}\nScheduled: {Scheduled}\nPickedUp: {PickedUp}\nDelivered: {Delivered}\nDrone: \n    {Drone.ToString()}Target: {Target.ToString()}Sender: {Sender}\n"; }
        }
    }
}