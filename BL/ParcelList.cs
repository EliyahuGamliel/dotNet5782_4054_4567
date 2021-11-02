using System;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// Defining the "Parcel" class
        /// </summary>
        public class ParcelList
        {
            public int Id { get; set; }
            public int SenderId { get; set; }
            public int TargetId { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public Statuses Statis { get; set; }

            /// <summary><returns>
            /// The function returns a string to print on all entity data
            /// </returns></summary>
            public override string ToString() 
            { return $"Id: {Id}\nSenderId: {SenderId}\nTargetId: {TargetId}\nWeight: {Weight}\nPriority: {Priority}\n"; } //to fix
        }
    }
}