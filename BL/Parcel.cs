using System;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// Defining the "Parcel" class
        /// </summary>
        public struct Parcel
        {
            public int Id { get; set; }
            public int SenderId { get; set; }
            public int TargetId { get; set; }
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
            { return $"Id: {Id}\nSenderId: {SenderId}\nTargetId: {TargetId}\nWeight: {Weight}\nDrone: {Drone.ToString()}Priority: {Priority}\nRequested: {Requested}\nScheduled: {Scheduled}\nPickedUp: {PickedUp}\nDelivered:{Delivered}\n"; }
        }
    }
}