using System;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// Defining the "ParcelInCustomer" class
        /// </summary>
        public class ParcelInCustomer
        {
            public int Id { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public Statuses Status { get; set; }
            public CustomerInParcel CParcel { get; set; }
            
            /// <summary>
            /// Ctor of ParcelInCustomer
            /// </summary>
            public ParcelInCustomer() { this.CParcel = new CustomerInParcel(); }
            
            /// <summary><returns>
            /// The function returns a string to print on all entity data
            /// </returns></summary>
            public override string ToString() 
            { return $"    Id: {Id}\n    Weight: {Weight}\n    Priority: {Priority}\n    Status: {Status}\n    Customer In Parcel: {CParcel.ToString()}"; }
        }
    }
}
