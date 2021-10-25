using System;

namespace IDAL
{
    namespace DO
    {
        /// <summary>
        /// Defining the "Parcel" class
        /// </summary>
        public struct Parcel
        {
            public int Id { get; set; }
            public int SenderId { get; set; }
            public int TargetId { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities priority{ get; set; }
            public DateTime Requested{ get; set; }
            public int DroneId{ get; set; }
            public DateTime Scheduled{ get; set; }
            public DateTime PickedUp{ get; set; }
            public DateTime Delivered{ get; set; }

            /// <summary><returns>
            /// The function returns a string to print on all entity data
            /// </returns></summary>
            public override string ToString() 
            { return $"Id: {Id}\nSenderld: {SenderId}\nTargetld: {TargetId}\nWeight: {Weight}\nRequested: {Requested}\ndroneId: {DroneId}\nScheduled: {Scheduled}\nPickedUp: {PickedUp}\nDelivered:{Delivered}\n"; }
        }
    }
}
