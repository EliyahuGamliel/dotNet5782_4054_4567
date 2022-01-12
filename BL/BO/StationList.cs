namespace BO
{
    /// <summary>
    /// Defining the "StationList" class
    /// </summary>
    public class StationList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ChargeSlots { get; set; }
        public int ChargeSlotsCatched { get; set; }

        /// <summary><returns>
        /// The function returns a string to print on all entity data
        /// </returns></summary>
        public override string ToString() { return $"Id: {Id}\nName: {Name}\nCharge Slots Avaliable: {ChargeSlots}\nCharge Slots Catched: {ChargeSlotsCatched}\n"; }
    }
}
