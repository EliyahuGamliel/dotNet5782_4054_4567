namespace BO
{
    /// <summary>
    /// Defining the "ParcelTransfer" class
    /// </summary>
    public class ParcelTransfer
    {
        public int Id { get; set; }
        public Priorities Priority { get; set; }
        public CustomerInParcel Sender { get; set; }
        public CustomerInParcel Recipient { get; set; }
        public Location CollectionLocation { get; set; }
        public Location DestinationLocation { get; set; }
        public double TransportDistance { get; set; }
        public WeightCategories Weight { get; set; }
        public bool Status { get; set; }

        /// <summary><returns>
        /// The function returns a string to print on all entity data
        /// </returns></summary>
        public override string ToString() { return $"\n      Id: {Id}\n      Priority: {Priority}\n      Weight: {Weight}\n      Transport Distance: {TransportDistance}\n      Status: {Status}\n      Collection Location: {CollectionLocation}      Destination Location: {DestinationLocation}      The Sender: {Sender}      The Recipient: {Recipient}"; }
    }
}
