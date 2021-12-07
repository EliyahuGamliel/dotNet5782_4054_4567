using System;

namespace BO
{
    /// <summary>
    /// Defining the "ParcelList" class
    /// </summary>
    public class ParcelList
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int TargetId { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public Statuses Status { get; set; }

        /// <summary><returns>
        /// The function returns a string to print on all entity data
        /// </returns></summary>
        public override string ToString()
        { return $"Id: {Id}\nSenderId: {SenderId}\nTargetId: {TargetId}\nWeight: {Weight}\nPriority: {Priority}\nStatus: {Status}\n"; }
    }

}